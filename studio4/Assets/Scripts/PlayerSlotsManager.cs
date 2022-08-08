using System;
using UnityEngine;

public class PlayerSlotsManager : MonoBehaviour
{
    /**
     THIS MANAGES MULTIPLE SLOTS.
     */
    //Vector3 slotPosition;
    // public bool hasCard = false;
    // public enum slotType { Creature, Booster };
    // bool canBePlaced = false; //this is an attribute for the card, not the slot
    //  bool hasCollidedWithSlot = false;
    //  Card cardBeingMoved;
    //  Card cardInSlot;
    //[SerializeField] Card.CardType cardType;
    public Slot[] allSlots = new Slot[8];
    private Card cardBeingMoved;

    private void OnEnable()
    {
        CardMouseInteraction.onDragEvent += GetCardBeingMoved;
    }
    private void OnDisable()
    {
        CardMouseInteraction.onDragEvent -= GetCardBeingMoved;
    }

    private void GetCardBeingMoved(CardMouseInteraction card)
    {
        cardBeingMoved = card.card;
        card.onRelease += HandleCardRelease;
    }


    private void Update()
    {
        if (cardBeingMoved != null)
        {
            Slot closestSlot = GetClosestToMovingCard(allSlots, cardBeingMoved.Type);
            if(closestSlot != null)
                PlaceCard(closestSlot);
        }
    }
    
    private void HandleCardRelease()
    {
        Slot closestSlot = GetClosestToMovingCard(allSlots, cardBeingMoved.Type);
        if(closestSlot == null) return;
        if(cardBeingMoved.cost <= PlayerTurnSystem.currentMana)
        {
        closestSlot.PlaceCardInSlot(cardBeingMoved);
        }
        cardBeingMoved.MouseInteraction.onRelease -= HandleCardRelease;
        cardBeingMoved = null;
    }
    
    private Slot GetClosestToMovingCard(Slot[] slots, CardType slotType)
    {
        float minDis = float.MaxValue;
        Slot minSlots = null;

        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].Type != slotType || slots[i].HasCard) continue;
            float dis = Vector3.Distance(slots[i].transform.position, cardBeingMoved.MouseInteraction.DragPosition);
            if (dis < minDis)
            {
                minSlots = slots[i];
                minDis = dis;
            }
        }
        return minSlots;
    }

    private void PlaceCard(Slot targetSlots)
    {
        cardBeingMoved.transform.position = targetSlots.transform.position;
    }
}