using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiderlings : MonoBehaviour
{
    public float speed = 5f;

    private Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        // Calculate direction to player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move towards player
        transform.position += direction * speed * Time.deltaTime;
    }
}