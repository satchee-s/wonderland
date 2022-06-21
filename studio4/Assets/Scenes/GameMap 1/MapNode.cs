
using UnityEngine;
using UnityEngine.UI;
using System;



namespace GameMap
{

    public enum NodeStates
    {
        Locked,
        Visited,
        Attainable
    }

    public class MapNode : MonoBehaviour
    {
       public SpriteRenderer spriteRenderer;
       public SpriteRenderer visited;
       public Image visitedImage;

       public Node node { get; private set; }
       public NodeBlueprint nodeBlueprint;

        private float initialScale;
        private const float MaxClickDuration = 0.5f;
        private float mouseDownTime;
        private const float HoverScale = 1.2f;

        public void SetUp(Node n, NodeBlueprint bluePrint)
        {
            node = n;
            nodeBlueprint = bluePrint;
            spriteRenderer.sprite = bluePrint.sprite;
            if (n.nodeType == NodeType.Battle) transform.localScale *= 1.5f;
            initialScale = spriteRenderer.transform.localScale.x;
            visited.color = MapView.Instance.visitedColor;
            visited.gameObject.SetActive(false);
            SetState(NodeStates.Locked);
        }

        public void SetState(NodeStates state)
        {
            visited.gameObject.SetActive(false);
            switch (state)
            {
                case NodeStates.Locked:
                    //to do  switch color
                    spriteRenderer.color = MapView.Instance.lockedColor;
                    break;

                case NodeStates.Visited:
                    //to do  switch color
                    spriteRenderer.color = MapView.Instance.visitedColor;
                    visited.gameObject.SetActive(true);
                    break;
                case NodeStates.Attainable:
                    spriteRenderer.color = MapView.Instance.lockedColor;
                    //to do  switch color
                    
                    break;

                    default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);

            }
        }

        private void OnMouseEnter()
        {
           //set visuals
        }

        private void OnMouseExit()
        {
            //set visuals
        }

        private void OnMouseDown()
        {
            mouseDownTime = Time.time;
        }

        private void OnMouseUp()
        {
            if(Time.time - mouseDownTime < MaxClickDuration)
            {
                // player clicked on this node
                MapPlayerTracker.Instance.SelectNode(this);
            }
        }

        public void ShowAnimation()
        {
            if (visitedImage == null)
                return;

       
        }

    }

}
