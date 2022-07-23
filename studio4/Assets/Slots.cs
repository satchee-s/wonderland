using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    bool cardInSlot = false;
    GameObject currentCard;

    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    int currentCardPos = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (!cardInSlot)
        {
            if (other.CompareTag("Card"))
            {
                /*Debug.Log("card detected");
                other.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y +0.5f, transform.position.z);
                cardInSlot = true;
                currentCard = other.gameObject;*/

                if (currentCardPos < 4)
                {
                    Debug.Log("Card added to hand");
                    //other.gameObject.SetActive(true);
                    other.transform.position = cardSlots[currentCardPos].transform.position;
                    other.gameObject.GetComponent<MouseMovement>().canBeMoved = false;
                    availableCardSlots[currentCardPos] = false;
                    currentCardPos++;
                }
            }
        }
    }


    /*private void OnMouseEnter()
    {
        Debug.Log("Object clicked");
        if (cardInSlot)
        {
            currentCard = null;
            cardInSlot = false;
        }
    }*/

}
