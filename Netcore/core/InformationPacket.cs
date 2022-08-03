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
      
        }

        public byte[] StarStartSerialization()
        {
            base.StartSerialization();


            return ms.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
    
            return this;
        }
    }
}

