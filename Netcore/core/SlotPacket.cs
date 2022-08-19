using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public  class SlotPacket : BasePacket
    {
        public int slotId { get; private set; }
        public int cardId { get; private set; }

        public SlotPacket()
        {
           
        }

        public SlotPacket(int slotId , int cardId, Player player) : base(PacketType.SlotPacket, player)
        {
            this.slotId = slotId;
            this.cardId = cardId;   
            this.player = player;
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();

            bw.Write(slotId);
            bw.Write(cardId);
         
            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            slotId = br.ReadInt32();
            cardId = br.ReadInt32();

            return this;
        }

    }
}
