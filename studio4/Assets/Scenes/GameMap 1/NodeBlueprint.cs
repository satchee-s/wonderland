using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace GameMap
{
    public enum NodeType
    {
        Battle,
        PlayerBattle
    }

    [CreateAssetMenu]
    public class NodeBlueprint : ScriptableObject
    {
        public Sprite sprite;
        public NodeType nodetype;
    }
}