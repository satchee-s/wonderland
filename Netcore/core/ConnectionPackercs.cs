using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class ConnectionPacket : BasePacket // Load Packet
    {
        public string Scene { get; private set; }
        public ConnectionPacket()
        {
            Scene = "";
        }

        public ConnectionPacket(string scene, Player player) : base(PacketType.Connection, player)
        {
            this.Scene = scene;
            this.player = player;
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();

            bw.Write(Scene);
            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            Scene = br.ReadString();


            return this;
        }

    }
}
