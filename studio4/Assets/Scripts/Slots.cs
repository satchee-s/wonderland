using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    Vector3 slotPosition;
    public bool hasCard = false;
    bool canBePlaced = false;
    Card cardBeingMoved;
    Card cardInSlot;
    [SerializeField] Card.CardType cardType;
    public Slots creatureCardSlot; //for booster card slots to track the corresponding creature card slot below it

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Selectable"))
        {
            canBePlaced = false;
            cardBeingMoved = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //use q to place card in slot
        {
            PlaceCard();
        }

        else if (Input.GetKeyDown(KeyCode.W))//to attack opponent's cards
        {
            if (cardBeingMoved != cardInSlot)
            {
                if (canBePlaced && hasCard) //checks if opponent card is in trigger and player has card in slot +canBeSummoned
                {
                    //something about checking if opponent card is being used
                    if (cardBeingMoved.Type == Card.CardType.Creature)
                    {
                        cardBeingMoved.Damage(cardInSlot, this);
                    }
                    else if (cardBeingMoved.Type == Card.CardType.Booster)
                    {
                        cardBeingMoved.GetComponent<CreatureManager>().PlayCard(cardInSlot, this);
                    }
                }
            }

        }
    }

    private void PlaceCard()
    {
        if (canBePlaced && !hasCard)
        {
            if (cardType == cardBeingMoved.Type) //&&card.canBeSummoned == true
            {
                cardInSlot = cardBeingMoved;
                cardInSlot.originalPos = slotPosition;
                cardInSlot.transform.position = slotPosition;
                hasCard = true;
                //cardInSlot.Summon();
            }
        }
    }
}
