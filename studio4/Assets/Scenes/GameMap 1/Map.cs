using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameMap
{
    public class Map
    {
        public List<Node> nodes;
        public List<Point> path;
        public string PlayerBattleNodeName;
        public string configName;

        public Map(string configName,string PlayerBattleNodeName, List<Node> nodes, List<Point> path)
        {
            this.configName = configName;
            this.PlayerBattleNodeName = PlayerBattleNodeName;
            this.nodes = nodes;
            this.path = path;
        }

        public Node GetPlayerBattle()
        {
            return nodes.FirstOrDefault(n => n.nodeType == NodeType.PlayerBattle);
        }

        public float DistanceBetweenFirstAndLastLayers()
        {
            Node BattleNode = GetPlayerBattle();
            Node firstLayerNode = nodes.FirstOrDefault(n => n.point.y == 0);

            if (BattleNode == null || firstLayerNode == null) return 0f;

            return BattleNode.pos.y - firstLayerNode.pos.y;
        }

        public Node GetNode(Point point)
        {
            return nodes.FirstOrDefault(n => n.point.Equals(point));
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}