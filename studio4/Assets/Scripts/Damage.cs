using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    //card functions
    public HealthSystem healthSystem;
    public Card opponentCard;
    [SerializeField] Slider opponentCardSlider;

    private void Start()
    {
        opponentCardSlider.maxValue = opponentCard.health;
    }

    public void PlayerAttack(Card card)
    {
        healthSystem.DealDamage(card.attack);
    }

    public void AttackAgainstCard(Card card, Card opponentCard)
    {
        opponentCard.health -= card.attack;
        if (opponentCard.health <= 0)
        {
            Debug.Log("Card deactivated");
            opponentCard.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerAttack(this.GetComponent<Card>());
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            AttackAgainstCard(this.GetComponent<Card>(), opponentCard);
            opponentCardSlider.value = opponentCard.health;

        }
    }

}
