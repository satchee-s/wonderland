using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionBooster : CreatureManager
{
    public override void PlayCard(Card otherCard, PlayerSlotsManager slot)
    {
        otherCard.health -= 3;
        base.PlayCard(otherCard, slot);
    }
}
