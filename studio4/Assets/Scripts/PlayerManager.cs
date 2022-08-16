using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int health;
    //[SerializeField] Slider healthSlider;
    [SerializeField] Text healthText;

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            healthText.text = "Player died";
            Debug.Log(gameObject.name + " died");
            gameObject.SetActive(false);
        }
    }
}
