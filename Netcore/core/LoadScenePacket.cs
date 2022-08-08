using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public  class LoadScenePacket : BasePacket
    {
        string loadScene;
        public LoadScenePacket()
        {

        }

        public LoadScenePacket(Player player, string loadScene) : base(PacketType.Information, player)
        {
            this.loadScene = loadScene;
            this.player = player;
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();


            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            return this;
        }


    }
}
