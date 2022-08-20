using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class CardPacket : BasePacket
    {
      public int cardID { get; private set; }
      public string cardName { get; private set; }
      public int cardHealth { get; private set; }
      public int cardAttack { get; private set; }
      public bool sleep { get; private set; }

        public CardPacket()
        {
            cardID = 0;
            cardAttack = 0;
            cardHealth = 0;
            sleep = false;
            cardName = "";
        }

        public CardPacket(int cardID, string cardName, int cardHealth, int cardAttack, bool sleep) : base(PacketType.Card)
        {
            this.cardID = cardID;
            this.cardName = cardName;
            this.cardHealth = cardHealth;
            this.cardAttack = cardAttack;
            this.sleep = sleep;

        }


        public override byte[] StartSerialization()
        {
            base.StartSerialization();

           bw.Write(cardID);
           bw.Write(cardAttack);
           bw.Write(cardHealth);
           bw.Write(sleep);
           bw.Write(cardName);

            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            cardID = br.ReadInt32();
            cardAttack = br.ReadInt32();
            cardHealth = br.ReadInt32();
            cardName = br.ReadString();
            sleep = br.ReadBoolean();

            return this;
        }
    }
}
