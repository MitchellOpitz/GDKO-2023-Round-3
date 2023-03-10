using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss1Attacks : MonoBehaviour
{
    public float speed;
    public float moveDuration;
    public float pauseTime;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject minion;
    public CameraMovement cam;
    public float offsetAngle;
    public bool isDead;

    private bool waiting;
    private Vector3 playerDirection;
    private Transform player;
    private int p2AttackNumber;
    private int p3AttackNumber;
    private Animator animator;
    private AudioManager audioManager;

    private void Start()
    {
        isDead = false;
        p2AttackNumber = 1;
        p3AttackNumber = 1;
        waiting = false;
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        cam = FindObjectOfType<CameraMovement>();
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeTrack(1, 1f);

        int penaltyRank = GameObject.Find("PenaltyHolder").GetComponent<EnemySpeedUp>().currentRank;
        speed *= (float)Math.Pow(1.1f, penaltyRank + 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 playerDirection = player.position - transform.position;
            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

            // Rotate firePoint to aim at player
            firePoint.rotation = Quaternion.Euler(0f, 0f, angle);

            switch (GetComponent<Phases>().PhaseCheck())
            {
                case 1:
                    Phase1Attack();
                    break;
                case 2:
                    Phase2();
                    break;
                case 3:
                    Phase3();
                    break;
            }
        }
    }

    private void Phase1Attack()
    {
        if (!waiting)
        {
            waiting = true;
            TargetPlayer();
            StartCoroutine(MoveTowardsPlayer(1f));
            StartCoroutine(Fire(3));
            StartCoroutine(WaitReset(3f));
        }
    }

    private void Phase2()
    {
        if (!waiting)
        {
            waiting = true;
            if (p2AttackNumber == 1)
            {
                waiting = false;
                Phase2Attack();
            } else
            {
                waiting = false;
                Phase1Attack();
            }
            p2AttackNumber++;
        }
        
        if (p2AttackNumber == 5)
        {
            p2AttackNumber = 1;
        }
    }

    private void Phase2Attack()
    {
        waiting = true;
        TargetPlayer();
        StartCoroutine(MoveTowardsPlayer(3f));
        StartCoroutine(WaitReset(3f));
    }

    private void Phase3()
    {
        if (!waiting)
        {
            waiting = true;
            if (p3AttackNumber == 1)
            {
                waiting = false;
                Phase3Attack();
            }
            else if (p3AttackNumber == 3)
            {
                waiting = false;
                Phase2Attack();
            } else
            {
                waiting = false;
                Phase1Attack();
            }
            p3AttackNumber++;
        }

        if (p3AttackNumber == 5)
        {
            p3AttackNumber = 1;
        }
    }

    private void Phase3Attack()
    {
        waiting = true;
        StartCoroutine(SpawnMinions(3));
        StartCoroutine(WaitReset(3f));
    }

    IEnumerator SpawnMinions(int numberOfMinions)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < numberOfMinions; i++)
        {
            if (!isDead)
            {
                Instantiate(minion, firePoint.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void TargetPlayer()
    {
        playerDirection = (player.position - transform.position).normalized;
    }

    IEnumerator MoveTowardsPlayer(float speedMultiplier)
    {
        // Move towards player for moveDuration
        animator.SetFloat("Speed", speed);
        float timer = 0f;
        while (timer < moveDuration)
        {
            if (transform.position.x < cam.cameraXMin + 2)
            {
                transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, 0f);
            } else if (transform.position.x > cam.cameraXMax - 2)
            {
                transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, 0f);
            }
            else if (transform.position.y > cam.cameraYMax - 2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, 0f);
            }
            else if (transform.position.y < cam.cameraYMin + 2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, 0f);
            } else
            {
                transform.position += playerDirection * (speed * speedMultiplier) * Time.deltaTime;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        animator.SetFloat("Speed", 0f);
    }
    IEnumerator Fire(int numberOfShots)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < numberOfShots; i++)
        {
            // Calculate direction to player
            animator.SetBool("isShooting", true);
            StartCoroutine(AdjustParameterAfterFrames(12, "isShooting", false));
            Vector3 playerDirection = player.position - transform.position;
            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            if(transform.rotation.y == 0)
            {
                angle += (offsetAngle * (angle / 90));
            } else
            {
                angle -= (offsetAngle * (angle / -90));
            }

            // Rotate firePoint to aim at player
            firePoint.rotation = Quaternion.Euler(0f, 0f, angle);

            // Instantiate bullet prefab and set its direction
            if (!isDead)
            {
                GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                EnemyBullets bullet = bulletObject.GetComponent<EnemyBullets>();
                bullet.direction = playerDirection.normalized;
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(pauseTime);
    }

    IEnumerator WaitReset(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        waiting = false;
    }

    IEnumerator AdjustParameterAfterFrames(int framesToWait, string parameterName, bool value)
    {
        for (int i = 0; i < framesToWait; i++)
        {
            yield return null;
        }

        animator.SetBool(parameterName, value);
    }
}
