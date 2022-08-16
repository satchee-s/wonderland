﻿using System.Net.Sockets;
using System.Net;
using System.Text;
using core;

namespace Server
{
 /*public  class Client
    {
        public Socket socket;
        public Player player;

        public Client(Socket socket, Player player)
        {
            this.socket = socket;
            this.player = player;
        }
    }*/
    public class Program
    {
        static void Main(string[] args)
        {
            Socket Listening = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Listening.Bind(new IPEndPoint(IPAddress.Any, 3000));

            Listening.Listen(10);
            Listening.Blocking = false;

            Console.WriteLine("Waiting for a Client to Connect");
            List<Client> clients = new List<Client>();

            //Player player;

            while (true)
            {
                try
                {
                    clients.Add(new Client(Listening.Accept(), new Player("", "")));               
                    //clients[clients.Count -1].socket.Send(new acknowledgedPacket().StartSerialization());
                    Console.WriteLine("Client Connected");
                 
                }
                catch (SocketException se)
                {
                    if (se.SocketErrorCode != SocketError.WouldBlock)
                        Console.WriteLine(se);
                }

                try
                {
                    for (int i = 0; i < clients.Count; i++)
                    {

                        if (clients[i].socket.Available > 0)
                        {
                            byte[] recievedBuffer = new byte[clients[i].socket.Available];
                            clients[i].socket.Receive(recievedBuffer);
                            
                            BasePacket pb = new BasePacket().StartDeserialization(recievedBuffer);

                           // player = pb.player;


                            switch (pb.Type)
                            {

                                case BasePacket.PacketType.Information:
                                    InformationPacket infoPacket = (InformationPacket)new InformationPacket().StartDeserialization(recievedBuffer);

                                    clients[i].player.ID   = infoPacket.player.ID;
                                    clients[i].player.Name = infoPacket.player.Name;

                                    //Room room = new Room(clients, 2);

                                    List<string> clientNames = new List<string>();

                                    for (int j = 0; j < clients.Count; j++)
                                    {
                                        clientNames.Add(clients[j].player.Name);
                                    }
                                    //add player info to Cilent List     

                                    for (int j = 0; j < clients.Count; j++) //For every cilent in list, send LobbyInfo Packet
                                    {
                                        clients[j].socket.Send(new LobbyPacket(clientNames, clients[j].player).StartSerialization());
                                        Console.WriteLine($"Sent lobby packet {clientNames[j]}");
                                    }
                                       
                                    
                                    break;

                              case BasePacket.PacketType.Message:
                                    MessagePacket mp = (MessagePacket)new MessagePacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine($"{mp.player.Name}Said:{mp.message}");
                                    break;

                                case BasePacket.PacketType.Instantiate:
                                    InstantiatePacket IP = (InstantiatePacket)new InstantiatePacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine("InstantiatePacket");
                                    break;

                                case BasePacket.PacketType.Destroy:
                                    DestroyPacket DP = (DestroyPacket)new DestroyPacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine("DestroyPacket");
                                    break;

                                case BasePacket.PacketType.Rigidbody:
                                    RigidbodyPacket RP = (RigidbodyPacket)new RigidbodyPacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine("RigidbodyPacket");
                                    break;

                                case BasePacket.PacketType.Position:
                                    PositionPacket PP = (PositionPacket)new PositionPacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine("PositionPacket");
                                    break;

                                case BasePacket.PacketType.Rotation:
                                    RotationPacket RotatP = (RotationPacket) new RotationPacket().StartDeserialization(recievedBuffer);
                                    Console.WriteLine("RotationPacket");

                                    break;

                                case BasePacket.PacketType.RotationAndPosition:
                                    RotationAndPositonPacket RPP = (RotationAndPositonPacket)new RotationAndPositonPacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine(" RotationAndPositonPacket");
                                    break;

                                case BasePacket.PacketType.Card:
                                    CardPacket CP = (CardPacket)new CardPacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine("CardPacket");

                                    break;

                                case BasePacket.PacketType.Lobby:
                                    LobbyPacket LP = (LobbyPacket)new LobbyPacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine("LobbyPacket");

                                    break;

                                case BasePacket.PacketType.Acknowledged:
                                    acknowledgedPacket AP = (acknowledgedPacket)new acknowledgedPacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine("acknowledgedPacket");

                                    break;
                                default:
                                    break;

                                    /*BasePacket bp = new BasePacket().Deserialize(receivedBuffer);
                                    client[i].Send(receivedBuffer);*/
                            }
                        }
                    }
                }
                catch (SocketException se)
                {
                    Console.WriteLine("Exception");
                }
               // for(int i = 0; i < clients.Count; i++)
            }
         Console.ReadKey(); 
        }
    }
}