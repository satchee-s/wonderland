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
    public List<Card> handCards = new List<Card>();
    public GameObject OneboosterOneCreature;
    public GameObject TwoCreature;
    public GameObject ShuffleCard;

    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public bool HasDrawedCards = false;
    private bool FirstStep = false;
    private bool SecondStep = false;
    private bool ThirdStep = false;

    public RaycastAnim rA;

    public void Start()
    {
        FirstStep = false;
        SecondStep = false;
        ThirdStep = false;
        HasDrawedCards = false;

        FirstTurn();
    }

    void SetRandomCards()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i < 2)
            { 
                handCards.Add(boosterDeck[Random.Range(0, boosterDeck.Count)]);
                handCards[i].tag = "Selectable";
                boosterDeck.Remove(handCards[i]);
            }
            else
            {
                handCards.Add(creatureDeck[Random.Range(0, creatureDeck.Count)]);
                handCards[i].tag = "Selectable";
                creatureDeck.Remove(handCards[i]);
            }
        }
    }

    public void FirstTurn()
    {
        SetRandomCards();

        ShuffleCard.SetActive(false);

        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i] == true)
            {
                handCards[i].transform.position = cardSlots[i].transform.position;
                handCards[i].originalPos = handCards[i].transform.position;
                availableCardSlots[i] = false;
            }
        }
        HasDrawedCards = true;
        OneboosterOneCreature.SetActive(false);
        TwoCreature.SetActive(false);

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

    /*public void GoBack()
    {
        GetComponent<RaycastAnim>().GoBack1();
    }*/
}
