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

    private bool waiting;
    private Vector3 playerDirection;
    private Transform player;
    private int p2AttackNumber;
    private int p3AttackNumber;
    private Animator animator;

    private void Start()
    {
        p2AttackNumber = 1;
        p3AttackNumber = 1;
        waiting = false;
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();

        int penaltyRank = GameObject.Find("PenaltyHolder").GetComponent<EnemySpeedUp>().currentRank;
        speed *= (float)Math.Pow(1.1f, penaltyRank + 1);
    }

    // Update is called once per frame
    void Update()
    {        
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
            Instantiate(minion, firePoint.position, Quaternion.identity);

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
            transform.position += playerDirection * (speed * speedMultiplier) * Time.deltaTime;
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
            Vector3 shotDirection = (player.position - firePoint.position).normalized;

            // Instantiate bullet prefab and set its direction
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            EnemyBullets bullet = bulletObject.GetComponent<EnemyBullets>();
            bullet.direction = shotDirection;

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
