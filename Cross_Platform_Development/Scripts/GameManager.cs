using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameAnalyticsSDK;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] bossPrefabs;
    public Transform bossSpawnPoint;
    public EnemySpawner enemySpawner;
    private int enemiesKilled = 0;
    private bool isBossActive = false;
    private int bossCounter = 0;
    private float timeSinceLastBoss = 0f;
    private Boss currentBoss;

    public GameObject levelCompleteUI;
    public GameObject upgradePrefab;
    public Transform upgradeSpawnPoint;
    private int nextUpgradeThreshold = 100;
    public float upgradeSpeed = 5f;
    public PlayerController player;

    public int score = 0;
    public TMP_Text scoreText;
    public int coinValue = 10;

    public float enemySpeed = 5f;
    public float backgroundSpeed = 2f;

    public TextMeshProUGUI highScoreText;

    public int bulletsFired = 0;
    public int bulletsHit = 0;

    private string filePath;
    private int highScore = 0;

    void Start()
    {
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE
        Screen.orientation = ScreenOrientation.LandscapeLeft;
#endif

#if !UNITY_EDITOR
        GameAnalytics.Initialize();
#endif

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level", "1");

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        filePath = Path.Combine(Application.persistentDataPath, "highscore.txt");
        LoadHighScore();

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
        enemySpawner.StartSpawning();
    }

    void Update()
    {
        if (!isBossActive && bossCounter > 0 && bossCounter < bossPrefabs.Length)
        {
            timeSinceLastBoss += Time.deltaTime;
            if (timeSinceLastBoss >= 40f)
            {
                SpawnBoss();
            }
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        GameAnalytics.NewDesignEvent("Enemy:Killed");

        if (enemiesKilled >= 10 && bossCounter == 0)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        if (bossCounter < bossPrefabs.Length && !isBossActive)
        {
            isBossActive = true;
            timeSinceLastBoss = 0f;

            enemySpawner.StopSpawning();

            GameObject bossObj = Instantiate(bossPrefabs[bossCounter], bossSpawnPoint.position, Quaternion.identity);
            currentBoss = bossObj.GetComponent<Boss>();

            currentBoss.SetGameManager(this);

            GameAnalytics.NewDesignEvent("Boss:Spawned:" + bossCounter);
            bossCounter++;
        }
    }

    public void BossKilled()
    {
        isBossActive = false;
        enemiesKilled = 0;

        UpdateEnemySpeeds();
        UpdateBackgroundSpeed();

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Boss", "Boss_" + bossCounter);
        GameAnalytics.NewDesignEvent("Achievement:FirstBossDefeated");

        if (bossCounter >= bossPrefabs.Length)
        {
            LevelComplete();
        }
        else
        {
            enemySpawner.StartSpawning();
        }
    }

    public void LevelComplete()
    {
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
        }

        SaveHighScore();

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }

        GameAnalytics.NewDesignEvent("Level:Complete");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level", "1");
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Coins", score, "Level", "Completed");

        if (bulletsFired > 0)
        {
            float accuracy = (float)bulletsHit / bulletsFired;
            GameAnalytics.NewDesignEvent("Player:Accuracy", accuracy);
        }
    }

    private void OnApplicationQuit()
    {
        SaveHighScore();
    }

    void UpdateEnemySpeeds()
    {
        enemySpeed *= 2f;
        var enemies = FindObjectsOfType<EnemyMovement>();
        foreach (var enemy in enemies)
        {
            enemy.speed = enemySpeed;
        }
    }

    void UpdateBackgroundSpeed()
    {
        backgroundSpeed *= 2f;
        BackgroundScrolling[] backgrounds = FindObjectsOfType<BackgroundScrolling>();
        foreach (var bg in backgrounds)
        {
            bg.speed = backgroundSpeed;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Coins", amount, "Pickup", "EnemyDrop");

        if (score >= 100)
        {
            GameAnalytics.NewDesignEvent("Achievement:100CoinsCollected");
        }

        if (score >= nextUpgradeThreshold && nextUpgradeThreshold <= 300)
        {
            SpawnUpgrade();
            nextUpgradeThreshold += 100;
        }
    }

    void SpawnUpgrade()
    {
        Vector3 spawnPos = new Vector3(upgradeSpawnPoint.position.x, Random.Range(-3f, 3f), 0);
        GameObject upgrade = Instantiate(upgradePrefab, spawnPos, Quaternion.identity);
        upgrade.GetComponent<Upgrade>().SetSpeed(upgradeSpeed);

        GameAnalytics.NewDesignEvent("Upgrade:Spawned:" + nextUpgradeThreshold);
    }

    public void UpgradeBulletSpeed()
    {
        if (player != null)
        {
            player.UpgradeFireRate();
            GameAnalytics.NewDesignEvent("Player:Upgrade:BulletSpeed");
        }
    }

    void SaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            File.WriteAllText(filePath, highScore.ToString());
            Debug.Log("New high score saved: " + highScore);
        }
    }

    void LoadHighScore()
    {
        if (File.Exists(filePath))
        {
            string savedScore = File.ReadAllText(filePath);
            int.TryParse(savedScore, out highScore);
            Debug.Log("High score loaded: " + highScore);
        }
        else
        {
            highScore = 0;
            Debug.Log("No high score file found. Starting fresh.");
        }
    }

    public void ResetGameValues()
    {
        
        score = 0;
        enemiesKilled = 0;
        isBossActive = false;
        bossCounter = 0;
        timeSinceLastBoss = 0f;
        nextUpgradeThreshold = 100;
        enemySpeed = 5f;
        backgroundSpeed = 2f;
        bulletsFired = 0;
        bulletsHit = 0;

        if (player != null)
        {
            player.ResetFireRate();
            player.ResetPlayerPosition();
            player.ResetHealth();
        }

        EnemyMovement.ResetEnemyStats();

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (highScoreText != null)
        {
            highScoreText.text = "";
        }

        Debug.Log("Game values have been reset.");
    }
}