using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card : MonoBehaviour
{
    public int cardId;
    public string cardName;
    public int attack;
    public int health;
    public string cardDescription;

    public Card()
    {

    }

    public Card(int CardId, string CardName, int Attack, int Health, string CardDescription)
    {
        cardId = CardId;
        cardName = CardName;
        attack = Attack;
        health = Health;
        cardDescription = CardDescription;
    }
}