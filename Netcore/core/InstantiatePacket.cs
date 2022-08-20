
using UnityEngine;

namespace core
{
    public class InstantiatePacket : BasePacket
    {
        //public string GameObjectId { get;private set; }
        public string PrefabName { get;private set; }

        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }

        public InstantiatePacket() 
        {
            //GameObjectId = "";
            PrefabName = "";
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
        }

        public InstantiatePacket(string prefabName, Vector3 position, Quaternion rotation) : base(PacketType.Instantiate)
        {
            //GameObjectId = gameObjectId;
            PrefabName = prefabName;
            Position = position;
            Rotation = rotation;
        }

       

        public override byte[] StartSerialization()
        {
            base.StartSerialization();

            //bw.Write(GameObjectId);
            bw.Write(PrefabName);

            bw.Write(Position.x);
            bw.Write(Position.y);
            bw.Write(Position.z);

            bw.Write(Rotation.x);
            bw.Write(Rotation.y);
            bw.Write(Rotation.z);
            bw.Write(Rotation.w);

            return msw.GetBuffer();
        }

        public override BasePacket StartDeserialization(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            //GameObjectId = br.ReadString();
            PrefabName = br.ReadString();

            Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            Rotation = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());

            return this;
        }
    }
}
