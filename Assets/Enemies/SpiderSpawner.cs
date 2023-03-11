using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    public GameObject spiderPrefab;
    public float spawnInterval = 2f;
    public bool isCrawler = false;
    public bool isHornet = false;

    private CameraMovement cameraMovement;
    private IEnumerator spawner;
    private float currentLevel;

    private string[] directions = { "top", "bottom", "left", "right" };

    void Start()
    {
        cameraMovement = FindObjectOfType<CameraMovement>();
        StartCoroutine(SpawnMinions());
        currentLevel = FindObjectOfType<GameManager>().level;
    }

    IEnumerator SpawnMinions()
    {
        while (true)
        {
            // Randomly choose a direction to spawn the minion
            string direction = directions[Random.Range(0, directions.Length)];
            Vector2 spawnPosition = Vector2.zero;

            switch (direction)
            {
                case "top":
                    spawnPosition = new Vector2(Random.Range(cameraMovement.cameraXMin, cameraMovement.cameraXMax), cameraMovement.cameraYMax + 5f);
                    break;
                case "bottom":
                    spawnPosition = new Vector2(Random.Range(cameraMovement.cameraXMin, cameraMovement.cameraXMax), cameraMovement.cameraYMin - 5f);
                    break;
                case "left":
                    spawnPosition = new Vector2(cameraMovement.cameraXMin - 5f, Random.Range(cameraMovement.cameraYMin, cameraMovement.cameraYMax));
                    break;
                case "right":
                    spawnPosition = new Vector2(cameraMovement.cameraXMax + 5f, Random.Range(cameraMovement.cameraYMin, cameraMovement.cameraYMax));
                    break;
            }

            if (isCrawler)
            {
                GameObject crawler = Instantiate(spiderPrefab, spawnPosition, Quaternion.identity);
                crawler.GetComponent<CrawlerMovement>().Move(direction);
            }
            if (isHornet)
            {
                GameObject hornet = Instantiate(spiderPrefab, spawnPosition, Quaternion.identity);
                hornet.GetComponent<HornetMovement>().Move(direction);
            }
            else
            {
                Instantiate(spiderPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
