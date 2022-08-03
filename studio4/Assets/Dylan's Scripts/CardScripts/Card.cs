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
    public bool sleep;
    public bool cantAttack;
    public bool canAttack;
    public bool targeting;
    public bool targetingEnemy;
    public bool onlyThisCardAttack;
    public static bool staticTarget;
    public static bool staticTargetEnemy;
    private GameManager gm;
    
  
    public TextMeshPro nameText;
    public TextMeshPro attackText;
    public TextMeshPro healthText;
    public TextMeshPro descriptionText;

    public GameObject attackBordor;
    public GameObject Target;
    public GameObject Enemy;

   


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

        }

        if (canAttack == true)
        {
            attackBordor.SetActive(true);
        }
        else
        {
            attackBordor.SetActive(false);
        }

        if (PlayerTurnSystem.isYourTurn == false && summoned == true)
        {
            sleep = false;
            canAttack = false;
        }

        if(PlayerTurnSystem.isYourTurn == true && sleep == false && cantAttack == false)
        {
            cantAttack = true;
        }
        else
        {
            cantAttack= false;
        }

        targeting = staticTarget;

        targetingEnemy = staticTargetEnemy;

        if(targetingEnemy == true)
        {
            Target = Enemy;
        }
        else
        {
            Target = null;
        }

        if(targeting == true && targetingEnemy == true && onlyThisCardAttack == true)
        {
            Attack();
        }

    }

    private void Start()
    {
        //gm = FindObjectOfType<GameManager>();
        originalRotationValue = transform.rotation;

        canBeSummoned = false;
        summoned = false;

        canAttack = false;
        sleep = true;

        Enemy = GameObject.Find("Enemy HP");

        targeting = false;
        targetingEnemy = false;

    }

   

    public void Summon()
    {
        PlayerTurnSystem.currentMana -= cost;
        summoned = true;
        hasBeenPlayed = true;
    }

        

    }

    public void Maxmana(int x)
    {
        PlayerTurnSystem.maxMana += x;
    }

    public void Attack()
    {
        if(canAttack == true)
        {
            if(Target != null)
            {
                if(Target == Enemy)
                {
                    //EnemyHp.staticHp -= power;
                    targeting = false;
                    cantAttack = true;
                }

                if(Target.name == "CardToHand(Clone)")
                {
                    canAttack = true;
                }
            }
        }
    }

    public void UntargetEnemy()
    {
        staticTargetEnemy = false;
    }

    public void TargetEnemy()
    {
        staticTargetEnemy = true;
    }

    public void StartAttack()
    {
        staticTarget = true;
    }

    public void StopAttack()
    {
        staticTarget = false;
    }

    public void OneCardAttack()
    {
        onlyThisCardAttack = true;
    }

    public void OneCardAttackStop()
    {
        onlyThisCardAttack = false;
    }

}
