using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public  class InformationPacket : BasePacket
    {
        public InformationPacket()
        {

        }

        public InformationPacket(Player player) : base(PacketType.Information, player)
        {
           this.player = player;
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();
            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            return this;
        }
    }
}

