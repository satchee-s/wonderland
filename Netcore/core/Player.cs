
using UnityEngine;

namespace core
{
    public class Player
    {
        public string ID { get;  set; }
        public string Name { get;  set; }


       public Player(string Id, string name)
       {
           ID = Id;
            Name = name;
       }
    }

}
