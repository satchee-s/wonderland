using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    [SerializeField] TutorialGameManager gameManager;
    [SerializeField] PlayerManager playerManager;
    //[SerializeField] PlayerTurnSystem turnSystem;

    [SerializeField] GameObject TurnObj;
    [SerializeField] GameObject discardButton;
    [SerializeField] GameObject DrawOneandOne;
    [SerializeField] GameObject DrawTwo;
    [SerializeField] GameObject Shuffle;
    [SerializeField] GameObject endTurn;
    [SerializeField] GameObject sacrifice;
    [SerializeField] GameObject AttackCard;
    [SerializeField] GameObject AttackFace;
    [SerializeField] GameObject endTutorial;
    [SerializeField] Card swiftRecovery;
    [SerializeField] PlayerSlotsManager slots;

    private int i = 0;
    private int countForInput;


    void Start()
    {
        countForInput = 0;
        DrawOneandOne.SetActive(false);
        DrawTwo.SetActive(false);
        Shuffle.SetActive(false);
        discardButton.SetActive(false);
    }

    public void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (this.i == i)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        if (i != 2)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                i++;
        }  
        if (i == 2)
        {
            if (Input.GetMouseButtonDown(1))
            {
                i++;
            }
        }

        else if (i == 8)
        {
            //if player places booster card
            //{ if(player clicks on Discard button)
            //destroy this card (that is being interacted with)
            //discardButton.SetActive(true);
            //i++; else if(player palces creature card)
            //Error.SetActive(true); 
            discardButton.SetActive(true);

            if (slots.currentCard.type == CardType.Booster)
            {
                popUps[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    gameManager.TutorialDeck.Remove(slots.currentCard);
                    slots.currentCard.gameObject.SetActive(false);
                    i++;
                    return;
                });

                FindCard(swiftRecovery);
            }
        }
        else if (i == 9)
        {
            discardButton.SetActive(false);
        }
        else if (i == 11)
        {
            //if(player places 2 Creature cards on the creature row)
            //{ //if player taps on 2 creature cards then sacrifice button should pop up!)} 
            // IF sacrifice button is pressed then destroy both of those cards 
            //Increase Mana
            // { i++ }

            //else if(booster is placed, Error Message (This is Not a Creature Card!
            //Error.SetActive(true);
            int creatureCounter = 0;
            Card[] sacrificeCards = new Card[2];
            foreach (Card cards in slots.cardsPlaced)
            {
                if (cards.type == CardType.Creature)
                {
                    sacrificeCards[creatureCounter] = cards;
                    creatureCounter++;
                }
                else if (cards.type == CardType.Booster)
                {
                    //error ui
                }
            }
            if (creatureCounter == 2)
                sacrifice.SetActive(true); 
            sacrifice.GetComponent<Button>().onClick.AddListener(() =>
            {
                sacrificeCards[0].gameObject.SetActive(false);
                sacrificeCards[1].gameObject.SetActive(false);
                PlayerTurnSystem.maxMana += 2;
                PlayerTurnSystem.currentMana += 2;
                sacrifice.SetActive(false);
                i++;
            });
        }
        else if (i == 12)
        {
            sacrifice.SetActive(false);
        }
        else if (i == 16)
        {
            endTurn.SetActive(true);
        }
        else if (i == 17)
        {
            endTurn.SetActive(false);
        }
        else if (i == 18) //if start pop up (Deactivate Opponents turn UI or Move it elsewhere)
        {
            /*if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }*/
        }
        else if (i == 19)
        {
            DrawOneandOne.SetActive(true);
            DrawTwo.SetActive(true);
        }
        else if (i == 20)
        {
            DrawOneandOne.SetActive(false);
            DrawTwo.SetActive(false);

            //if(2 creature cards have been played)
            //{  i++; }
            //else if(Booster card has been played)
            //{ activate error that says (Cant Place Booster card here)
            //Error.SetActive(true);
            int creatureCounter = 0;
            foreach (Card cards in slots.cardsPlaced)
            {
                if (cards.type == CardType.Booster)
                {
                    //error message ui
                    break;
                }
                if (cards.type == CardType.Creature) 
                    creatureCounter++;
            }
            if (creatureCounter >= 0) 
                i++;            
        }
        else if (i == 21)
        {
            //if( 1 creature card has been tapped on
            // i++
            if (slots.currentCard.type == CardType.Creature)
            {
                i++;
            }
        }
        else if (i == 22)
        {
            AttackCard.SetActive(true);
            AttackFace.SetActive(true);
        }
        else if (i == 23)
        {
            AttackCard.SetActive(false);
            AttackFace.SetActive(false);
            AttackFace.GetComponent<Button>().onClick.AddListener(() =>
            {
                playerManager.health -= slots.currentCard.attack;
                i = 25;
            });

            AttackCard.GetComponent<Button>().onClick.AddListener(() =>
            {
                //attack card ui should specify which card is being used
                //slots.currentCard.health -= opponentCard.attack;
                //opponentCard.health -= slots.currentCard.health;
                i++;
            });

            //if(players taps on 1 enemy card)
            //deal damage & take damage
            //set i == 25 else if(player taps on face button)
            //{ i == 25 }
        }
        else if (i == 27)
        {
            endTutorial.SetActive(true);
        }
    }

    public void Next()
    {
        i++;
    }

    void FindCard(Card cardNeeded)
    {
        for (int i = 0; i < 8; i++)
        {
            if (slots.allSlots[i].currentCard != null)
            {
                if (slots.allSlots[i].currentCard.name == cardNeeded.name)
                {
                    Debug.Log(cardNeeded + " Played");
                    i++;
                    break;
                }
            }
        }
    }
}
