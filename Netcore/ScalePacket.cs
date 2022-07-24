using UnityEngine;

namespace core
{
    public class ScalePacket : BasePacket
    {

        public Vector3 Scale { get; private set; }


        public ScalePacket()
        {
            Scale = Vector3.zero;

        }

        public ScalePacket(Vector3 position, Player player) : base(PacketType.Scale, player)
        {
            Scale = position;

        }

        public byte[] StarStartSerialization()
        {
            base.StartSerialization();

            bw.Write(Scale.x);
            bw.Write(Scale.y);
            bw.Write(Scale.z);

            return ms.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            Scale = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

            return this;
        }
    }
}