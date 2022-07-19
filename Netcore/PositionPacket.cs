using UnityEngine;

namespace core
{
    public class PositionPacket : BasePacket
    {

        public Vector3 Position { get; private set; }
      

        public PositionPacket(Player player) : base(PacketType.Position, player)
        {
            Position = Vector3.zero;
           
        }

        public PositionPacket(Vector3 position, Quaternion rotation)
        {
            Position = position;
            
        }

        public byte[] StarStartSerialization()
        {
            base.StartSerialization();

            bw.Write(Position.x);
            bw.Write(Position.y);
            bw.Write(Position.z);

            return ms.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

            return this;
        }
    }
}