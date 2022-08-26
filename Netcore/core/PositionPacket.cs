using UnityEngine;

namespace core
{
    public class PositionPacket : BasePacket
    {
        public Vector3 Position { get; private set; }
        public int Id { get; private set; }

        public PositionPacket()
        {
            Position = Vector3.zero;
            Id = 0;
        }

        public PositionPacket(Vector3 position, int id, Player player) : base(PacketType.Position, player)
        {
            Position = position;
            Id = id;
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();

            bw.Write(Position.x);
            bw.Write(Position.y);
            bw.Write(Position.z);
            bw.Write(Id);

            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            Id = br.ReadInt32();
            return this;
        }
    }
}