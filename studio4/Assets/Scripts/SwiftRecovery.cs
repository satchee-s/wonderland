using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftRecovery : BoosterManager
{
    public override void PlayCard(Card otherCard)
    {
        if (playerCreatureCards.Contains(otherCard))
        {
            if (otherCard.isHibernating)
            {
                otherCard.isHibernating = false;
                base.PlayCard(otherCard);
            }
        }
        
    }
}
