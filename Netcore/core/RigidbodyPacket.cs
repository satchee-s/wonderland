using UnityEngine;

namespace core
{
    public class RigidbodyPacket : BasePacket
    {

        public string GameObjectId { get; private set; }
        public Vector3 Velocity { get; private set; }

        public RigidbodyPacket()
        {
            GameObjectId = "";
            Vector3 velocity = Vector3.zero;
        }

        public RigidbodyPacket(Player player, string gameObjectId, Vector3 velocity): base(PacketType.Rigidbody, player)
        {
            GameObjectId = gameObjectId;
            Velocity = velocity;


        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();

            bw.Write(GameObjectId);
            bw.Write(Velocity.x);
            bw.Write(Velocity.y);
            bw.Write(Velocity.z);

            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            GameObjectId = br.ReadString();
            Velocity = new Vector3(br.ReadSingle() , br.ReadSingle(), br.ReadSingle());
            

            return this;
        }
    }
}
