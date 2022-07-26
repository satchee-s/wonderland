using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Camera cam;
    public float yDistFromTable;
    public bool canBeMoved = true;
    Card card;

    private void Start()
    {
        cam = Camera.main;
        card = GetComponent<Card>();
        yDistFromTable = transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (canBeMoved && gameObject.tag == "Selectable")
        {
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z);
            Vector3 newPos = cam.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(newPos.x, yDistFromTable, newPos.z);
        }
    }

    private void OnMouseUp()
    {
        if (canBeMoved && gameObject.tag == "Selectable")
        {
            transform.position = card.originalPos;
        }
    }
}
