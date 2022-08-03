using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBooster : CreatureManager
{
    public override void PlayCard(Card otherCard, Slots slot)
    {
        if (opponentCreatureCards.Count > 0 && playerCreatureCards.Count > 0)
        {
            playerCreatureCards.Remove(otherCard);
            Card swappedCard = opponentCreatureCards[Random.Range(0, opponentCreatureCards.Count)];
            opponentCreatureCards.Remove(swappedCard);
            playerCreatureCards.Add(swappedCard);
            opponentCreatureCards.Add(otherCard);
            Debug.Log("Card swapped");
            gameObject.SetActive(false);
        }
        base.PlayCard(otherCard, slot);
    }
}
