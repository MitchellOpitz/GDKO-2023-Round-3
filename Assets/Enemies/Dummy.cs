using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : MonoBehaviour
{
    public int maxHealth = 100;      // maximum health of the enemy
    public int minHealth = 5;        // lowest health the enemy can have
    public float healDelay = 2f;     // time delay for the enemy to heal after taking damage
    public Image healthBar;

    private int currentHealth;       // current health of the enemy
    private float lastDamageTime;    // time when the enemy was last damaged

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;   // set initial health to maximum
    }

    // Update is called once per frame
    void Update()
    {
        // check if the enemy needs to heal
        if (currentHealth < maxHealth && Time.time > lastDamageTime + healDelay)
        {
            currentHealth = maxHealth;   // reset health to maximum
            UpdateHealth();
        }
    }

    // function to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;        // subtract damage from current health
        lastDamageTime = Time.time;     // record the time of the damage

        // check if the enemy's health has dropped below the minimum
        if (currentHealth < minHealth)
        {
            currentHealth = minHealth;  // set health to the minimum
        }

        UpdateHealth();
    }

    public void UpdateHealth()
    {
        // Update the health bar image
        if (healthBar != null)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            healthBar.fillAmount = fillAmount;
        }
    }
}
