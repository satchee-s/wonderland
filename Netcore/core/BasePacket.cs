using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;


namespace core
{
    public class BasePacket
    {
        protected MemoryStream ms;
        protected BinaryReader br;
        protected BinaryWriter bw;

        public enum PacketType
        {
            Unknown = -1,
            None,
            Transform,
            Instantiate,
            Destroy,
            Position,
            Rotation,
            Scale,
            RotationAndPosition,
            Message,
            Rigidbody,
             Information,
                Lobby
        }

      public PacketType Type { get; set; }
      public Player player { get;  set; }

        public BasePacket(PacketType type, Player player )
        {
            this.player = player;
            Type = type;
        }

        public BasePacket()
        {
            Type = PacketType.Unknown;
            player = null;
        }

        public virtual byte[] StartSerialization()
        {
            ms = new MemoryStream();
            bw = new BinaryWriter(ms);

            bw.Write((int)Type);
            bw.Write(player.ID);
            bw.Write(player.Name);

            return null;
        }

        public virtual BasePacket StartDeserialization(byte[] buffer)
        {
            ms = new MemoryStream(buffer);
            br = new BinaryReader(ms);

            Type = (PacketType)br.ReadInt32();
            player = new Player(br.ReadString(), br.ReadString());

            return this;
        }
    }
}
