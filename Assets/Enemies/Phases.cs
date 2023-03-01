using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phases : MonoBehaviour
{
    public float phase2threshold = .66f;
    public float phase3threshold = .33f;
    private EnemyHealth health;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
    }

    public int PhaseCheck()
    {
        float healthPercent = (float)health.currentHealth / (float)health.maxHealth;

        if (healthPercent < phase3threshold)
        {
            return 3;
        }
        else if (healthPercent < phase2threshold)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
