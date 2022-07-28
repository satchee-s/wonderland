using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullsChoice : MonoBehaviour
{
    public List<Card> opponentCreatureCards;

    public void RemoveCard()
    {
        if (opponentCreatureCards.Count > 0)
        {
            Card removedCard = opponentCreatureCards[(Random.Range(0, opponentCreatureCards.Count))];
            opponentCreatureCards.Remove(removedCard);
            Debug.Log("Card removed");
            removedCard.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}