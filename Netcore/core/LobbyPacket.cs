using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public class LobbyPacket : BasePacket
    {
        public List<string> clientsName;
        public LobbyPacket()
        {
            
        }

        public LobbyPacket(List<string> clientNames, Player player) : base(PacketType.Lobby, player)
        {
            this.clientsName = clientNames;
        }

        public override byte[] StartSerialization()
        {
            base.StartSerialization();

            bw.Write(clientsName.Count);  //sereialize list count 

            for (int i = 0; i < clientsName.Count; i++)//loop through the list and serialize the list of strings (All the elements)
            {
                bw.Write(clientsName[i]);
            }

            return msw.GetBuffer();
        }
        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            int count = br.ReadInt32();

            clientsName = new List<string>(count);

            for (int i = 0; i < count; i++) 
            {
                clientsName.Add(br.ReadString());
            }
          
            return this;
        }
       
    }
}
