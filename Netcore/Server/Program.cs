using System.Net.Sockets;
using System.Net;
using System.Text;
using core;

namespace Client
{

    public class Program
    {
        static void Main(string[] args)
        {
            Socket Listening = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Listening.Bind(new IPEndPoint(IPAddress.Any, 3000));

            Listening.Listen(10);
            Listening.Blocking = false;

            Console.WriteLine("Waiting for a Client to Connect");
            List<Socket> clients = new List<Socket>();

            while (true)
            {
                try
                {
                    clients.Add(Listening.Accept());
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


                        if (clients[i].Available > 0)
                        {
                            byte[] recievedBuffer = new byte[clients[i].Available];

                            clients[i].Receive(recievedBuffer);
                            BasePacket pb = new BasePacket().StartDeserialization(recievedBuffer);



                            switch (pb.Type)
                            {
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


                                    Console.WriteLine("(DestroyPacket");
                                    break;

                                case BasePacket.PacketType.Rigidbody:
                                    RigidbodyPacket RP = (RigidbodyPacket)new RigidbodyPacket().StartDeserialization(recievedBuffer);

                                    Console.WriteLine("(RigidbodyPacket");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                catch (SocketException se)
                {
                    Console.WriteLine("Exception");
                }

            }
         Console.ReadKey(); 
        }
    }
}