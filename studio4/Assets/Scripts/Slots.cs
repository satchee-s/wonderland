using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    Vector3 slotPosition;
    bool hasCard = false;

    private void Start()
    {
        slotPosition = new Vector3 (transform.position.x, transform.position.y + 0.1f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Selectable"))
        {
            if (!hasCard)
            {
                other.gameObject.GetComponent<Card>().originalPos = slotPosition;
                other.GetComponent<MouseMovement>().canBeMoved = false;
                other.gameObject.transform.position = slotPosition;
                hasCard = true;
            }
        }
    }
}
