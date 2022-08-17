using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class StartGamePacket : BasePacket 
    {
       
        public StartGamePacket()
        {
            
        }

        public StartGamePacket( Player player) : base(PacketType.StartGame, player)
        {
            
            
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