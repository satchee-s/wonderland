using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
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
    public Slot[] creatureCardSlots = new Slot[3]; //4 slots
    public Slot[] boosterCardSlots = new Slot[3]; //4 slots 
    


    private void Start()
    {
        slotPosition = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Selectable"))
        {
            cardBeingMoved = other.gameObject.GetComponent<Card>();
            canBePlaced = true;
            hasCollidedWithSlot = true;
            Debug.Log("CARD COLLIDED WITH SLOT");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (cardInSlot == null) return;
        if (other.CompareTag("Selectable") && cardInSlot.canBeSummoned == true)
        {
            canBePlaced = false;
            cardBeingMoved = null;
        }
    }

    private void Update()
    {
        if (hasCollidedWithSlot) //this is drag and drop card placement. Make sure to place it in the middle of the slot bc wonky
        {
            PlaceCard();
            Debug.Log("CARD PLACED IN SLOT");

        }
        else if (Input.GetKeyDown(KeyCode.W))//to attack opponent's cards
        {
            if (cardBeingMoved != cardInSlot)
            {
                if ( hasCard) //checks if opponent card is in trigger and player has card in slot +canBeSummoned
                {
                    //something about checking if opponent card is being used
                    if (cardBeingMoved.Type == Card.CardType.Creature)
                    {
                        cardBeingMoved.TakeDamage(cardInSlot.attack);
                    }
                    else if (cardBeingMoved.Type == Card.CardType.Booster)
                    {
                        cardBeingMoved.GetComponent<CreatureManager>().PlayCard(cardInSlot, this);
                    }
                }
            }

        }
    }

  /**  private void HoverCard()
    {
        if (canBePlaced && !hasCard)
        {
            if (cardType == cardBeingMoved.Type)
            {
                cardInSlot.originalPos = slotPosition;

                Debug.Log("Hover");
            }
        }
    }*/

    private void PlaceCard(Collider other)
    {
        if (!hasCard)
        {
            if (this.slotType == other.gameObject.GetComponent<Card>().getCardType()) //&&card.canBeSummoned == true
            {
                cardInSlot = cardBeingMoved;
                cardInSlot.originalPos = slotPosition;
                cardInSlot.transform.position = slotPosition;
                hasCard = true;
                //cardInSlot.Summon();
                Debug.Log("Place");
            }
        }
    }
}

public class Slot : MonoBehaviour
{
    //this is a single SLOT. ONE RECTANGLE.
    Vector3 slotPosition;
    public bool availableSlot = false;
    public enum slotType { Creature, Booster };
    public bool hasCollidedWithSlot = false; //this is for the card, not the slot
    public bool isPlaced = false; //this is also for the card
    public bool isAvailable = false;

    public bool getSlotAvailability()
    {
        return this.availableSlot;
    }
}
