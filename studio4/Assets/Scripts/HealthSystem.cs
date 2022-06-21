using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] Slider healthSlider;

    private void Start() //hello
    {
        healthSlider.maxValue = health;
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " died");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DealDamage(2);
        }
    }
}
