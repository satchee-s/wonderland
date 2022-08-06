using UnityEngine;
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
    public bool sleep;
    public PlayerManager playerManager;
    public bool isHibernating;
    public static bool staticTarget;
    public static bool staticTargetEnemy;


    public TextMeshPro nameText;
    public TextMeshPro attackText;
    public TextMeshPro healthText;
    public TextMeshPro descriptionText;

    // public GameObject attackBordor;
    // public GameObject Target;
    // public GameObject Enemy;

    [HideInInspector] public Vector3 originalPos;
    [HideInInspector] public Quaternion originalRotationValue;

    public enum CardType { Creature, Booster };
    public CardType Type;

    public enum CreatureCardClass { Peasant, Elite };
    public CreatureCardClass CardClass;

    public bool isOpponentCard;

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

    private void Awake()
    {
        isOpponentCard = FindLastParent().GetComponent<PlayerRole>().IsOpponent;
    }

    void Update()
    {
        /*nameText.text = " " + cardName; --- why??
        attackText.text = " " + attack;
        healthText.text = " " + health;
        descriptionText.text = " " + description;*/

        if (PlayerTurnSystem.currentMana >= cost && summoned == false) //does this need to be in update?
        {
            canBeSummoned = true;
        }
        else canBeSummoned = false;


        if (canBeSummoned == true)
        {

        }

       

        
       
       

    }

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        //gm = FindObjectOfType<GameManager>();
        originalRotationValue = transform.rotation;
        originalPos = transform.position;

        canBeSummoned = false;
        summoned = false;

        
        sleep = true;

        //  Enemy = GameObject.Find("Enemy HP");

        
    }

    public void Attack(Card opponentCard)
    {
        
        if (Type == CardType.Creature && opponentCard.Type == CardType.Creature && sleep == false)
        {
            opponentCard.TakeDamage(attack);
           
           
        }
    }

    public void TakeDamage(int amount)
    {
        
        if(CardType.Creature == Type)
        {
            health -= amount;

            if (health <= 0)
            {
                gameObject.SetActive(false);
                playerManager.health -= Mathf.Abs(health);
            }
        }

       
    }

    public void Summon()
    {
        PlayerTurnSystem.currentMana -= cost;
        summoned = true;
        hasBeenPlayed = true;
        sleep = true;
    }
    public void Maxmana(int x)
    {
        PlayerTurnSystem.maxMana += x;
    }

    public CardType getCardType()
    {
        return this.Type;
    }

    

   

    private Transform FindLastParent()
    {
        Transform current = transform.parent;

        while (current != null && current.parent != null)
            current = current.parent;

        return current;
    }
}
