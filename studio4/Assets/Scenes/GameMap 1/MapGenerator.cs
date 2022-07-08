using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameMap
{ 

   public static class MapGenerator
   {

        private static MapConfig config;
        private static readonly List<NodeType> RandomNodes = new List<NodeType>
        {NodeType.Mystery, NodeType.Battle , NodeType.PlayerBattle , NodeType.Shop };

        private static List<float> layerDistances;
        private static List<List<Point>> paths;

        private static readonly List<List<Node>> nodes = new List<List<Node>>();

        public static Map GetMap(MapConfig conf)
        {
            if (conf == null)
            {
                Debug.LogWarning("Config was null in MapGenerator.Generate()");
                return null;
            }

            config = conf;
            nodes.Clear();

            GenerateLayerDistances();

            for (int i = 0; i < conf.layers.Count; i++) PlaceLayer(i);

            GeneratePaths();

            RandomizeNodePositions();

            SetUpConnections();

            RemoveCrossConnections();

            // select all the nodes with connections
            List<Node> nodesList = nodes.SelectMany(n => n).Where(n => n.incoming.Count > 0 || n.outgoing.Count > 0).ToList();

            string PlayerBattleName = config.nodeBlueprints.Where(b => b.nodetype == NodeType.PlayerBattle).ToList().Random().name;
            return new Map(conf.name, PlayerBattleName, nodesList, new List<Point>());


        }


        private static void GenerateLayerDistances()
        {
            layerDistances = new List<float>();

            foreach (MapLayer layer in config.layers) layerDistances.Add(layer.distanceFromPreviousLayer.GetValue());
        }

        private static float GetDistanceToLayer(int layerIndex)
        {
            if (layerIndex < 0 || layerIndex > layerDistances.Count) return 0f;

            return layerDistances.Take(layerIndex + 1).Sum();
        }

        private static void PlaceLayer(int layerIndex)
        {
            MapLayer layer = config.layers[layerIndex];
            List<Node> nodesOnThisLayer = new List<Node>();

            var offset = layer.nodesApartDistance * config.GridWidth / 2f;

            for (int i = 0; i < config.GridWidth; i++)
            {
                NodeType nodeType = Random.Range(0f, 1f) < layer.randomizeNodes ? GetRandomNode() : layer.nodeType;
                string blueprintName = config.nodeBlueprints.Where(b => b.nodetype == nodeType).ToList().Random().name;
                Node node = new Node(nodeType, blueprintName, new Point(i, layerIndex))
                {
                    pos = new Vector2(-offset + i * layer.nodesApartDistance, GetDistanceToLayer(layerIndex))
                };

                nodesOnThisLayer.Add(node);

            }
            nodes.Add(nodesOnThisLayer);
        }

        private static void RandomizeNodePositions()
        {
            for (int index = 0; index < nodes.Count; index++)
            {
                List<Node> list = nodes[index];
                MapLayer layer = config.layers[index];
                var distToNextLayer = index + 1 >= layerDistances.Count ? 0f : layerDistances[index + 1];

                var distToPreviousLayer = layerDistances[index];

                foreach (Node node in list)
                {
                    var xRnd = Random.Range(-1f, 1f);
                    var yRnd = Random.Range(-1f, 1f);

                    var x = xRnd * layer.nodesApartDistance / 2f;
                    var y = yRnd < 0 ? distToPreviousLayer * yRnd / 2f : distToNextLayer * yRnd / 2f;

                    node.pos = new Vector2(x, y) * layer.randomizePosition;
                }
            }

        }

        private static void SetUpConnections()
        {
            foreach (List<Point> path in paths)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    Node node = GetNode(path[i]);

                    if (i > 0)
                    {
                        // previous because the path is flipped
                        Node nextNode = GetNode(path[i - 1]);
                        nextNode.AddIncoming(node.point);
                        node.AddOutgoing(nextNode.point);
                    }

                    if (i < path.Count - 1)
                    {
                        Node previousNode = GetNode(path[i + 1]);
                        previousNode.AddOutgoing(node.point);
                        node.AddIncoming(previousNode.point);
                    }
                }
            }
        }

        private static void RemoveCrossConnections()
        {
            for (int i = 0; i < config.GridWidth - 1; i++)
            {
                for (int j = 0; j < config.layers.Count - 1; j++)
                {
                    Node node = GetNode(new Point(i, j));
                    if (node == null || node.HasNoConnections()) continue;

                    Node right = GetNode(new Point(i, j + 1));
                    if (right == null || right.HasNoConnections()) continue;

                    Node top = GetNode(new Point(i + 1, j + 1));
                    if (top == null || top.HasNoConnections()) continue;

                    Node topRight = GetNode(new Point(i + 1, j + 1));
                    if (topRight == null || topRight.HasNoConnections()) continue;


                    if (!node.outgoing.Any(element => element.Equals(topRight.point))) continue;
                    if (!right.outgoing.Any(element => element.Equals(top.point))) continue;

                    // we managed to find a cross node
                    node.AddOutgoing(top.point);
                    top.AddIncoming(node.point);

                    right.AddOutgoing(topRight.point);
                    topRight.AddIncoming(right.point);

                    var rnd = Random.Range(0f, 1f);
                    if (rnd == 0.2f)
                    {
                        // remove both cross connections
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);

                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
                    else if (rnd < 0.6f)
                    {
                        //a
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);
                    }
                    else
                    {
                        //b
                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
                }
            }
        }

        private static Node GetNode(Point p)
        {
            if (p.y >= nodes.Count) return null;
            if (p.x >= nodes.Count) return null;

            return nodes[p.y][p.x];
        }

        private static Point GetFinalNode()
        {
            int y = config.layers.Count - 1;
            if (config.GridWidth % 2 == 1) return new Point(config.GridWidth / 2, y);

            return Random.Range(0, 2) == 0
                 ? new Point(config.GridWidth / 2, y)
                : new Point(config.GridWidth / 2 - 1, y);
        }


        private static void GeneratePaths()
        {
            Point finalNode = GetFinalNode();
            paths = new List<List<Point>>();
            int numberOfStartingNodes = config.numOfStartingNodes.GetValue();
            int numberOfPlayerBattleNodes = config.numOfPrePlayerBattleNodes.GetValue();

            List<int> candidateXs = new List<int>();
            for (int i = 0; i < config.GridWidth; i++) candidateXs.Add(i);

            candidateXs.Shuffle();
            IEnumerable<int> preXs = candidateXs.Take(numberOfPlayerBattleNodes);
            List<Point> preEndPoints = (from x in preXs select new Point(x, finalNode.y - 1)).ToList();
            int attempt = 0;

            foreach (Point point in preEndPoints)
            {
                List<Point> path = Path(point, 0, config.GridWidth);
                path.Insert(0, finalNode);
                paths.Add(path);
                attempt++;
            }

            while (!PathsLeadToAtLeastNDifferentPoints(paths, numberOfStartingNodes) && attempt < 100)
            {
                Point randomPreEndPoint = preEndPoints[Random.Range(0, preEndPoints.Count)];
                List<Point> path = Path(randomPreEndPoint, 0, config.GridWidth);
                path.Insert(0, finalNode);
                paths.Add(path);
                attempt++;
            }

            Debug.Log("Attempts to generate paths: " + attempt);
        }

        private static bool PathsLeadToAtLeastNDifferentPoints(IEnumerable<List<Point>> paths, int n)
        {
            return (from path in paths select path[path.Count - 1].x).Distinct().Count() >= n;
        }

        private static List<Point> Path(Point from, int toY, int width, bool firstStepUnconstrained = false)
        {
            if(from.y == toY)
            {
                Debug.LogError("Points are on same layers, return");
                return null;
            }

            // making one y step in this direction with each move
            int direction = from.y > toY ? -1 : 1;

            List<Point> path = new List<Point> {from };
            while (path[path.Count - 1].y != toY)
            {
                Point lastPoint = path[path.Count - 1];
                List<int> candidateXs = new List<int>();

                if(firstStepUnconstrained && lastPoint.Equals(from))
                {
                    for(int i = 0; i < width; i++) candidateXs.Add(i);
                }
                else
                {
                    // forward
                    candidateXs.Add(lastPoint.x);
                    // left
                    if (lastPoint.x - 1 >= 0) candidateXs.Add(lastPoint.x - 1);
                    // right
                    if (lastPoint.x + 1 < width) candidateXs.Add(lastPoint.x + 1);
                }

                Point nextPoint = new Point(candidateXs[Random.Range(0, candidateXs.Count)], lastPoint.y + direction);
                path.Add(nextPoint);
            }
            return path;
            
        }

        private static NodeType GetRandomNode()
        {
            return RandomNodes[Random.Range(0, RandomNodes.Count)];
        }




    }


}