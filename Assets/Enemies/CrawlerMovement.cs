using UnityEngine;
using System;

public class CrawlerMovement : MonoBehaviour
{
    public float speed = 5f;
    Vector2 movement = Vector2.zero;

    private void Start()
    {
        if (GameObject.Find("PenaltyHolder"))
        {
            int penaltyRank = GameObject.Find("PenaltyHolder").GetComponent<EnemySpeedUp>().currentRank;
            speed *= (float)Math.Pow(1.1f, penaltyRank + 1);
        }
    }

    public void Move(string direction)
    {

        switch (direction)
        {
            case "top":
                movement = Vector2.down;
                break;
            case "bottom":
                movement = Vector2.up;
                break;
            case "left":
                movement = Vector2.right;
                break;
            case "right":
                movement = Vector2.left;
                break;
            default:
                Debug.LogWarning($"Invalid direction: {direction}");
                return;
        }

        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void Update()
    {
        if (movement != Vector2.zero)
        {
            transform.Translate(movement * speed * Time.deltaTime);
        }
    }
}
