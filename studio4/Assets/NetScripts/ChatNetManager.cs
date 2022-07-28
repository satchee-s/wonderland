
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using core;
using UnityEngine.UI;
using TMPro;
using System;

public class ChatNetManager : MonoBehaviour
{
    delegate void ConnectedToServer();
    ConnectedToServer ConnectedToServerEvent;

    delegate void RecieveMessage();
    RecieveMessage RecieveMessageEvent;

    delegate void SendMessages();
    SendMessages SendMessageEvent;
    [Header("Connect Panel")]
    [SerializeField] Button connectButton;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] GameObject connectPanel;

    [Header("Chat Panel")]
    [SerializeField] GameObject ChatPanel;
    [SerializeField] Button SendButton;
    [SerializeField] TMP_InputField chatInputField;


    Socket socket;
    Player player;

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
                ChatPanel.SetActive(true);

                if (ConnectedToServerEvent != null) ConnectedToServerEvent();
                

            }
            catch(SocketException e)
            {
                print(e);
            }

        });

        SendButton.onClick.AddListener(() => 
        {
            socket.Send(new MessagePacket(player ,chatInputField.text).StartSerialization());

            if (RecieveMessageEvent != null) RecieveMessageEvent();


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
                    default:
                        break;
                }
            }
        }
        else
        {

        }
    }
}