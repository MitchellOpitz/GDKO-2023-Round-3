using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    public bool facingRight = true; // Is the player facing right?

    void Update()
    {
        // Get the position of the mouse in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Flip the sprite horizontally based on the position of the mouse
        if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        
        if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Flip the sprite horizontally
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
