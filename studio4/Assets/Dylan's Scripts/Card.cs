using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using TMPro;

[System.Serializable]
public class Card : MonoBehaviour
{
    public int cardId;
    public string cardName;
    public int attack;
    public int health;
    public string description;
  
    public TextMeshPro nameText;
    public TextMeshPro attackText;
    public TextMeshPro healthText;
    public TextMeshPro descriptionText;

    public Card()
    {

    }

    public Card(int CardId, string CardName, int Attack, int Health, string Description)
    {
        cardId = CardId;
        cardName = CardName;
        attack = Attack;
        health = Health;
        description = Description;
    }

     void Update()
    {
        nameText.text = " " + cardName;
        attackText.text = " " + attack;
        healthText.text = " " + health;
        descriptionText.text = " " + description;
    }
}