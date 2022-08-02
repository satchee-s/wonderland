using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class LobbyPacket : BasePacket
    {
        public List<string> clientsName = new List<string>();
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

            for (int i = 0; i < clientsName.Count; i++)//loop through the list and serialize the list of strings (All the elements)
            {
                bw.Write(clientsName[i]);
            }

            return ms.GetBuffer();
        }
        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            int count = br.ReadInt32();

            for (int i = 0; i < count; i++) 
            {
                clientsName.Add(br.ReadString());
            }
          
            return this;
        }
       
    }
}
