﻿using UnityEngine;

namespace core
{
    public class DestroyPacket : BasePacket
    {

        public string GameObjectId { get; private set; }

        public DestroyPacket() 
        {
            GameObjectId = "";
        }

        public DestroyPacket(string gameObjectId , Player player) : base(PacketType.Destroy, player)
        {
            GameObjectId = gameObjectId;
        }

        public byte[] StarStartSerialization()
        {
            base.StartSerialization();

            bw.Write(GameObjectId);

            return ms.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            GameObjectId = br.ReadString();

            return this;
        }
    }
}