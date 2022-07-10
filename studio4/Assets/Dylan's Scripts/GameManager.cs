using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public GameObject Hand;
    public GameObject CardSlots;
    public Transform[] cardSlots;
    public bool[] availableCardSlots;


    public void DrawCard()
    {
        if(deck.Count >= 1)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for(int i = 0; i < availableCardSlots.Length; i++)
            {
                if(availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlots[i].transform.position;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    public void ShuffleDeck()
    {
        if (deck.Count > 1) //check if there are more than 1 card in the deck of cards
        {
            Debug.Log("You Only Have 1 Card Left!!!");

            for(int i = 0; i < deck.Count; i++)
            {
                container[0] = deck[i]; 
                int randomIndex = Random.Range(i, deck.Count);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = container[0];
            }
        }
    }
}
