using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMap
{

    public class MapView : MonoBehaviour
    {
        public enum MapOrientation
        {
            BottomToTop,
            TopToBottom,
            RightToLeft,
            LeftToRight
        }

        public MapManager mapManager;
        public MapOrientation mapOrientation;

        [Tooltip("List of all the MapConfig scriptable objects from the " +
            "Assets folder that might be used to construct the maps. define general layout")]

       public List<MapConfig> allMapConfigs;
       public GameObject nodePrefab;

        [Tooltip("Offsets the start of the end nodes of the map form the edges of the screen")]

       public float orientationOffset;

        [Header("Background Settings")]
        [Tooltip("If the background sprite is null, the background will not be shown")]

       public Sprite background;
       public Color32 backgroundColor = Color.white;
       public float xSize;
       public float yOffset;

        [Header("Line Settings")]

       public GameObject linePrefab;

        [Tooltip("Line point count should be greater than 2 to get smooth color gradients")]

        [Range(3, 10)]

       public int linePointCount = 10;

        [Tooltip("The distance from the node till the Line starting point")]

       public float offsetFromNodes = 0.5f;

        [Header("Colors")]
        [Tooltip("Node Visited or Attainable color")]

       public Color32 visitedColor = Color.grey;

        [Tooltip("Locked node color")]

       public Color32 lockedColor = Color.white;

        [Tooltip("Node Visited or Attainable color")]

       public Color32 linevisitedColor = Color.grey;

        [Tooltip("Unavailable path color")]
       
       public Color32 lineLockedColor = Color.white;


       private GameObject firstParent;
       private GameObject mapParent;
       private List<List<Point>> paths;
       private Camera Cam;

       public readonly List<MapNode> mapNodes = new List<MapNode>();
       private readonly List<LineConnection> lineConnections = new List<LineConnection>();

       public static MapView Instance;

        private void Awake()
        {
            Instance = this;
            Cam = Camera.main;
        }

       private void ClearMap()
       {
            if(firstParent != null)
            {
                Destroy(firstParent);

                mapNodes.Clear();
                lineConnections.Clear();
            }
       }

       public void ShowMap(Map m)
       {
            if(m == null)
            {
                Debug.LogWarning("Map was null in MapView.ShowMap()");
                return;

                ClearMap();

                CreateMapParent();

                CreateNodes(m.nodes);

                DrawLines();

                SetOrientation();

                ResetNodesRotation();

                SetAttainableNodes();

                SetLineColors();

                CreateMapBackground(m);
            }

       }

       private void CreateMapBackground(Map m)
       {

            if (background == null) 
                return;

            GameObject backgroundObject = new GameObject("Background");
            backgroundObject.transform.SetParent(mapParent.transform);

            MapNode playerBattleNode = mapNodes.FirstOrDefault(n => n.node.nodeType == NodeType.PlayerBattle);

            float span = m.DistanceBetweenFirstAndLastLayers();

            backgroundObject.transform.localPosition = new Vector3(playerBattleNode.transform.localPosition.x, span / 2f, 0f);
            backgroundObject.transform.localRotation = Quaternion.identity;

            SpriteRenderer sr = backgroundObject.AddComponent<SpriteRenderer>();

            sr.color = backgroundColor;
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.sprite = background;
            sr.size = new Vector2(xSize, span + yOffset * 2f);

       }


       private void CreateMapParent()
       {
            firstParent = new GameObject("OuterMapParent");
            mapParent = new GameObject("MapParentWithAScroll");
            mapParent.transform.SetParent(firstParent.transform);

            ScrollNonUI scrollNonUi = mapParent.AddComponent<ScrollNonUI>();
            scrollNonUi.freezeX = orientation == MapOrientation.BottomToTop || orientation == MapOrientation.TopToBottom;
            scrollNonUi.freezeY = orientation == MapOrientation.LeftToRight || orientation == MapOrientation.RightToLeft;

            BoxCollider box = mapParent.AddComponent<BoxCollider>();
            box.size = new Vector3(100, 100, 1);


        }

        private void CreateNodes(IEnumerable<Node> nodes)
        {
            foreach(Node node in nodes)
            {
                var mapNode = CreateMapNode(node);
                mapNodes.Add(mapNode);

            }
        }

        private MapNode CreateMapNode(Node node)
        {
            GameObject mapNodeObject = Instantiate(nodePrefab, mapParent.transform);
            MapNode mapNode = mapNodeObject.AddComponent<MapNode>();
            NodeBlueprint blueprint = GetBlueprint(node.bluePrintName);

            mapNode.SetUp(node, blueprint);
            mapNode.transform.localPosition = node.pos;
            return mapNode;
        }

        public void SetAttainableNodes()
        {
            //first we set all the nodes as unattainable/locked
            foreach(MapNode node in mapNodes)
            {
                node.SetState(NodeStates.Locked);
            }

            if(mapManager.CurrentMap.path.Count == 0)
            {
               // we have not started traveling on this map yet, set entire first layer as attainable
              foreach(MapNode node in mapNodes.Where(n => n.node.point.y == 0))
              {
                node.SetState(NodeStates.Attainable);
              }
            }
            else
            {
                // we already started moving on the current map, first  highlight the path as visited
                foreach(Point point in mapManager.CurrentMap.path)
                {
                    MapNode mapNode = GetNode(point);
                    if(mapNode != null) mapNode.SetState(NodeStates.Visited);
                }

                Point currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                // set all the nodes that we can travel to as attainable
                foreach(Point point in currentNode.outgoing)
                {
                    MapNode mapNode = GetNode(point);
                    if (mapNode != null) mapNode.SetState(NodeStates.Attainable);
                }
            }
        }

        public void SetLineColors()
        {

            foreach (var connection in lineConnections) connection.SetColor(lineLockedColor);

            // we set all the lines that are part of the path to visited color
            // if we have not started moving on the map yet then leave everything as is
            if (mapManager.CurrentMap.path.Count == 0) return;

            // we mark outgoing connections from the final node with visible/attainable color
            Point currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];

            Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);

            foreach(Point point in currentNode.outgoing)
            {
                var lineConnection = lineConnections.FirstOrDefault(conn => conn.from.Node == currentNode && conn.to.Node.point.Equals(point));
                lineConnection.SetColor(linevisitedColor);
            }

            if(mapManager.CurrentMap.path.Count <= 1) return;

            for (int i = 0; i < mapManager.CurrentMap.path.Count - 1; i++)
            {
                var current = mapManager.CurrentMap.path[i];
                var next = mapManager.CurrentMap.path[i + 1];
                var lineConnection = lineConnections.FirstOrDefault( conn => conn.@from.node.point.Equals(current) && conn.to.Node.point.Equals(next));

                lineConnection?.SetColor(linevisitedColor);
            }


        }

        private void SetOrientation()
        {

        }


        private void DrawLines()
        {
            foreach(MapNode node in mapNodes)
            {
                foreach (Point connection in node.node.outgoing) AddLineConnection(node, GetNode(connection));
            }
        }

        public void AddLineConnection(MapNode from, MapNode to)
        {
            GameObject lineObj = Instantiate(linePrefab, mapParent.transform);
            LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
            Vector3 fromPoint = from.transform.position + (to.transform.position - from.transform.position).normalized * offsetFromNodes;
            Vector3 toPoint = to.transform.position + (from.transform.position - to.transform.position).normalized * offsetFromNodes;

            // drawing lines in local space
            lineObj.transform.position = fromPoint;
            lineRenderer.useWorldSpace = false;

            // line renderer with 2 points
            lineRenderer.positionCount = linePointCount;

            for(int i = 0; i < linePointCount; i++)
            {
                lineRenderer.SetPosition(i,Vector3.Lerp(Vector3.zero, toPoint - fromPoint, linePointCount - 1));
            }

            DottedLineRenderer dottedLine = lineObj.GetComponent<DottedLineRenderer>();
            if(dottedLine != null) dottedLine.ScaleMaterial();

            lineConnections.Add(new LineConnection(lineRenderer, from, to));
        }


        private MapNode GetNode(Point p)
        {
            return mapNodes.FirstOrDefault(n => n.node.point.Equals(p));
        }

        private MapConfig GetConfig(string configName)
        {
            return allMapConfigs.FirstOrDefault(c => c.name == configName);
        }

        public NodeBlueprint GetBlueprint(NodeType type)
        {
            MapConfig config = GetConfig(mapManager.CurrentMap.configName);
            return config.nodeBlueprints.FirstOrDefault(n => n.nodetype == type);
        }

        public NodeBlueprint GetBlueprint(string blueprintName)
        {
          MapConfig config = GetConfig(mapManager.CurrentMap.configName);
            return config.nodeBlueprints.FirstOrDefault(n => n.name == blueprintName);
        }



    }

}