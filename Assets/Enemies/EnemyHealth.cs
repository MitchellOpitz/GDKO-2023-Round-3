using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Image healthBarImage;

    private void Start()
    {
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
            FindObjectOfType<AudioManager>().FadeOut(3f); // For test purposes.
            gameObject.GetComponent<Boss>().StartUpgrades();
        }
        Destroy(gameObject);
    }
}
