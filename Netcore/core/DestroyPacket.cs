using UnityEngine;

namespace core
{
    public class DestroyPacket : BasePacket
    {
        public int GameObjectId { get; private set; }
        public DestroyPacket() 
        {
            GameObjectId = 0;
        }

        public DestroyPacket(int gameObjectId) : base(PacketType.Destroy)
        {
            GameObjectId = gameObjectId;
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();
            
            bw.Write(GameObjectId);
            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            return this;
        }
    }
}
