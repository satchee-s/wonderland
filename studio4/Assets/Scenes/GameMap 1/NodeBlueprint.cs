using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace GameMap
{
    public enum NodeType
    {
        Battle,
        PlayerBattle,
        Mystery,
        Shop
    }

    [CreateAssetMenu]
    public class NodeBlueprint : ScriptableObject
    {
        public Sprite sprite;
        public NodeType nodetype;
    }
}