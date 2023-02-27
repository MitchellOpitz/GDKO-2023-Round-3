using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject boss;

    private bool spawnStarted = false;

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
        Instantiate(boss);
        spawnStarted = false;
    }
}
