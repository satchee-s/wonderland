using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBooster : MonoBehaviour
{
    public List<Card> opponentCreatureCards;
    public List<Card> playerCreatureCards;
    public Card chosenCard;

    public void SelectRandomCard()
    {
        if (opponentCreatureCards.Count > 0 && playerCreatureCards.Count > 0)
        {
            playerCreatureCards.Remove(chosenCard);
            Card swappedCard = opponentCreatureCards[Random.Range(0, opponentCreatureCards.Count)];
            opponentCreatureCards.Remove(swappedCard);
            playerCreatureCards.Add(swappedCard);
            opponentCreatureCards.Add(chosenCard);
            Debug.Log("Card swapped");
            gameObject.SetActive(false);
        }
    }
}
