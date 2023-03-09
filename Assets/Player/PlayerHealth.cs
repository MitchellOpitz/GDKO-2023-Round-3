using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;

    private HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.maxHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.currentHealth = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealth(currentHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy touched.");
            currentHealth--;
        }
    }
}
