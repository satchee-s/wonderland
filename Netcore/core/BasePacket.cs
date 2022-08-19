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
        protected MemoryStream msr;
        protected MemoryStream msw;
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
            Lobby,
            Card,
            Room,
            Acknowledged,
            PlayerData,
            Connection,
            StartGame,
            SlotPacket
        }

      public PacketType Type { get; set; }
      public Player player { get;  set; }

        public BasePacket(PacketType type, Player player )
        {
            this.player = player;
            Type = type;
        } 
        
        public BasePacket(PacketType type )
        {
            Type = type;
        }

        public BasePacket()
        {
            Type = PacketType.Unknown;
            player = null;
        }

        public virtual byte[] StartSerialization()
        {
            msw = new MemoryStream();
            bw = new BinaryWriter(msw);

            bw.Write((int)Type);
            bw.Write(player.ID);
            bw.Write(player.Name);

            return null;
        }

        public virtual BasePacket StartDeserialization(byte[] buffer)
        {
            msr = new MemoryStream(buffer);
            br = new BinaryReader(msr);

            Type = (PacketType)br.ReadInt32();
            player = new Player(br.ReadString(), br.ReadString());

            return this;
        }
    }
}
