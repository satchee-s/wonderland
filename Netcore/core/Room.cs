using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public  class Room : BasePacket
    {

        public List<Client> clientList;
        public int roomID;
        public Room()
        {

        }

        public Room(List<Client> clients, int roomID, Player player) : base(PacketType.Room, player)

        {
            clientList = clients;
            this.roomID = roomID;
            //this.player = player;
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
