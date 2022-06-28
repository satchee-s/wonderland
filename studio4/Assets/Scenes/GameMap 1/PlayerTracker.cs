using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace GameMap
{
    public class PlayerTracker : MonoBehaviour 
    { 
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView mapView;
        public static PlayerTracker Instance;
        public bool Locked { get;  set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            if(mapManager.CurrentMap.path.Count == 0)
            {
                if (mapNode.node.point.y == 0) SendPLayerToNode(mapNode);
                else WarningNodeConnotBeAccessed();
            }
            else
            {
                Point currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.node.point))) SendPLayerToNode(mapNode);
                else WarningNodeConnotBeAccessed();




            }
        }

        private void SendPLayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.path.Add(mapNode.node.point);
            mapManager.SaveMap();
            mapView.SetAttainableNodes();
            mapView.SetLineColors();
            mapNode.ShowAnimation();
        }

        private static void EnterNode(MapNode mapNode)
        {
            switch (mapNode.node.nodeType)
            {
                case NodeType.Battle:
                    break;
                case NodeType.PlayerBattle:
                    break;
                    default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WarningNodeConnotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }

    }

}