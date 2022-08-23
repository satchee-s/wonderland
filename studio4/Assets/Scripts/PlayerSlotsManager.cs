using System;
using System.Collections.Generic;
using UnityEngine;
using core;

public class PlayerSlotsManager : MonoBehaviour
{
    /**
     THIS MANAGES MULTIPLE SLOTS.
     */
 
    public Slot[] allSlots = new Slot[8]; 
    private Card cardBeingMoved;//this card is gonna be palced somewhere. when this is placed in a slot, it gets added to allSlots
    public List<Card> cardsPlaced = new List<Card>();// EACH SLOT HOLDS A CARD OBJECT, THIS MEANS THAT EACH INDEX IS A SLOT ID
    [HideInInspector] public Card currentCard;
    public PlayerTurnSystem ptsi;
    NetManager netManager;
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
        currentCard = card.gameObject.GetComponent<Card>();
        cardsPlaced.Add(currentCard);

        //now we need to send the cardsPlaced over the network IN THE NET MANAGER.
    }

    private void Start()
    {
        netManager = FindObjectOfType<NetManager>();
    }
    private void Update()
    {
        if (PlayerTurnSystem.isYourTurn) {
            if (cardBeingMoved != null)
            {
                Slot closestSlot = GetClosestToMovingCard(allSlots, cardBeingMoved.type);
                if (closestSlot != null)
                    PlaceCard(closestSlot);
            }
        }
        /*else {
            Debug.Log("Ïts not your turn bro." + ptsi.getTime() + "seconds left"); 
        }*/
       
    }
    
    private void HandleCardRelease()
    {
        Slot closestSlot = GetClosestToMovingCard(allSlots, cardBeingMoved.type);
        if(closestSlot == null) return;
        if(cardBeingMoved.cost <= PlayerTurnSystem.currentMana)
        {
        closestSlot.PlaceCardInSlot(cardBeingMoved);
        }
        cardBeingMoved.MouseInteraction.onRelease -= HandleCardRelease;
        cardsPlaced.Remove(cardBeingMoved);

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
        if (!cardsPlaced.Contains(cardBeingMoved)){
            cardsPlaced.Add(cardBeingMoved);
        }
        cardBeingMoved.transform.position = targetSlots.transform.position;
        PositionPacket pp = new PositionPacket(cardBeingMoved.transform.position, netManager.player);
        netManager.SendPacket(pp.StartSerialization());
        //Debug.Log("Sent position packet to unity");
    }
}