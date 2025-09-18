using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   //simple script for enemy spawner
    public Transform spawnPoint;
    public float spawnInterval = 3f;
    public int spawnLimit = 5;

    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()    //sets the time between spawns based on the interval set by whats hardcoded(3) or what is chanegs in the inspector
    {
        spawnTimer = spawnTimer - Time.deltaTime;
        if (spawnLimit > 0)
        {
            if (spawnTimer <= 0f)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval;
                spawnLimit--;
            }
        }
    }

    void SpawnEnemy()   //creates a new enemy of whateaver prefab is connected in the inspector
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
