
using UnityEngine;

namespace core
{
    public class Player
    {
        public string ID { get; private set; }
        public string Name { get; private set; }


       public Player(string Id, string name)
       {
           ID = Id;
            Name = name;
       }
    }

}
