﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class AcknowledgedPacket: BasePacket
    {
        public bool gameover { get;private set; }
        public int Health { get; private set; }
        public bool moveAccepted { get; private set; }

        public AcknowledgedPacket(Player player) : base(PacketType.Acknowledged, player)
        {
            gameover = false;
            Health = 0;
            moveAccepted = false;
            
            
        } 
        public AcknowledgedPacket() : base(PacketType.Acknowledged)
        {
            gameover = false;
            Health = 0;
            moveAccepted = false;
            
            
        }

        public AcknowledgedPacket(bool gameover, int health, bool moveAccepted, Player player): base(PacketType.Acknowledged, player)
        {
            this.gameover = gameover;
            Health = health;
            this.moveAccepted = moveAccepted;
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();
            bw.Write(gameover);
            bw.Write(Health);
            bw.Write(moveAccepted);
            
            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            gameover = br.ReadBoolean();
            Health = br.ReadInt32();
            moveAccepted = br.ReadBoolean();

            return this;
        }
    }
}
