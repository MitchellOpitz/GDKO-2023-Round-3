using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public Transform gunTip;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f;
    public int damage = 5;
    public int upgradeRank = 0;
    public GameObject bang;
    public float directionOffset;
    public float offset;

    private Animator animator;
    private PlayerDirection dir;

    private float nextFireTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dir = GetComponent<PlayerDirection>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            Fire(damage);
            animator.SetBool("isShooting", true);
            StartCoroutine(AdjustParameterAfterFrames(12, "isShooting", false));
        }

        // Aim the gun towards the mouse cursor with an offset angle of 90 degrees
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gunTip.rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);

    }

    IEnumerator AdjustParameterAfterFrames(int framesToWait, string parameterName, bool value)
    {
        for (int i = 0; i < framesToWait; i++)
        {
            yield return null;
        }

        animator.SetBool(parameterName, value);
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
            if (dir.facingRight)
            {
                Instantiate(bang, startPosition, Quaternion.Euler(0, 0, 0));
            } else
            {
                Vector3 newPosition = startPosition;
                newPosition.x -= directionOffset;
                Instantiate(bang, newPosition, Quaternion.Euler(0, 180f, 0));
            }
            projectile.GetComponent<Bullet>().damage = damageAmount;
            projectile.transform.position = new Vector3(projectile.transform.position.x, projectile.transform.position.y, 0f);

            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = projectile.transform.right * projectileSpeed;
        }
    }
}