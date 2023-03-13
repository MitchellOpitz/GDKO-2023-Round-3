using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float level = 0;
    public float scoreMultiplier = 1;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;

    private float score = 0;

    public void AddScore(float amount)
    {
        score += Mathf.Floor(amount * scoreMultiplier);
        scoreText.text = "Score: " + score;
        gameOverScoreText.text = score.ToString();
    }
}
