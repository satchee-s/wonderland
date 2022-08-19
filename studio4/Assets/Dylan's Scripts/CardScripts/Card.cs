using UnityEngine;
using TMPro;
using core;


[System.Serializable]
public class Card : MonoBehaviour
{
    public int cardId;
    public string cardName;
    public int attack;
    public int health;
    public string description;
    public bool hasBeenPlayed;
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
    [SerializeField] NetManager netManager;

    public CardType Type;

    public enum CreatureCardClass { Peasant, Elite };
    public CreatureCardClass CardClass;

    public bool isOpponentCard;

    private CardMouseInteraction mouseInteraction;
    public CardMouseInteraction MouseInteraction => mouseInteraction;

    public Card(int CardId, string CardName, int Attack, int Health, string Description, int Cost)
    {
        cardId = CardId;
        cardName = CardName;
        attack = Attack;
        health = Health;
        description = Description;
        cost = Cost;
    }

    private void Awake()
    {
        isOpponentCard = FindLastParent().GetComponent<PlayerManager>().IsPlayer2;
        mouseInteraction = GetComponent<CardMouseInteraction>();
    }

    void Update()
    {
        if (PlayerTurnSystem.currentMana >= cost && summoned == false)
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
        if (CardType.Creature == Type)
        {
            health -= amount;
            CardPacket cp = new CardPacket(cardId, cardName, health, attack, sleep, netManager.player);
            netManager.SendPacket(cp.StartSerialization());

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
