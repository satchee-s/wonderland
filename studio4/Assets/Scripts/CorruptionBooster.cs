using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionBooster : BoosterManager
{
    public override void PlayCard(Card otherCard)
    {
        otherCard.health -= 3;
        base.PlayCard(otherCard);
    }
}
