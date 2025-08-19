using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private float speed;

    void Update()
    {
        
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float upgradeSpeed)
    {
        speed = upgradeSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.UpgradeBulletSpeed();
        }
        Destroy(gameObject);
    }
}
}
