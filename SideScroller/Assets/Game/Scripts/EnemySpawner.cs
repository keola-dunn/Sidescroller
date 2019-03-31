using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnCount;
    public float spawnRate;
    private float timeToSpawn;
    public GameObject enemyToSpawn;

    // Initialization
    private void Awake()
    {
        spawnCount = 5;
        spawnRate = 10f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > timeToSpawn) {
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            timeToSpawn = Time.time + 10 / spawnRate;
            spawnCount--;
        } 
        if (spawnCount == 0) {
            Destroy(gameObject, 0.1f);
        }
    }
}
