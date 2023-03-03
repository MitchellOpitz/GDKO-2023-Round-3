using UnityEngine;

public class CrawlerMovement : MonoBehaviour
{
    public float speed = 5f;
    Vector2 movement = Vector2.zero;

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
