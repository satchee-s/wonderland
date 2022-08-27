using System.Net.Sockets;
using System.Net;
using System.Text;
using core;
using System.Diagnostics;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            Socket Listening = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Listening.Bind(new IPEndPoint(IPAddress.Any, 3000));
            Player player = new Player("12","name1");
            Listening.Listen(10);
            Listening.Blocking = false;
            int clientsOnline;

            Console.WriteLine("Waiting for a Client to Connect");
            List<Client> clients = new List<Client>();

            
            //2 clients which you already have
            //1 scene
            //clients change the scene in order based on their turn, then the scene is sent over to the other client
            while (true)
            {
                try
                {
                    clients.Add(new Client(Listening.Accept(), new Player("", "")));               
                    clients[clients.Count -1].socket.Send(new AcknowledgedPacket(player).StartSerialization());
                    Console.WriteLine("Client Connected");

                    if(clients.Count == 2)
                    {
                        clients[0].socket.Send(new StartGamePacket(player).StartSerialization());
                        Console.WriteLine(clients[0].socket);
                        Thread.Sleep(2000);
                        clients[1].socket.Send(new StartGamePacket(player).StartSerialization());
                        Console.WriteLine(clients[1].socket);
                        Console.WriteLine("send startgamePacket");
                    }
                 
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
                            switch (pb.Type)
                            {
                                case BasePacket.PacketType.Information:
                                    InformationPacket infoPacket = (InformationPacket)new InformationPacket().StartDeserialization(recievedBuffer);
                                    clients[i].player.ID   = infoPacket.player.ID;
                                    clients[i].player.Name = infoPacket.player.Name;
                                    Console.WriteLine("playerID infromation: "+ infoPacket.player.ID);
                                    Console.WriteLine("playerName infromation: " + infoPacket.player.Name);
                                    List<string> clientNames = new List<string>();
                                    Thread.Sleep(2000);
                                    for (int j = 0; j < clients.Count; j++)
                                    {
                                        clientNames.Add(clients[j].player.Name);
                                    }
                                    //add player info to Cilent List     
                                    for (int j = 0; j < clients.Count; j++) //For every cilent in list, send LobbyInfo Packet
                                    {
                                       // clients[j].player.PlayerNumber = j + 1;
                                        Console.WriteLine("Assigning player value " + clients[j].player);
                                        clients[j].socket.Send(new LobbyPacket(clientNames, clients[j].player).StartSerialization());
                                        Console.WriteLine($"Sent lobby packet {clientNames[j]}");
                                        Thread.Sleep(2000);
                                    }
                                    break;

                                default:
                                    for (int k = 0; k < clients.Count; k++)
                                    {
                                        if (k != i)
                                        {
                                            clients[k].socket.Send(recievedBuffer);

                                            Console.WriteLine($"{pb.Type} sent to {clients[k].player.Name}");
                                        }
                                    }
                                    break;

                            }
                        }
                    }
                }
                catch (SocketException se)
                {
                    Console.WriteLine(se);
                }
               // for(int i = 0; i < clients.Count; i++)
            }
         Console.ReadKey(); 
        }
    }
}