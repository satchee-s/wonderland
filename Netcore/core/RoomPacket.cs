using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class RoomPacket : BasePacket
    {
       

        public override byte[] StartSerialization()
        {
            base.StartSerialization();


            return ms.GetBuffer();
        }
    }
}
