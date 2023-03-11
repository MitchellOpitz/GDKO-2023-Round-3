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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            currentHealth--;
            if (!collider.gameObject.GetComponent<Boss>())
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
