//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SlotsManager : MonoBehaviour
//{
//    /**
//     THIS MANAGES MULTIPLE SLOTS.
//     */
//    //Vector3 slotPosition;
//    // public bool hasCard = false;
//    // public enum slotType { Creature, Booster };
//    // bool canBePlaced = false; //this is an attribute for the card, not the slot
//    //  bool hasCollidedWithSlot = false;
//    //  Card cardBeingMoved;
//    //  Card cardInSlot;
//    //[SerializeField] Card.CardType cardType;
//    public Slot[] creatureCardSlots = new Slot[4]; //4 slots
//    public Slot[] boosterCardSlots = new Slot[4]; //4 slots 
//    public Slot[] allSlots = new Slot[8];
//    public Card cardBeingMoved;


//    private void OnEnable()
//    {
//        CardMouseInteraction.onDragEvent += getCardBeingMoved;
//    }
//    private void OnDisable()
//    {
//        CardMouseInteraction.onDragEvent -= getCardBeingMoved;
//    }

//    private void getCardBeingMoved(CardMouseInteraction card)
//    {
//        cardBeingMoved = card.card;

//    }



//    private void Update()
//    {

//      Slot closestCard = GetClosestToCard(allSlots);
//        PlaceCard(closestCard);
      

//        //if (hasCollidedWithSlot) //this is drag and drop card placement. Make sure to place it in the middle of the slot bc wonky
//        //{
//        //    PlaceCard();
//        //    Debug.Log("CARD PLACED IN SLOT");

//        //}
//        //else if (Input.GetKeyDown(KeyCode.W))//to attack opponent's cards
//        //{
//        //    if (cardBeingMoved != cardInSlot)
//        //    {
//        //        if ( hasCard) //checks if opponent card is in trigger and player has card in slot +canBeSummoned
//        //        {
//        //            //something about checking if opponent card is being used
//        //            if (cardBeingMoved.Type == Card.CardType.Creature)
//        //            {
//        //                cardBeingMoved.TakeDamage(cardInSlot.attack);
//        //            }
//        //            else if (cardBeingMoved.Type == Card.CardType.Booster)
//        //            {
//        //                cardBeingMoved.GetComponent<CreatureManager>().PlayCard(cardInSlot, this);
//        //            }
//        //        }
//        //    }

//        //}


//    }

//    private Slot GetClosestToCard(Slot[] slots)
//    {
//        float minDis = float.MaxValue;
//        Slot minSlots = null;

//        for (int i = 0; i < slots.Length; i++)
//        {
//            float dis = Vector3.Distance(slots[i].transform.position, cardBeingMoved.transform.position);
//            if (dis < minDis)
//            {
//                minSlots = slots[i];
//                minDis = dis;
//            }
//        }
//        return minSlots;
//    }

//  /**  private void HoverCard()
//    {
//        if (canBePlaced && !hasCard)
//        {
//            if (cardType == cardBeingMoved.Type)
//            {
//                cardInSlot.originalPos = slotPosition;

//                Debug.Log("Hover");
//            }
//        }
//    }*/

//    private void PlaceCard(Slot targetSlots)
//    {
//        cardBeingMoved.transform.position = targetSlots.transform.position;
//    }
//}

//public class Slot : MonoBehaviour
//{
//    //this is a single SLOT. ONE RECTANGLE.
//    Vector3 slotPosition;
//    public bool availableSlot = false;
//    public enum slotType { Creature, Booster };
//    public bool hasCollidedWithSlot = false; //this is for the card, not the slot
//    public bool isPlaced = false; //this is also for the card
//    public bool isAvailable = false;

//    public bool getSlotAvailability()
//    {
//        return this.availableSlot;
//    }
//}
