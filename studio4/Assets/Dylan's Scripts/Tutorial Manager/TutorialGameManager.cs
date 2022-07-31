using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGameManager : GameManager
{
    public List<Card> TutorialDeck = new List<Card>();

    new void Start()
    {
        for(int i = 0; i < 7; i++)
        {
            handCards.Add(TutorialDeck[Random.Range(0, TutorialDeck.Count)]);
            handCards[i].tag = "Selectable";
            TutorialDeck.Remove(handCards[i]);

            handCards[i].transform.position = cardSlots[i].transform.position;
        }
    }
}
