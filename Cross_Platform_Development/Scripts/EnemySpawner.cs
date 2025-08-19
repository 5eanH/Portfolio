using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; //6 enemy
    public Transform spawnPoint; 
    public float spawnRate = 2f; 

    private bool canSpawn = false;

    public void StartSpawning()
    {
        if (!canSpawn)
        {
            canSpawn = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }

    IEnumerator SpawnEnemies()
    {
        while (canSpawn)
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        //pick random offset
        float randomHeight = Random.Range(-5f, 5f);

        // Set spawn pos random height
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y + randomHeight, spawnPoint.position.z);

        GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);

        //enemyIndex to right value
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.enemyIndex = randomIndex;
        }

        yield return new WaitForSeconds(spawnRate);
    }
    }
}