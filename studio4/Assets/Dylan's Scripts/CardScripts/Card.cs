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
    public int cardId; //dont need this
    public string cardName;
    public int attack;
    public int health;
    public string description; //???
    public bool hasBeenPlayed;
    //public int handIndex; //???
    public int cost;
    public bool canBeSummoned;
    public bool summoned;
    public bool isHibernating;
    //private GameManager gm;


    [SerializeField] TextMeshPro nameText;
    [SerializeField] TextMeshPro attackText;
    [SerializeField] TextMeshPro healthText;
    [SerializeField] TextMeshPro descriptionText;

    [HideInInspector] public Vector3 originalPos;
    [HideInInspector] public Quaternion originalRotationValue;

    public enum CardType {Creature, Booster};
    public CardType Type;

    public enum CreatureCardClass { Peasant, Elite};
    public CreatureCardClass CardClass;

    public Card(int CardId, string CardName, int Attack, int Health, string Description, int Cost)
    {
        cardId = CardId;
        cardName = CardName;
        attack = Attack;
        health = Health;
        description = Description;
        cost = Cost;
    }

    /*public Card(string CardName, int Attack, int Health, int Cost, CardType type, CreatureCardClass cardClass)
    { //--- alt one for creatures
        cardName = CardName;
        attack = Attack;
        health = Health;
        cost = Cost;
        Type = type;
        CardClass = cardClass;
    }*/

/*public Card (string CardName, int Cost, CardType type) --- alt one for boosters
{
    cardName = CardName;
    cost = Cost;
    Type = type;
}*/

void Update()
    {
        /*nameText.text = " " + cardName; --- why??
        attackText.text = " " + attack;
        healthText.text = " " + health;
        descriptionText.text = " " + description;*/

        if(PlayerTurnSystem.currentMana >= cost && summoned == false) //does this need to be in update?
        {
            canBeSummoned = true;
        }
        else canBeSummoned = false;

        /*if(canBeSummoned == true)
        {

        }*/        
    }

    private void Start()
    {
        //gm = FindObjectOfType<GameManager>();
        originalRotationValue = transform.rotation;

        canBeSummoned = false;
        summoned = false;
    }

    public void Summon()
    {
        PlayerTurnSystem.currentMana -= cost;
        summoned = true;
        hasBeenPlayed = true;
    }

    public void Damage(Card opponentCard, Slots slot)
    {
        if (Type == CardType.Creature && opponentCard.Type == CardType.Creature)
        {
            health -= opponentCard.attack;
            opponentCard.health = health;
            //Debug.Log($"card health: {health} opponent health {opponentCard.health}");
            ChangeDescription();
            opponentCard.ChangeDescription();
            if (health <= 0)
            {
                gameObject.SetActive(false);
                slot.hasCard = false;
                //Debug.Log("card destroyed");
            }
            if (opponentCard.health <= 0)
            {
                opponentCard.gameObject.SetActive(false);
                slot.hasCard = false;
                //Debug.Log("opponent card destroyed");
            }
        }
    }

    void ChangeDescription()
    {
        //nameText.text = " " + cardName;
        attackText.text = " " + attack;
        healthText.text = " " + health;
        //descriptionText.text = " " + description;
    }

}