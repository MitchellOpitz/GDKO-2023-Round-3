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
