using UnityEngine;

public class BossParticleController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float maxEmissionRate = 10f;
    public float bossHealthPercentageThreshold = 0.5f;

    private EnemyHealth health;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
        particleSystem.Play(); // Stop the particle system initially
    }

    private void Update()
    {
        float bossHealthPercentage = (float)health.currentHealth / (float)health.maxHealth; /* Get the boss health percentage */
        float targetEmissionRate = 0f;
        if (bossHealthPercentage < bossHealthPercentageThreshold)
        {
            // Scale emission rate from 0 to maxEmissionRate
            float healthPercentageMissing = (bossHealthPercentageThreshold - bossHealthPercentage) / bossHealthPercentageThreshold;
            targetEmissionRate = Mathf.Lerp(0f, maxEmissionRate, healthPercentageMissing);
        }

        var emission = particleSystem.emission;
        var rateOverTime = emission.rateOverTime;
        rateOverTime.constant = targetEmissionRate;
        emission.rateOverTime = rateOverTime;
    }

    // Call this method when the boss loses all its health
    public void OnBossDefeated()
    {
        // Stop the particle system
        particleSystem.Stop();
    }
}
