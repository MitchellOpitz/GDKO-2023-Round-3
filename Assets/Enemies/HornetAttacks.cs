using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetAttacks : MonoBehaviour
{
    public float speed;
    public float pauseTime;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float moveRadius = 10f;
    public float numAttacks = 3;

    private bool waiting;
    private Vector3 playerDirection;
    private Transform player;
    private int p1AttackNum;
    private int p2AttackNum;
    private int p3AttackNumber;

    private void Start()
    {
        p3AttackNumber = 1;
        p2AttackNum = 1;
        waiting = false;
        player = GameObject.Find("Player").transform;

        int penaltyRank = GameObject.Find("PenaltyHolder").GetComponent<EnemySpeedUp>().currentRank;
        speed *= (float)System.Math.Pow(1.1f, penaltyRank + 1);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GetComponent<Phases>().PhaseCheck())
        {
            case 1:
                Phase1();
                break;
            case 2:
                Phase2();
                break;
            case 3:
                Phase3();
                break;
        }
    }

    private void Phase1()
    {
        if (!waiting)
        {
            waiting = true;
            p1AttackNum = 1;
            Phase1Attack();
            StartCoroutine(WaitReset(6f));
        }
    }

    private void Phase1Attack()
    {
        // Choose a random position within attack radius
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 randomPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * moveRadius;

        // Move to the random position
        StartCoroutine(MoveToPosition(randomPosition));
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float t = 0f;
        Vector3 startingPosition = transform.position;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null;
        }

        StartCoroutine(FireAtPlayer(1));

    }
    IEnumerator FireAtPlayer(int shotsToFire)
    {
        for (int shotsFired = 0; shotsFired < shotsToFire; shotsFired++)
        {
            yield return new WaitForSeconds(pauseTime);

            // Aim at player
            Vector3 playerDirection = (player.position - firePoint.position).normalized;
            float angle = Mathf.Atan2(playerDirection.x, playerDirection.z) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));

            // Fire bullet
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.direction = playerDirection;
        }

        yield return new WaitForSeconds(.5f);
        if(p1AttackNum < 3)
        {
            Phase1Attack();
            p1AttackNum++;
        }
    }
    public IEnumerator Phase2Attack()
    {
        waiting = true;
        for (int i = 0; i < 5; i++)
        {
            Vector3 shotDirection = Quaternion.Euler(0, -15 + i * 7.5f, 0) * (player.position - firePoint.position).normalized;

            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.direction = shotDirection;
        }
        StartCoroutine(WaitReset(1f));
        yield return new WaitForSeconds(1f);
        p2AttackNum++;
    }

    private void Phase2()
    {
        if (!waiting)
        {
            waiting = true;
            if (p2AttackNum == 1)
            {
                waiting = false;
                StartCoroutine(Phase2Attack());
            }
            else
            {
                waiting = false;
                Phase1();
            }
        }

        if (p2AttackNum == 2)
        {
            p2AttackNum = 1;
        }
    }

    private void Phase3()
    {
        if (!waiting)
        {
            waiting = true;
            if (p3AttackNumber == 1)
            {
                waiting = false;
                StartCoroutine(Phase3Attack());

            }
            else if (p3AttackNumber == 3)
            {
                waiting = false;
                StartCoroutine(Phase2Attack());
            }
            else
            {
                waiting = false;
                Phase1();
            }
            p3AttackNumber++;
        }

        if (p3AttackNumber == 5)
        {
            p3AttackNumber = 1;
        }
    }

    IEnumerator Phase3Attack()
    {
        waiting = true;
        for (int i = 0; i < 30; i++)
        {
            // Calculate direction to player with sine wave
            Vector3 shotDirection = (player.position - firePoint.position).normalized;
            float sineOffset = Mathf.Sin((float)i / 10f * Mathf.PI) * 30f;
            shotDirection = Quaternion.Euler(0f, sineOffset, 0f) * shotDirection;

            // Instantiate bullet prefab and set its direction
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.direction = shotDirection;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        waiting = false;
    }

    IEnumerator Fire(int numberOfShots)
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < numberOfShots; i++)
        {
            // Calculate direction to player
            Vector3 shotDirection = (player.position - firePoint.position).normalized;

            // Instantiate bullet prefab and set its direction
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
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
}
