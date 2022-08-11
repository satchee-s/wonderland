using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int health;
    [SerializeField] Slider healthSlider;
    [SerializeField] Text healthText;

    private void Start()
    {
        healthSlider.maxValue = health;
    }

    public void DealDamage(int damage)
    {
        health -= damage;
       healthSlider.value = health;
        if (health <= 0)
        {
            healthText.text = "Player died";
            Debug.Log(gameObject.name + " died");
            gameObject.SetActive(false);
        }
    }
}
