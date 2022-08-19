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
using UnityEngine.SceneManagement;
using System.Threading;

public class NetManager : MonoBehaviour
{
    delegate void ConnectedToServer();
    ConnectedToServer ConnectedToServerEvent;

    [Header("Connect Panel")]
    [SerializeField] Button connectButton;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] GameObject connectPanel;
    [SerializeField] GameObject matchMakingPanel;
    [SerializeField] GameObject Player2Panel;

    [SerializeField] GameObject opponentFound;
    [SerializeField] GameObject lookingForOpponent;

    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;
    [SerializeField] TextMeshProUGUI setPlayerName;

    SceneController sC;
    [HideInInspector] public Socket socket;
    public Player player;
    public NetworkComponent nc;
    Card card;
    GameManager gameManager;
    PlayerManager playerManager;
    PlayerTurnSystem turnSystem;
    [SerializeField] PlayerRole role;

    List<GameObject> playerObjs = new List<GameObject>();
    public TextMeshProUGUI[] playerName;
    [SerializeField] PlayerManager [] playerManagers = new PlayerManager [2];
    //don't destroy on load

    void Start()
    {
        connectButton.onClick.AddListener(() =>
        {
            try
            {
                player = new Player(Guid.NewGuid().ToString(), playerNameInputField.text);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
                

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
                //Thread.Sleep(2000);
                Rig();

                if (ConnectedToServerEvent != null) ConnectedToServerEvent();

            }
            catch (SocketException e)
            {
                print(e);
            }

            turnSystem = FindObjectOfType<PlayerTurnSystem>();
        });
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (socket != null)
        {
            Debug.Log(socket.Available);
            if (socket.Available > 0)
            {
                byte[] recievedBuffer = new byte[socket.Available];

                socket.Receive(recievedBuffer);

                BasePacket pb = new BasePacket().StartDeserialization(recievedBuffer);
                Debug.Log("Heres your base packet:" + pb.Type);
                Debug.Log("received packet");
                switch(pb.Type)
                {
                    case BasePacket.PacketType.Lobby:
                        Debug.Log("case 1");

                        LobbyPacket LP = (LobbyPacket)new LobbyPacket().StartDeserialization(recievedBuffer);
                        for (int i = 0; i < LP.clientsName.Count; i++) // loop 
                        {
                            print(LP.clientsName[i]);
                            playerName[i].text = LP.clientsName[i];
                        }
                        if (LP.clientsName.Count == 1) //this is the first player that joins
                        {
                            Debug.Log("if1");
                            //set this player as player 1 from player manager + roles
                            Player2Panel.SetActive(false);
                            opponentFound.SetActive(false);
                            lookingForOpponent.SetActive(true);
                            playerManagers[0].role = PlayerManager.Role.Player1;
                        }
                        if (LP.clientsName.Count == 2) //this is when the 2nd client joins
                        {
                            Debug.Log("if2");
                            //set this player as player 2
                            Player2Panel.SetActive(true);
                            startButton.gameObject.SetActive(true);
                            opponentFound.SetActive(true);
                            lookingForOpponent.SetActive(false);
                            playerManagers[1].role = PlayerManager.Role.Player2;
                        }
                        //if both players are there, then player 1 should click on start and launch both players into the game scene!

                        break;

                    //Once start button has been clicked, send packet to server, then have server send TransitionToScene Packet to all exisiting clients
                    //case BasePacket.PacketType.Connection:
                    //  ConnectionPacket mp = (ConnectionPacket)new ConnectionPacket().StartDeserialization(recievedBuffer);

                    case BasePacket.PacketType.Message:
                        {
                        MessagePacket mp = (MessagePacket)new MessagePacket().StartDeserialization(recievedBuffer);
                        Debug.Log("case 2");

                        print($"{mp.player.Name}Said:{mp.message}");
                        break;
                        }
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

                        getPosition();
                        break;

                    case BasePacket.PacketType.Rotation:
                        RotationPacket RotatP = new RotationPacket();
                        RotatP.StartDeserialization(recievedBuffer);

                        getRotation();
                        break;

                    case BasePacket.PacketType.Card:
                        CardPacket cp = new CardPacket();
                        cp.StartDeserialization(recievedBuffer);
                        CardInformation();
                        UpdateCardStats(cp.cardHealth, cp.sleep);
                        break;

                    case BasePacket.PacketType.Acknowledged:
                        AcknowledgedPacket AP = new AcknowledgedPacket(player);
                        AP.StartDeserialization(recievedBuffer);
                        socket.Send(new InformationPacket(player).StartSerialization());
                        Debug.Log("Acknowledged");
                        break;

                    case BasePacket.PacketType.RotationAndPosition:
                        RotationAndPositonPacket RPP = new RotationAndPositonPacket();

                        getPositionAndRotation();
                        break;

                    case BasePacket.PacketType.PlayerData:
                        PlayerDataPacket PD = new PlayerDataPacket();
                        PD.StartDeserialization(recievedBuffer);
                        PlayerData(PD);
                        break;


                    case BasePacket.PacketType.StartGame:
                        StartGamePacket SG = new StartGamePacket(player);
                        startButton.onClick.AddListener(() =>
                        {
                            startGame();
                        });
                        Debug.Log("startGame");
                        break;
                    default:
                        break;
                }
            }

        }
    }

    void UpdateCardStats(int health, bool sleep)
    {
        for (int i = 0; i < playerManager.playedCards.Count; i++)
        {
            if (playerManager.playedCards[i] == card)
            {
                playerManager.playedCards[i].health = health;
                playerManager.playedCards[i].sleep = sleep;
            }
        }
    }

    private void PlayerData(PlayerDataPacket PD)
    {
        if (PD.player != player)
        {
            turnSystem.ChangeOpponentMana(PD.Mana);
            if (playerManager.IsPlayer2)
            {
                this.playerManager.health = PD.Health;
            }

        }
    }

    GameObject InstantiateOverNetwork(string prefabName, Vector3 position, Quaternion rotation)
    {
        Debug.Log(Resources.Load($"Prefabs/{prefabName}"));
        GameObject go = Instantiate(Resources.Load($"Prefabs/{prefabName}"), position, rotation) as GameObject;
        nc = go.AddComponent<NetworkComponent>();
        nc.OwnerID = player.ID;
        nc.GameObjectID = Guid.NewGuid().ToString("N");
         //Debug.Log(rotation);
         //Debug.Log(prefabName);
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

    private void DestroyObject(string GameObjectID, Player player)
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

    private Card CardInformation()
    {
        card = GetComponent<Card>();
        socket.Send(new CardPacket(card.cardId, card.cardName, card.health, card.attack, card.sleep, player).StartSerialization());
        return card;
    }

    private void AcknowledgedInformation()
    {
        //gameManager = GetComponent<GameManager>();
        playerManager = GetComponent<PlayerManager>();


        socket.Send(new AcknowledgedPacket(player).StartSerialization());
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

    public void SendPacket(byte[] buffer)
    {
        socket.Send(buffer);
    }

    public void startGame()
    {
        SceneManager.LoadScene("Level Design 2");
    }
}
