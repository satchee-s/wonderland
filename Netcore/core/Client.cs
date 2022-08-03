using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace core
{
    public class Client
    {
        public Socket socket;
        public Player player;

        public Client(Socket socket, Player player)
        {
            this.socket = socket;
            this.player = player;
        }
    }
}
