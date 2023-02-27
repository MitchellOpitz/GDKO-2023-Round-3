using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    private bool facingRight = true; // Is the player facing right?

    void Update()
    {
        // Get input value for horizontal movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        // Flip the player sprite if the input direction changes
        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
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
