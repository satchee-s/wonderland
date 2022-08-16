using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class PlayerDataPacket : BasePacket
    {
        public int Health { get; private set; }
        public int Mana { get; private set; }

        public PlayerDataPacket()
        {
            Health = 0;
            Mana = 0;
        }

        public PlayerDataPacket(Player player, int health, int mana) : base(PacketType.Rigidbody, player)
        {
            Health = health;
            Mana = mana;
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();
            bw.Write(Health);
            bw.Write(Mana);
            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            Health = br.ReadInt32();
            Mana = br.ReadInt32();
            return this;
        }
    }
}
