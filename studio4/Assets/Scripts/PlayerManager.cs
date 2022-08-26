using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int health;
    [SerializeField] TextMeshProUGUI healthText;
    public List<Card> playedCards = new List<Card>();
    public List<GameObject> playedHandCards = new List<GameObject>();

    private void Start()
    {
        healthText.text = health.ToString();
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            healthText.text = "Player died";// add ui
            Debug.Log(gameObject.name + " died");
            gameObject.SetActive(false);
        }
    }

    public enum Role
    {
        Player1,
        Player2
    }

    public Role role;
    public bool IsPlayer1 => role == Role.Player1;
    public bool IsPlayer2 => role == Role.Player2;
    public void DamageFromOpponent(int cardAttack)
    {
        health -= cardAttack;
        healthText.text = health.ToString();
        Debug.Log("New Health" + health);
    }

}
