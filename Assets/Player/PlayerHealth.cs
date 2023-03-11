using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;

    private HealthBar healthBar;
    private bool isInvulnerable = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.maxHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.currentHealth = currentHealth;
        if (isInvulnerable)
        {
            float blinkInterval = 0.2f;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = (Mathf.Sin(Time.time * 10f) > 0f) ? Color.clear : Color.white;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            isInvulnerable = true;
            StartCoroutine(FlashPlayer());
            healthBar.UpdateHealth(currentHealth);
        }
    }
    IEnumerator FlashPlayer()
    {
        float duration = 1.5f;
        float flashInterval = 0.2f;
        float timer = 0f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while (timer < duration)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(flashInterval);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval * 2;
        }

        isInvulnerable = false;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            TakeDamage(1);
            if (!collider.gameObject.GetComponent<Boss>())
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
