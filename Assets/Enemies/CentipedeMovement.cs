using UnityEngine;

public class CentipedeMovement : MonoBehaviour
{
    public float speed = 5f;
    public float xMin = -10f;
    public float xMax = 10f;
    public float yMin = -10f;
    public float yMax = 10f;
    public Transform firePoint;

    private EnemyHealth enemyHealth;
    private float baseSpeed;
    private float xDirection = 1f; // start moving to the right
    private float yDirection = 1f; // start moving to the right
    private float nextYPos = 0f; // the next y position to move to
    private float yPos = 0f; // the current y position
    public bool flipped;
    private AudioManager audioManager;
    private Animator animator;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        baseSpeed = speed;
        flipped = false;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeTrack(3, 1f);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        speed = baseSpeed * (2 - ((float)enemyHealth.currentHealth / (float)enemyHealth.maxHealth));
        float xPos = transform.position.x;

        // Move left/right within the boundaries
        xPos += speed * xDirection * Time.deltaTime;
        if (xPos > xMax || xPos < xMin)
        {
            // If we hit a boundary, move down and reverse direction
            xPos = Mathf.Clamp(xPos, xMin, xMax);
            xDirection *= -1f;
            UpdateFirePoint();
            animator.SetFloat("xDirection", xDirection);
            yPos = nextYPos;
            nextYPos = yPos + Mathf.Sign(yDirection) * 2f;
        }
        
        // Move up/down within the boundaries
        yPos = Mathf.MoveTowards(yPos, nextYPos, speed * Time.deltaTime);
        if (yPos > yMax || yPos < yMin)
        {
            // If we hit a boundary, reverse direction
            yPos = Mathf.Clamp(yPos, yMin, yMax);
            yDirection *= -1f;
            nextYPos = yPos + Mathf.Sign(yDirection) * 2f;
        }

        // Update the position of the GameObject
        transform.position = new Vector3(xPos, yPos, 0f);
    }

    void UpdateFirePoint()
    {
        Vector3 firePointLocalPos = firePoint.localPosition;
        firePointLocalPos = new Vector3(-firePointLocalPos.x, firePointLocalPos.y, firePointLocalPos.z);
        firePoint.localPosition = firePointLocalPos;
        if (!flipped)
        {
            firePoint.rotation = Quaternion.Euler(0f, 180f, 0f);
            flipped = true;
        } else
        {
            firePoint.rotation = Quaternion.Euler(0f, 0f, 0f);
            flipped = false;
        }

    }
}
