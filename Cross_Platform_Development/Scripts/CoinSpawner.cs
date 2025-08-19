using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;  
    public Transform spawnPoint;   
    public float spawnRate = 2f;   
    public float coinSpeed = 3f;  

    private bool canSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        while (canSpawn)
        {
            // Randomize height
            float randomY = Random.Range(-5f, 5f); 
            Vector2 spawnPosition = new Vector2(10f, randomY); 

            GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            Coin coinScript = coin.GetComponent<Coin>();
            coinScript.SetSpeed(coinSpeed);  

            yield return new WaitForSeconds(spawnRate);
        }
    }
}