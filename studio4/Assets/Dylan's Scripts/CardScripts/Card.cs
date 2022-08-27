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
    //public PlayerManager playerManager;
    public bool isHibernating;
    public static bool staticTarget;
    public static bool staticTargetEnemy;

    public TextMeshPro nameText;
    public TextMeshPro attackText;
    public TextMeshPro healthText;
    public TextMeshPro descriptionText;

    public Vector3 originalPos;
    [HideInInspector] public Quaternion originalRotationValue;
    [SerializeField] GameObject cardPrefab;

    public CardType type;

    public enum CreatureCardClass { Peasant, Elite };
    public CreatureCardClass cardClass;

    public enum NameOfCard { Dodo, Rabbit, Hatter, Twins, Executioner, Guardsman, Regeneration, Corruption, Booster, Hypnosis, Recovery, Swap, Choice, Cat, Knight};
    public NameOfCard nameCard;

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

    public Card(string CardName, int Attack, int Health, int Cost, CreatureCardClass CardClass, CardType Type)
    {
        cardName = CardName;
        attack = Attack;
        health = Health;
        cost = Cost;
        cardClass = CardClass;
        type = Type;
    }

    private void Awake()
    {
        isOpponentCard = FindLastParent().GetComponent<PlayerManager>().IsPlayer2;
        mouseInteraction = GetComponent<CardMouseInteraction>();
        //netManager = FindObjectOfType<NetManager>();
    }

    void Update()
    {
        if (PlayerTurnSystem.currentMana >= cost && summoned == false)
        {
            canBeSummoned = true;
        }
        else canBeSummoned = false;
        cardName = cardPrefab.name;
    }

    private void Start()
    {
        //playerManager = GetComponent<PlayerManager>();
        originalRotationValue = transform.rotation;
        originalPos = transform.position;

        canBeSummoned = false;
        summoned = false;
        sleep = true;
    }

    public void Attack(Card opponentCard)
    {
        if (type == CardType.Creature && opponentCard.type == CardType.Creature && sleep == false)
        {
            opponentCard.TakeDamage(attack);
        }
    }

    public void TakeDamage(int amount)
    {
        if (CardType.Creature == type)
        {
            health -= amount;
            //CardPacket cp = new CardPacket(cardId, cardName, health, attack, sleep);
            //netManager.SendPacket(cp.StartSerialization());

            if (health <= 0)
            {
                //DestroyPacket dp = new DestroyPacket(GetInstanceID());
                //netManager.SendPacket(dp.StartSerialization());
                //playerManager.health -= Mathf.Abs(health);
                gameObject.SetActive(false);
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
        return this.type;
    }

    private Transform FindLastParent()
    {
        Transform current = transform.parent;

        while (current != null && current.parent != null)
            current = current.parent;

        return current;
    }
}
