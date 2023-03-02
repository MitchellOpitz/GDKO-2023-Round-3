using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform gunTip;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f;
    public int damage = 5;
    public int upgradeRank = 0;

    public Animator animator;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            Fire(damage);
        }

        // Aim the gun towards the mouse cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gunTip.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Fire(int damageAmount)
    {
        nextFireTime = Time.time + fireRate;
        Vector3 startPosition = gunTip.position;
        Quaternion startRotation = Quaternion.Euler(0, 0, gunTip.rotation.eulerAngles.z);

        for (int i = 0; i <= upgradeRank; i++)
        {
            float spreadAngle = -10f + (10f * i);
            Quaternion spreadRotation = Quaternion.Euler(0, 0, gunTip.rotation.eulerAngles.z + spreadAngle);
            GameObject projectile = Instantiate(projectilePrefab, startPosition, spreadRotation);
            projectile.GetComponent<Bullet>().damage = damageAmount;
            projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y, 0f);

            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = projectile.transform.right * projectileSpeed;
        }
    }
}

/*

    void Fire(int damageAmount)
    {
        nextFireTime = Time.time + fireRate;
        GameObject projectile = Instantiate(projectilePrefab, gunTip.position, Quaternion.Euler(0, 0, gunTip.rotation.eulerAngles.z));
        projectile.GetComponent<Bullet>().damage = damageAmount;
        projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y, 0f);

        // Set the velocity of the projectile based on the gun tip's right vector
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.velocity = gunTip.right * projectileSpeed;
    }
*/