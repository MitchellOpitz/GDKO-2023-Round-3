using UnityEngine;
using System;

public class HornetMovement : MonoBehaviour
{
    public float speed = 3f;
    public float amplitude = 1f; // the height of the sine wave
    public float frequency = 1f; // the frequency of the sine wave
    private Vector2 direction = Vector2.zero;
    private float time = 0f;

    private string passDirection;

    private void Start()
    {
        if (GameObject.Find("PenaltyHolder"))
        {
            int penaltyRank = GameObject.Find("PenaltyHolder").GetComponent<EnemySpeedUp>().currentRank;
            speed *= (float)Math.Pow(1.1f, penaltyRank + 1);
        }
    }

    public void Move(string dir)
    {
        passDirection = dir;
        switch (dir)
        {
            case "top":
                direction = Vector2.down;
                break;
            case "bottom":
                direction = Vector2.up;
                break;
            case "left":
                direction = Vector2.right;
                break;
            case "right":
                direction = Vector2.left;
                break;
            default:
                Debug.LogWarning($"Invalid direction: {dir}");
                return;
        }
    }

    private void Update()
    {
        if (direction != Vector2.zero)
        {
            // calculate the position offset using a sine wave
            float offset = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * time);

            // update the object's position using the sine wave offset and the current direction
            if(passDirection == "top" || passDirection == "bottom")
            {
                transform.Translate((direction + new Vector2(offset, 0)) * speed * Time.deltaTime);
            } else
            {
                transform.Translate((direction + new Vector2(0, offset)) * speed * Time.deltaTime);
            }

            // update the time for the next sine wave calculation
            time += Time.deltaTime;
        }
    }
}
