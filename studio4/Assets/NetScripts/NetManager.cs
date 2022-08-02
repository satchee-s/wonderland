using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using core;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

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
    [SerializeField] TextMeshProUGUI setPlayerName;

    Socket socket;
    Player player;
    public NetworkComponent nc;

    List<GameObject> playerObjs = new List<GameObject>();
    //List<TextMeshProUGUI> playerName = new List<TextMeshProUGUI>();
    public TextMeshProUGUI[] playerName = new TextMeshProUGUI[2];

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
                socket.Blocking = false;

                connectPanel.SetActive(false);
                matchMakingPanel.SetActive(true);

                playerName[0].text = playerNameInputField.text;
               // playerName[1].text = playerNameInputField.text;



                lookingForOpponent.SetActive(true);


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
                byte[] recievedBuffer = new byte[socket.Available];

                socket.Receive(recievedBuffer);
                BasePacket pb = new BasePacket().StartDeserialization(recievedBuffer);

                switch (pb.Type)
                {

                    case BasePacket.PacketType.Lobby:
                        LobbyPacket lp = (LobbyPacket)new LobbyPacket().StartDeserialization(recievedBuffer);

                        playerName[1].text = lp.clientsName[lp.clientsName.Count];

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

    void Rig()
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


    void DestroyObject(string GameObjectID, Player player)
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
}