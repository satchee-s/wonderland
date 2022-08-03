using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypnosis : CreatureManager
{
    public override void PlayCard(Card otherCard, Slots slot)
    {
        if (opponentCreatureCards.Count > 0)
        {
            //networking stuff
            //on opponent's next turn disable all actions except attacking slot in slot.creatureCardSlot
            base.PlayCard(otherCard, slot);
        }
    }
}
