using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationBooster : MonoBehaviour
{
    Card card;

    private void Start()
    {
        card = GetComponent<Card>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (card.hasBeenPlayed)
        {
            if (other.CompareTag("CreatureCard"))
            {
                other.GetComponent<Card>().health += 3;
            }
        }
    }
}
