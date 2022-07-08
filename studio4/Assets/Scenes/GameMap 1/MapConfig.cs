using System.Collections.Generic;
using Malee;
using UnityEngine;
using OneLine;



namespace GameMap
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        public List<NodeBlueprint> nodeBlueprints;
        public int GridWidth => Mathf.Max(numOfPrePlayerBattleNodes.max, numOfStartingNodes.max);
        
        [OneLineWithHeader]
        public IntMinMax numOfPrePlayerBattleNodes;
        [OneLineWithHeader]
        public IntMinMax numOfStartingNodes;
        [Reorderable]
        public ListOfMapLayers layers;

        [System.Serializable]
        public class ListOfMapLayers : ReorderableArray<MapLayer>
        {

        }
    }
}