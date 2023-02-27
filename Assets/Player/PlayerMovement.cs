using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // The player's movement speed
    public Animator animator;

    void Update()
    {
        // Get input values for movement on the X and Z axes
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f).normalized;

        // Apply movement to the player object
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Set the animator speed
        float movementAbs = Mathf.Max(Mathf.Abs(moveHorizontal), Mathf.Abs(moveVertical));
        if (movementAbs > 0)
        {
            animator.SetFloat("Speed", movementAbs);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }
}
