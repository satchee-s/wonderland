using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameMap
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        public List<NodeBlueprint> nodeBlueprints;
        public int GridWidth => Mathf.Max(numOfPrePlayerBattleNodes.max, numOfStartingNodes.max);

        public IntMinMax numOfPrePlayerBattleNodes;

        public IntMinMax numOfStartingNodes;

        public ListOfMapLayers layers;

        [System.Serializable]
        public class ListOfMapLayers
        {

        }
    }
}