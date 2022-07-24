using UnityEngine;

namespace core
{
    public class RotationPacket : BasePacket
    {
        public Quaternion Rotation { get; private set; }

        public RotationPacket()
        {
            Rotation = Quaternion.identity;
        }

        public RotationPacket(Quaternion rotation, Player player) : base(PacketType.Rotation, player)
        {
            Rotation = rotation;
        }

        public byte[] StarStartSerialization()
        {
            base.StartSerialization();



            bw.Write(Rotation.x);
            bw.Write(Rotation.y);
            bw.Write(Rotation.z);
            bw.Write(Rotation.w);

            return ms.GetBuffer();
        }


        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            Rotation = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

            return this;
        }
    }
}
