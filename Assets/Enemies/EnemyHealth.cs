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
    public GameObject healthBar;
    private GameManager gameManager;
    private AudioManager audioManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        maxHealth = (int)(maxHealth * (float)Math.Pow(1 + 0.1, gameManager.level));
        currentHealth = maxHealth;
        audioManager = FindObjectOfType<AudioManager>();
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
            SetDead();
            healthBar.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            FindObjectOfType<GameManager>().level++;
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.ChangeTrack(0, .75f);
            StartCoroutine(StartUpgradePanel());
            gameManager.AddScore(points);
        } else
        {
            gameManager.AddScore(points);
            Destroy(gameObject);
        }
    }

    IEnumerator StartUpgradePanel()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Boss>().StartUpgrades();
        Destroy(gameObject);
    }

    private void SetDead()
    {
        if (gameObject.GetComponent<Boss1Attacks>())
        {
            gameObject.GetComponent<Boss1Attacks>().isDead = true;
        }

        if (gameObject.GetComponent<CentipedeShooting>())
        {
            gameObject.GetComponent<CentipedeShooting>().isDead = true;
        }

        if (gameObject.GetComponent<HornetAttacks>())
        {
            gameObject.GetComponent<HornetAttacks>().isDead = true;
        }
    }
}
