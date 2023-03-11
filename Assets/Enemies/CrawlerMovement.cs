using UnityEngine;
using System;
using System.Collections;

public class CrawlerMovement : MonoBehaviour
{
    public float speed = 5f;
    Vector2 movement = Vector2.zero;
    private string dir;
    private CameraMovement cameraMovement;

    private void Start()
    {
        cameraMovement = FindObjectOfType<CameraMovement>();
        if (GameObject.Find("PenaltyHolder"))
        {
            int penaltyRank = GameObject.Find("PenaltyHolder").GetComponent<EnemySpeedUp>().currentRank;
            speed *= (float)Math.Pow(1.1f, penaltyRank + 1);
        }
    }

    public void Move(string direction)
    {
        dir = direction;
        //Debug.Log("Starting Move: " + direction);
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
                //Debug.LogWarning($"Invalid direction: {direction}");
                RecalculateMovement();
                return;
        }
        StartCoroutine(StartMovement());
    }

    private void RecalculateMovement()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        if (position.y == cameraMovement.cameraYMax + 5f) {
            Move("top");
        }
        if (position.y == cameraMovement.cameraYMin - 5f)
        {
            Move("bottom");
        }
        if (position.x == cameraMovement.cameraXMin - 5f)
        {
            Move("left");
        }
        if (position.x == cameraMovement.cameraXMax + 5f)
        {
            Move("right");
        }
    }

    private void Update()
    {
        if (movement != Vector2.zero)
        {
            transform.Translate(movement * speed * Time.deltaTime);
        } else
        {
            Move(dir);
            transform.Translate(movement * speed * Time.deltaTime);
        }
    }

    IEnumerator StartMovement()
    {
        yield return new WaitForSeconds(.5f);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
