
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using core;
using UnityEngine.UI;
using TMPro;
using System;

public class NetManager : MonoBehaviour
{
    delegate void ConnectedToServer();
    ConnectedToServer ConnectedToServerEvent;

   
    
    [Header("Connect Panel")]
    [SerializeField] Button connectButton;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] GameObject connectPanel;

   


    Socket socket;
    Player player;
    NetworkComponent nc;

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

                InstantiateOverNetwork(nc.prefabName, Vector3.zero, Quaternion.identity);

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
                    case BasePacket.PacketType.Message:
                        MessagePacket mp = (MessagePacket)new MessagePacket().StartDeserialization(recievedBuffer);

                        print($"{mp.player.Name}Said:{mp.message}");
                        break;

                    case BasePacket.PacketType.Instantiate:
                        {
                            Debug.Log("Received instantiate packet");
                            InstantiatePacket ip = new InstantiatePacket(player);
                            ip.StartDeserialization(recievedBuffer);

                            print(ip.player.ID);
                            print(ip.player.Name);
                            print(ip.PrefabName);

                            // InstantiateFromResources(ip.PrefabName, ip.Position, ip.Rotation, ip.GameObjectID, ip.Player);
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
        GameObject go = Instantiate(Resources.Load($"Prefab/{prefabName}"), position, rotation) as GameObject;
        nc = go.AddComponent<NetworkComponent>();
        nc.OwnerID = player.ID;
        nc.GameObjectID = Guid.NewGuid().ToString("N");
        socket.Send(new InstantiatePacket(prefabName, nc.GameObjectID, position, rotation).StartSerialization());

        return go;
    }


    GameObject InstantiateFromResources(string prefabName, Vector3 position, Quaternion rotation, string gameObjectID, Player player)
    {
        GameObject go = Instantiate(Resources.Load($"Prefab/{prefabName}"), position, rotation) as GameObject;
        nc = go.AddComponent<NetworkComponent>();
        nc.OwnerID = player.ID;
        nc.GameObjectID = gameObjectID;


        return go;
    }
}