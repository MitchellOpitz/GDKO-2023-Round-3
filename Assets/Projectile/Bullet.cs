using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 1f;
    public int damage = 5;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set the velocity of the bullet based on its facing direction
        rb.velocity = transform.right * speed;

        // Destroy the bullet after its lifetime has elapsed
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the object is an enemy with the Dummy script attached
        Dummy enemy = collision.GetComponent<Dummy>();
        if (enemy != null)
        {
            // apply damage to the enemy and destroy the bullet
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
