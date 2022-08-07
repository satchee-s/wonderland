using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftRecovery : CreatureManager
{
    public override void PlayCard(Card otherCard, SlotsManager slot)
    {
        if (playerCreatureCards.Contains(otherCard))
        {
            if (otherCard.isHibernating)
            {
                otherCard.isHibernating = false;
                base.PlayCard(otherCard, slot);
            }
        }
        
    }
}
