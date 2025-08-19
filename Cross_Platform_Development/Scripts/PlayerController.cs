using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameAnalyticsSDK;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float touchSensitivity = 0.03f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float fireRate = 0.5f;

    [Header("UI")]
    public TMP_Text healthText;

    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("References")]
    public Camera mainCamera;

    private float screenHeight;
    private float nextFireTime = 0f;
    private GameManager gameManager;
    private Vector3 startingPosition;

    void Start()
    {
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE
        Screen.orientation = ScreenOrientation.LandscapeLeft;
#endif

        gameManager = FindObjectOfType<GameManager>();
        mainCamera = Camera.main;
        screenHeight = mainCamera.orthographicSize;
        currentHealth = maxHealth;

        startingPosition = transform.position;

        UpdateHealthText();
        StartCoroutine(AutoShoot());
    }

    void Update()
    {
        HandleTouchInput();
        HandleKeyboardInput();
    }

    void LateUpdate()
    {
        UpdateHealthText();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthText();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + " / " + maxHealth;
        }
    }

    void Die()
    {
        Vector3 deathPos = transform.position;
        GameAnalytics.NewDesignEvent($"Player:Death:Position:{deathPos.x}:{deathPos.y}");

        Destroy(gameObject);
        gameManager.LevelComplete();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float moveAmount = touch.deltaPosition.y * touchSensitivity;
                float newY = transform.position.y + moveAmount * speed * Time.deltaTime;
                newY = Mathf.Clamp(newY, -screenHeight, screenHeight);
                transform.position = new Vector2(transform.position.x, newY);
            }
        }
    }

    void HandleKeyboardInput()
    {
        float verticalMovement = 0f;

        if (Input.GetKey(KeyCode.W)) verticalMovement = 1f;
        if (Input.GetKey(KeyCode.S)) verticalMovement = -1f;

        float newY = transform.position.y + verticalMovement * speed * Time.deltaTime;
        newY = Mathf.Clamp(newY, -screenHeight, screenHeight);
        transform.position = new Vector2(transform.position.x, newY);
    }

    IEnumerator AutoShoot()
    {
        while (true)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            yield return null;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(10f, 0f);

        gameManager.bulletsFired++;
        GameAnalytics.NewDesignEvent("Bullet:Fired");
    }

    public void UpgradeFireRate()
    {
        if (fireRate > 0.1f)
        {
            fireRate /= 2f;
        }
    }
    public void ResetFireRate()
    {
        fireRate = 0.5f;
        Debug.Log("Fire rate reset to default.");
    }

    public void ResetPlayerPosition()
    {
        transform.position = startingPosition;
        Debug.Log("Player position reset to start.");
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        Debug.Log("Player health reset to full.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(10);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("BossBullet"))
        {
            TakeDamage(20);
            Destroy(other.gameObject);
        }
    }
}