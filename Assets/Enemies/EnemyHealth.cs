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
    public AudioClip clip;

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
        if (gameObject.GetComponent<Boss>())
        {
            maxHealth = (int)(maxHealth * (float)Math.Pow(1 + 0.05, gameManager.level));
        } else
        {
            int rank = GameObject.Find("PenaltyHolder").GetComponent<MinionHealth>().currentRank;
            maxHealth = (int)(maxHealth * (float)Math.Pow(1 + 0.10, rank));
        }
        currentHealth = maxHealth;
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Update()
    {
        if(transform.position.y > 10 ||
            transform.position.y < -35 ||
            transform.position.x > 60 ||
            transform.position.x < -10)
        {
            Destroy(gameObject);
        }
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
            Boss[] bosses = FindObjectsOfType<Boss>();
            SetDead();
            healthBar.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BossParticleController>().OnBossDefeated();
            if(name == "CentipedeBody(Clone)")
            {
                transform.FindChild("RubbleParticles").GetComponent<ParticleSystem>().Stop();
            }
            FindObjectOfType<GameManager>().level++;
            if(bosses.Length == 1)
            {
                audioManager = FindObjectOfType<AudioManager>();
                audioManager.ChangeTrack(0, .5f);
                CheckRestoreHealth();
            }
            StartCoroutine(StartUpgradePanel());
            gameManager.AddScore(points);
            GameObject.Find("SoundFX").GetComponent<AudioSource>().PlayOneShot(clip);
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

    private void CheckRestoreHealth()
    {
        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        int rank = GameObject.Find("UpgradeHolder").GetComponent<HealthPerLevel>().currentRank;
        if(rank == 1)
        {
            if(player.currentHealth < maxHealth - 2)
            {
                player.currentHealth++;
            }
        }
    }
}
