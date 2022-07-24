
using UnityEngine;

namespace GameMap
{
    [System.Serializable]
    public class LineConnection
    {
        public LineRenderer lineRenderer;
        public MapNode from;
        public MapNode to;

        public LineConnection(LineRenderer lineRenderer, MapNode from, MapNode to)
        {
            this.lineRenderer = lineRenderer;
            this.from = from;
            this.to = to;
        }

        public void SetColor(Color color)
        {
            Gradient gradient = lineRenderer.colorGradient;
            GradientColorKey[] colorKeys = gradient.colorKeys;
            for (int j = 0; j < colorKeys.Length; j++)
            {
                colorKeys[j].color = color;
            }

            gradient.colorKeys = colorKeys;
            lineRenderer.colorGradient = gradient;
        }
    }
}