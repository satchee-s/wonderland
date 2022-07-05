using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Camera cam;
    float xDist;

    private void Start()
    {
        cam = Camera.main;
        xDist = cam.ScreenToWorldPoint(transform.position).x;
    }

    private void OnMouseDrag()
    {
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.WorldToScreenPoint(transform.position).z);
        Vector3 newPos = cam.ScreenToWorldPoint(screenPos);
        transform.position = new Vector3(newPos.x, 1f, newPos.z);
    }
}
