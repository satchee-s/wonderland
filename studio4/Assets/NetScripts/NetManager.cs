using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using core;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class NetManager : MonoBehaviour
{
    delegate void ConnectedToServer();
    ConnectedToServer ConnectedToServerEvent;

    [Header("Connect Panel")]
    [SerializeField] Button connectButton;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] GameObject connectPanel;
    [SerializeField] GameObject matchMakingPanel;

    [SerializeField] GameObject opponentFound;
    [SerializeField] GameObject lookingForOpponent;

    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] TextMeshProUGUI setPlayerName;

    SceneController sC;
    Socket socket;
    Player player;
    public NetworkComponent nc;
    Card card;
    GameManager gameManager;
    PlayerManager playerManager;

    List<GameObject> playerObjs = new List<GameObject>();
    //List<TextMeshProUGUI> playerName = new List<TextMeshProUGUI>();
    public TextMeshProUGUI[] playerName;

    // Start is called before the first frame update
    void Start()
    {
        connectButton.onClick.AddListener(() =>
        {
            try
            {
                player = new Player(Guid.NewGuid().ToString(), playerNameInputField.text);
                
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
                socket.Send(new InformationPacket(player).StartSerialization());

                connectPanel.SetActive(false);
                matchMakingPanel.SetActive(true);
                startButton.gameObject.SetActive(false);

                socket.Blocking = false;

                //if one player joins, and lobby is empty look for another player UI should pop up and wait until another player Joins

                //Debug.Log(nc.GameObjectID );
                //Debug.Log(nc.prefabName);
                //Debug.Log(nc.GameId);
                //Debug.Log(nc.OwnerID);

                InstantiateOverNetwork(nc.prefabName, Vector3.zero, Quaternion.identity);

                Rig();

                if (ConnectedToServerEvent != null) ConnectedToServerEvent();

            }
            catch (SocketException e)
            {
                print(e);
            }

        });
    }

    void Update()
    {
        if (socket != null)
        {
            if (socket.Available > 0)
            {

                print("receiving from lobby");
                byte[] recievedBuffer = new byte[socket.Available];

                socket.Receive(recievedBuffer);

                BasePacket pb = new BasePacket().StartDeserialization(recievedBuffer);

                switch (pb.Type)
                {
                    case BasePacket.PacketType.Lobby:
                        LobbyPacket lp = (LobbyPacket)new LobbyPacket().StartDeserialization(recievedBuffer);


                        for (int i = 0; i < lp.clientsName.Count; i++) // loop 
                        {
                            print(lp.clientsName[i]);
                            playerName[i].text = lp.clientsName[i];
                        }
                        if(lp.clientsName.Count == 1) //this is the first player that joins
                        {
                            //set this player as player 1
                            opponentFound.SetActive(false);
                            lookingForOpponent.SetActive(true);
                        }
                        if(lp.clientsName.Count == 2) //this is when the 2nd client joins
                        {
                            //set this player as player 2
                            startButton.gameObject.SetActive(true);
                            print("doggy");
                            opponentFound.SetActive(true);
                            lookingForOpponent.SetActive(false);
                        }
                        //if both players are there, then player 1 should click on start and launch both players into the game scene!

                        break;

                    case BasePacket.PacketType.Message:
                        MessagePacket mp = (MessagePacket)new MessagePacket().StartDeserialization(recievedBuffer);

                        print($"{mp.player.Name}Said:{mp.message}");
                        break;

                    case BasePacket.PacketType.Instantiate:
                        {
                            Debug.Log("Received instantiate packet");
                            InstantiatePacket ip = new InstantiatePacket();
                            ip.StartDeserialization(recievedBuffer);

                            print(ip.player.ID);
                            print(ip.player.Name);
                            print(ip.PrefabName);

                            InstantiateFromResources(ip.PrefabName, ip.Position, ip.Rotation, ip.GameObjectId, ip.player);
                            break;
                        }

                    case BasePacket.PacketType.Rigidbody:
                        {
                            RigidbodyPacket Rp = new RigidbodyPacket();
                            Rp.StartDeserialization(recievedBuffer);
                            break;
                        }

                    case BasePacket.PacketType.Destroy:
                        {
                            DestroyPacket Dp = new DestroyPacket();
                            Dp.StartDeserialization(recievedBuffer);
                            print(Dp.GameObjectId);

                            DestroyObject(Dp.GameObjectId, player);
                            break;
                        }

                    case BasePacket.PacketType.Position:
                        PositionPacket PP = new PositionPacket();
                        PP.StartDeserialization(recievedBuffer);

                        break;

                    case BasePacket.PacketType.Rotation:
                        RotationPacket RotatP = new RotationPacket();
                        RotatP.StartDeserialization(recievedBuffer);

                        break;

                    case BasePacket.PacketType.Card:
                        CardPacket Card = new CardPacket();
                        Card.StartDeserialization(recievedBuffer);


                        CardInformation();
                        break;

                    case BasePacket.PacketType.Acknowledged:
                        acknowledgedPacket AP = new acknowledgedPacket();

                        break;

                    case BasePacket.PacketType.RotationAndPosition:
                        RotationAndPositonPacket RPP = new RotationAndPositonPacket();

                        break;
                    default:
                        break;
                }
            }

        }
        else
        {

        }

    }

    GameObject InstantiateOverNetwork(string prefabName, Vector3 position, Quaternion rotation)
    {
        Debug.Log(Resources.Load($"Prefabs/{prefabName}"));
        GameObject go = Instantiate(Resources.Load($"Prefabs/{prefabName}"), position, rotation) as GameObject;
        nc = go.AddComponent<NetworkComponent>();
        nc.OwnerID = player.ID;
        nc.GameObjectID = Guid.NewGuid().ToString("N");
        // Debug.Log(rotation);
        // Debug.Log(prefabName);
        //Debug.Log(position);
        //Debug.Log(nc.GameObjectID);

        playerObjs.Add(go);

        socket.Send(new InstantiatePacket(nc.GameObjectID, prefabName, position, rotation, player).StartSerialization());

        return go;
    }


    GameObject InstantiateFromResources(string prefabName, Vector3 position, Quaternion rotation, string gameObjectID, Player player)
    {
        GameObject go = Instantiate(Resources.Load($"Prefabs/{prefabName}"), position, rotation) as GameObject;
        nc = go.AddComponent<NetworkComponent>();
        nc.OwnerID = player.ID;
        nc.GameObjectID = gameObjectID;


        return go;
    }

  private void Rig()
    {


        GameObject go = playerObjs[0];
        go.GetComponent<Rigidbody>();

        Debug.Log(go);
        Debug.Log(go.GetComponent<Rigidbody>());

        Vector3 Velocity = go.GetComponent<Rigidbody>().velocity;
        Debug.Log(Velocity);

        socket.Send(new RigidbodyPacket(player, nc.GameObjectID, Velocity).StartSerialization());
        Debug.Log("send");


    }

    /***
    UI

    ***/


  private  void DestroyObject(string GameObjectID, Player player)
    {
        NetworkComponent[] nc = FindObjectsOfType<NetworkComponent>();

        for (int i = 0; i < nc.Length; i++)
        {
            if (nc[i].GameObjectID == GameObjectID)
            {
                Destroy(nc[i].gameObject);
                break;
            }

        }

    }

  private Card  CardInformation()
    {
        card = GetComponent<Card>();

        socket.Send(new CardPacket(card.cardId, card.cardName, card.health, card.attack, card.sleep, player).StartSerialization());
        return card;
    }

 private void AcknowledgedInformation()
    {
        //gameManager = GetComponent<GameManager>();
        playerManager = GetComponent<PlayerManager>();


        socket.Send(new acknowledgedPacket().StartSerialization());
    }

private void getPosition()
    {
        GameObject go = playerObjs[0];
        go.GetComponent<Transform>();

        socket.Send(new PositionPacket(go.transform.position, player).StartSerialization());
    }


private void getRotation()
    {
        GameObject go = playerObjs[0];
        go.GetComponent<Transform>();



        socket.Send(new RotationPacket(go.transform.rotation, player).StartSerialization());
    }

    private void getPositionAndRotation()
    {
        GameObject go = playerObjs[0];
        go.GetComponent<Transform>();


        socket.Send(new RotationAndPositonPacket(go.transform.position, go.transform.rotation, player).StartSerialization());
    }
}