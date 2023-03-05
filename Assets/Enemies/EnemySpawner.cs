using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] bosses;

    private bool spawnStarted = false;
    private int bossNumber = 0;

    private void Update()
    {
        if (!FindObjectOfType<Boss>() && !spawnStarted)
        {
            StartCoroutine(SpawnBoss());
            spawnStarted = true;
        }
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(bosses[bossNumber]);
        spawnStarted = false;
        bossNumber++;

        if (bossNumber == bosses.Length)
        {
            bossNumber = 0;
        }
    }
}
