
using UnityEngine;

namespace core
{
    public class RotationAndPositonPacket : BasePacket
    {
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }

        public RotationAndPositonPacket(Player player) : base(PacketType.RotationAndPosition , player)
        {
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
        }

        public RotationAndPositonPacket(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public byte[] StarStartSerialization()
        {
            base.StartSerialization();

            bw.Write(Position.x);
            bw.Write(Position.y);
            bw.Write(Position.z);

            bw.Write(Rotation.x);
            bw.Write(Rotation.y);
            bw.Write(Rotation.z);
            bw.Write(Rotation.w);

            return ms.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            Rotation = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

            return this;
        }
    }
}