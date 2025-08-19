using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameAnalytics.NewDesignEvent("Bullet:Hit");

            GameManager.instance.bulletsHit++;

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}