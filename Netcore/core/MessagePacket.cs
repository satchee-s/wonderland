

namespace core
{
    public class MessagePacket : BasePacket
    {
            public string message { get;private set; }

        public MessagePacket(Player player, string message) : base(PacketType.Message, player)
        {
            this.message = message;
            
        }

        public MessagePacket()
        {
            message = "";
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();
            bw.Write(message);

            return ms.ToArray();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            message = br.ReadString();

            return this;
        }
    }
}
