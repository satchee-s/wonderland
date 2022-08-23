
using UnityEngine;

namespace core
{
    public class Player
    {
        public string ID { get;  set; }
        public string Name { get;  set; }
        public int PlayerNumber;


       public Player(string Id, string name, int playerNumber)
       {
           ID = Id;
           Name = name;
           PlayerNumber = playerNumber;
       }
    }

}
