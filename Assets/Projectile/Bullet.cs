using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 5;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private float cameraXMin;
    private float cameraXMax;
    private float cameraYMin;
    private float cameraYMax;
    private const float destroyDistance = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set the velocity of the bullet based on its facing direction
        rb.velocity = transform.right * speed;

        // Get camera clamp
        mainCamera = Camera.main;
        CameraMovement cameraMovement = mainCamera.GetComponent<CameraMovement>();

        if (cameraMovement != null)
        {
            cameraXMin = cameraMovement.cameraXMin;
            cameraXMax = cameraMovement.cameraXMax;
            cameraYMin = cameraMovement.cameraYMin;
            cameraYMax = cameraMovement.cameraYMax;
        }
    }

    private void Update()
    {

        if (transform.position.x < cameraXMin - destroyDistance ||
            transform.position.x > cameraXMax + destroyDistance ||
            transform.position.y < cameraYMin - destroyDistance ||
            transform.position.y > cameraYMax + destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the object is an enemy with the Dummy script attached
        if (collision.tag == "Dummy")
        {
            Dummy enemy = collision.GetComponent<Dummy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        } else
        {
            // check if the object is an enemy with the Dummy script attached
            if (collision.tag == "Enemy")
            {
                EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
