using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spiderlings : MonoBehaviour
{
    public float speed = 5f;

    private Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        int penaltyRank = GameObject.Find("PenaltyHolder").GetComponent<EnemySpeedUp>().currentRank;
        speed *= (float)Math.Pow(1.1f, penaltyRank + 1);
        Debug.Log("Spiderling speed = " + speed);
    }

    private void Update()
    {
        // Calculate direction to player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move towards player
        transform.position += direction * speed * Time.deltaTime;
    }
}