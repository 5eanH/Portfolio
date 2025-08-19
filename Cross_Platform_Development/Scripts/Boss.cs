using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int health = 10; 
    private bool hasStopped = false;
    private GameManager gameManager;

    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float fireRate = 2f;
    private float nextFireTime = 0f;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 5f;
    
    public int bossIndex = 1;
    private bool movingUp = true; 
    private bool isAlive = true; 

    void Start()
    {
        AdjustDifficulty(); 
        StartCoroutine(MoveToPosition());
    }

    void AdjustDifficulty()
    {
        if (bossIndex == 2)
        {
            fireRate = 1.5f; 
            bulletSpeed = 12f;
        }
        else if (bossIndex == 3)
        {
            fireRate = 1.0f; 
            bulletSpeed = 15f;
        }
    }

    IEnumerator MoveToPosition()
    {
        Vector3 targetPosition = new Vector3(transform.position.x - 7f, transform.position.y, transform.position.z);

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        hasStopped = true; 
        StartCoroutine(ShootBullets()); 

        if (bossIndex == 3)
        {
            StartCoroutine(MoveUpAndDown()); 
        }
    }

    IEnumerator ShootBullets()
    {
        while (isAlive) 
        {
            if (hasStopped && Time.time >= nextFireTime)
            {
                ShootBullet();  
                nextFireTime = Time.time + fireRate;
            }
            yield return null;
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab == null || shootingPoint == null) return;

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null) return;

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        Destroy(bullet, bulletLifetime);
    }

    IEnumerator MoveUpAndDown()
    {
        float moveDistance = 3f;
        float moveSpeedY = 2f;

        while (isAlive) 
        {
            float targetY = movingUp ? transform.position.y + moveDistance : transform.position.y - moveDistance;

            while (Mathf.Abs(transform.position.y - targetY) > 0.1f && isAlive)
            {
                transform.position += new Vector3(0, (movingUp ? 1 : -1) * moveSpeedY * Time.deltaTime, 0);
                yield return null;
            }

            movingUp = !movingUp; 
            yield return new WaitForSeconds(0.5f); 
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (!isAlive) return;

        isAlive = false; 
        if (gameManager != null) 
        {
            gameManager.BossKilled(); 
        }
        Destroy(gameObject);
        EnemyMovement.IncreaseDifficulty();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet")) 
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }
}