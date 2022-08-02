using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class LobbyPacket : BasePacket
    {
        List<string> clientsName = new List<string>();
        public LobbyPacket()
        {
            
        }

        public LobbyPacket(Player player) : base(PacketType.Lobby, player)
        {

        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();

            bw.Write(clientsName.Count);  //sereialize list count 
           
            //loop through the list and serialize the list of strings (All the elements)

            return ms.GetBuffer();
        }
        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);


          //clientsName.Count = br.ReadString();
            return this;
        }
       
    }
}
