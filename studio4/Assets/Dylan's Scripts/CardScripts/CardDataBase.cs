using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    /*public int cardId;
    public string cardName;
    public int attack;
    public int health;
    public string cardDescription;*/

    void Awake()
    {
        cardList.Add(new Card(0, "Executioner", 6, 8, "This is an Executioner", 6));
        cardList.Add(new Card(1, "Cheshire Cat", 5, 7, "This is a Cheshire Cat", 5));
        cardList.Add(new Card(2, "Mad Hatter", 5, 6, "This is a Mad Hatter", 4));
        cardList.Add(new Card(3, "Red Knight", 4, 5, "This is a Red Knight", 3));
        cardList.Add(new Card(4, "Card Guardsman", 4, 4, "This is a Card Guardsman", 2));
        cardList.Add(new Card(5, "White Rabbit", 3, 3, "This is a White Rabbit", 1));
        cardList.Add(new Card(6, "Tweedle Dee & Tweedledum (The Twins)", 3, 2, "This is The Twins ", 0));
        cardList.Add(new Card(7, "Dodo", 2, 1, "This is a Dodo", 0));
    }

}
