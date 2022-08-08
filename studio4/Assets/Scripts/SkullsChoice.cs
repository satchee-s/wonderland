using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullsChoice : CreatureManager
{
    public override void PlayCard(Card otherCard, PlayerSlotsManager slot)
    {
        if (opponentCreatureCards.Count > 0)
        {
            Card removedCard = opponentCreatureCards[(Random.Range(0, opponentCreatureCards.Count))];
            opponentCreatureCards.Remove(removedCard);
            Debug.Log("Card removed");
            removedCard.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        base.PlayCard(otherCard, slot);
    }
}
