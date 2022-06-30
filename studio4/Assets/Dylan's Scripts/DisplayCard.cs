using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using TMPro;

public class DisplayCard : MonoBehaviour
{
    public static List<Card> displayCard = new List<Card>();

    public int displayId;
    public int cardId;
    public string cardName;
    public int attack;
    public int health;
    public string cardDescription;

    public TextMeshPro nameText;
    public TextMeshPro attackText;
    public TextMeshPro healthText;
    public TextMeshPro descriptionText;

    // Start is called before the first frame update
    void Start()
    {
        displayCard[0] = CardDataBase.cardList[displayId];
    }

    // Update is called once per frame
    void Update()
    {
        cardId = displayCard[0].cardId;
        cardName = displayCard[0].cardName;
        attack = displayCard[0].attack;
        health = displayCard[0].health;
        
        nameText.text = " " + cardName;
        attackText.text = " " + attack;
        healthText.text = " " + health;
        descriptionText.text = " " + cardDescription;

    }
}

