using UnityEngine;

public class CentipedeShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shootAngle = 35f;
    public float bulletSpeed = 20f;

    private float fireRate = 2f;
    private float fireTimer = 0f;
    private CentipedeMovement movement;

    private Phases phase;

    private void Start()
    {
        movement = GetComponent<CentipedeMovement>();
        phase = GetComponent<Phases>();
    }

    private void Update()
    {
        if (phase.PhaseCheck() != 1)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
    }

    private void Shoot()
    {
        if (!movement.flipped)
        {
            Quaternion leftRotation = Quaternion.Euler(0f, 0f, shootAngle);
            Quaternion rightRotation = Quaternion.Euler(0f, 0f, -shootAngle);
            Quaternion left2Rotation = Quaternion.Euler(0f, 0f, shootAngle/2);
            Quaternion right2Rotation = Quaternion.Euler(0f, 0f, -shootAngle/2);

            // Spawn the left bullet
            GameObject leftBullet = Instantiate(bulletPrefab, firePoint.position, leftRotation);
            leftBullet.GetComponent<Rigidbody2D>().velocity = leftRotation * firePoint.right * bulletSpeed;

            // Spawn the right bullet
            GameObject rightBullet = Instantiate(bulletPrefab, firePoint.position, rightRotation);
            rightBullet.GetComponent<Rigidbody2D>().velocity = rightRotation * firePoint.right * bulletSpeed;

            // Spawn the left bullet
            GameObject left2Bullet = Instantiate(bulletPrefab, firePoint.position, left2Rotation);
            left2Bullet.GetComponent<Rigidbody2D>().velocity = left2Rotation * firePoint.right * bulletSpeed;

            // Spawn the right bullet
            GameObject right2Bullet = Instantiate(bulletPrefab, firePoint.position, right2Rotation);
            right2Bullet.GetComponent<Rigidbody2D>().velocity = right2Rotation * firePoint.right * bulletSpeed;
        } else
        {
            Quaternion leftRotation = Quaternion.Euler(0f, 0f, shootAngle + 180f);
            Quaternion rightRotation = Quaternion.Euler(0f, 0f, -shootAngle + 180f);
            Quaternion left2Rotation = Quaternion.Euler(0f, 0f, (shootAngle / 2) + 180f);
            Quaternion right2Rotation = Quaternion.Euler(0f, 0f, (-shootAngle / 2) + 180f);

            // Spawn the left bullet
            GameObject leftBullet = Instantiate(bulletPrefab, firePoint.position, leftRotation);
            leftBullet.GetComponent<Rigidbody2D>().velocity = leftRotation * firePoint.right * bulletSpeed;

            // Spawn the right bullet
            GameObject rightBullet = Instantiate(bulletPrefab, firePoint.position, rightRotation);
            rightBullet.GetComponent<Rigidbody2D>().velocity = rightRotation * firePoint.right * bulletSpeed;

            // Spawn the left bullet
            GameObject left2Bullet = Instantiate(bulletPrefab, firePoint.position, left2Rotation);
            left2Bullet.GetComponent<Rigidbody2D>().velocity = left2Rotation * firePoint.right * bulletSpeed;

            // Spawn the right bullet
            GameObject right2Bullet = Instantiate(bulletPrefab, firePoint.position, right2Rotation);
            right2Bullet.GetComponent<Rigidbody2D>().velocity = right2Rotation * firePoint.right * bulletSpeed;
        }
    }
}
