using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public static float fireRate = 1.5f;
    public static float bulletSpeed = 6f;

    private GameManager gameManager;
    private Transform player;
    private bool canShoot = true;
    public int enemyIndex = -1;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindWithTag("Player").transform;

        if (enemyIndex == 5) 
        {
            canShoot = false;
            speed = 12f;
        }

        if (canShoot)
        {
            StartCoroutine(AutoShoot());
        }
    }

    void FixedUpdate()
    {
        if (enemyIndex != 5)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            MoveTowardsPlayer();
        }

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    IEnumerator AutoShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Shoot();
        }
    }

    void Shoot()
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.left * bulletSpeed; 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameManager.EnemyKilled();
        }
    }

    public static void IncreaseDifficulty()
    {
        fireRate *= 0.8f; // Increase firing speed 
        bulletSpeed *= 1.5f; // Increase bulletspeed 
    }

    public static void ResetEnemyStats()
    {
        fireRate = 1.5f; // Reset to default fire rate
        bulletSpeed = 6f; // Reset to default bullet speed
        Debug.Log("Enemy stats reset to default.");
    }
}