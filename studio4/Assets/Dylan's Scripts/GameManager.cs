using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Card> creatureDeck = new List<Card>();
    public List<Card> boosterDeck = new List<Card>();
    public List<Card> container = new List<Card>();
    public GameObject OneboosterOneCreature;
    public GameObject TwoCreature;
    public GameObject ShuffleCard;

    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public bool HasDrawedCards = false;
    private bool FirstStep = false;
    private bool SecondStep = false;
    private bool ThirdStep = false;


    public void Start()
    {
        FirstStep = false;
        SecondStep = false;
        ThirdStep = false;
        HasDrawedCards = false;

        FirstTurn();
    }

    public void FirstTurn()
    {

        ShuffleCard.SetActive(false);

        Card creatureCard = creatureDeck[Random.Range(0, creatureDeck.Count)];
        Card secCreatureCard = creatureDeck[Random.Range(0, creatureDeck.Count)];
        Card thirdCreatureCard = creatureDeck[Random.Range(0, creatureDeck.Count)];
        Card fourthCreatureCard = creatureDeck[Random.Range(0, creatureDeck.Count)];

        Card boosterCard = boosterDeck[Random.Range(0, boosterDeck.Count)];
        Card secBoosterCard = boosterDeck[Random.Range(0, boosterDeck.Count)];

        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i] == true)
            {
                creatureCard.gameObject.SetActive(true);
                secCreatureCard.gameObject.SetActive(true);
                thirdCreatureCard.gameObject.SetActive(true);
                fourthCreatureCard.gameObject.SetActive(true);

                boosterCard.gameObject.SetActive(true);
                secBoosterCard.gameObject.SetActive(true);  

                creatureCard.gameObject.tag = "Selectable";
                secCreatureCard.gameObject.tag = "Selectable";
                thirdCreatureCard.gameObject.tag = "Selectable";
                fourthCreatureCard.gameObject.tag = "Selectable";

                boosterCard.gameObject.tag = "Selectable";
                secBoosterCard.gameObject.tag = "Selectable";

                creatureCard.transform.position = cardSlots[i].transform.position;
                creatureCard.originalPos = creatureCard.transform.position;


                secCreatureCard.transform.position = cardSlots[i + 1].transform.position;
                secCreatureCard.originalPos = secCreatureCard.transform.position;


                thirdCreatureCard.transform.position = cardSlots[i + 2].transform.position;
                thirdCreatureCard.originalPos = thirdCreatureCard.transform.position;


                fourthCreatureCard.transform.position = cardSlots[i + 3].transform.position;
                fourthCreatureCard.originalPos = fourthCreatureCard.transform.position;


                boosterCard.transform.position = cardSlots[i + 4].transform.position;
                boosterCard.originalPos = boosterCard.transform.position;

                secBoosterCard.transform.position = cardSlots[i + 5].transform.position;
                secBoosterCard.originalPos = secBoosterCard.transform.position;

                availableCardSlots[i] = false;

                creatureDeck.Remove(creatureCard);
                creatureDeck.Remove(secCreatureCard);
                creatureDeck.Remove(thirdCreatureCard);
                creatureDeck.Remove(fourthCreatureCard);

                boosterDeck.Remove(boosterCard);
                boosterDeck.Remove(secBoosterCard);


                HasDrawedCards = true;
                OneboosterOneCreature.SetActive(false);
                TwoCreature.SetActive(false);
                return;
            }
        }

    }

    public void DrawOneFromBothDecks()
    {
        if(creatureDeck.Count  >= 1 && boosterDeck.Count >=1)
        {
            Card creatureCard = creatureDeck[Random.Range(0, creatureDeck.Count)];
            Card boosterCard = boosterDeck[Random.Range(0, boosterDeck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if(availableCardSlots[i] == true)
                {
                    creatureCard.gameObject.SetActive(true);
                    boosterCard.gameObject.SetActive(true);

                    creatureCard.gameObject.tag = "Selectable";
                    boosterCard.gameObject.tag = "Selectable";

                    creatureCard.transform.position = cardSlots[i].transform.position;
                    creatureCard.originalPos = creatureCard.transform.position;

                    boosterCard.transform.position = cardSlots[i + 1].transform.position;
                    boosterCard.originalPos = boosterCard.transform.position;   

                    availableCardSlots[i] = false;

                    creatureDeck.Remove(creatureCard);
                    boosterDeck.Remove(boosterCard);

                    HasDrawedCards = true;
                    OneboosterOneCreature.SetActive(false);
                    TwoCreature.SetActive(false);
                    return;
                }
            }
        }
    }

    public void DrawTwoCreatureCards()
    {
        if (creatureDeck.Count >= 2)
        {
            Card firstCreatureCard = creatureDeck[Random.Range(0, creatureDeck.Count)];
            Card secondCreatureCard = creatureDeck[Random.Range(0, creatureDeck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    firstCreatureCard.gameObject.SetActive(true);
                    secondCreatureCard.gameObject.SetActive(true);

                    firstCreatureCard.gameObject.tag = "Selectable";
                    secondCreatureCard.gameObject.tag = "Selectable";

                    firstCreatureCard.transform.position = cardSlots[i].transform.position;
                    firstCreatureCard.originalPos = firstCreatureCard.transform.position;

                    secondCreatureCard.transform.position = cardSlots[i + 1].transform.position;
                    secondCreatureCard.originalPos = secondCreatureCard.transform.position;

                    availableCardSlots[i] = false;

                    creatureDeck.Remove(firstCreatureCard);
                    creatureDeck.Remove(secondCreatureCard);

                    HasDrawedCards = true;
                    TwoCreature.SetActive(false);
                    OneboosterOneCreature.SetActive(false);
                    return;
                }
            }
        }
    }

    public void ShuffleDeck()
    {
        if (creatureDeck.Count > 1 ) //check if there are more than 1 card in the deck of cards
        {
            for(int i = 0; i < creatureDeck.Count; i++)
            {
                container[0] = creatureDeck[i]; 
                int randomIndex = Random.Range(i, creatureDeck.Count);
                creatureDeck[i] = creatureDeck[randomIndex];
                creatureDeck[randomIndex] = container[0];
            }
        }
    }
}
