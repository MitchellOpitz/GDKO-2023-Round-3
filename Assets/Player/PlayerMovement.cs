using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // The player's movement speed
    public Animator animator;

    // Variables for clamping movement
    public Camera mainCamera;
    private float cameraXMin, cameraXMax, cameraYMin, cameraYMax;

    void Start()
    {
        // Get the camera clamping values from the Camera script
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float halfWidth = spriteRenderer.bounds.size.x / 2.0f;
        float halfHeight = spriteRenderer.bounds.size.y / 2.0f;
        cameraXMin = mainCamera.GetComponent<CameraMovement>().cameraXMin + halfWidth;
        cameraXMax = mainCamera.GetComponent<CameraMovement>().cameraXMax - halfWidth;
        cameraYMin = mainCamera.GetComponent<CameraMovement>().cameraYMin + halfHeight;
        cameraYMax = mainCamera.GetComponent<CameraMovement>().cameraYMax - halfHeight;
    }

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

        ClampMovement();
    }

    void ClampMovement()
    {
        // Clamp player movement to camera clamping values
        float clampedX = Mathf.Clamp(transform.position.x, cameraXMin, cameraXMax);
        float clampedY = Mathf.Clamp(transform.position.y, cameraYMin, cameraYMax);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
