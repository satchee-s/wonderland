using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCard : MonoBehaviour
{
    public static List<Card> displayCard = new List<Card>();

    public int displayId;
    public int cardId;
    public string cardName;
    public int attack;
    public int health;
    public string cardDescription;

    public Text nameText;
    public Text idText;
    public Text attackText;
    public Text healthText;
    public Text descriptionText;

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
        cardDescription = displayCard[0].cardDescription;

        nameText.text = "" + cardName;
        attackText.text = "" + attack;
        healthText.text = "" + health;
        descriptionText.text = "" + cardDescription;

    }
}

