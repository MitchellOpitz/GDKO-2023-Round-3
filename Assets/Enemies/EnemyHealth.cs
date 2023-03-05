using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int points;
    public Animation animation;

    public Image healthBarImage;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        maxHealth = (int)(maxHealth * (float)Math.Pow(1 + 0.1, gameManager.level));
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Update the health bar image
        if (healthBarImage != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBarImage.fillAmount = fillAmount;
        }

        // Check if the enemy has died
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Do something when the enemy dies
        if (gameObject.GetComponent<Boss>())
        {
            FindObjectOfType<GameManager>().level++;
            FindObjectOfType<AudioManager>().FadeOut(3f); // For test purposes.
            gameObject.GetComponent<Boss>().StartUpgrades();
        }
        gameManager.AddScore(points);
        Destroy(gameObject);
    }
}
