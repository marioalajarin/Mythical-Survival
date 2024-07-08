using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CameraScripts.EnemySpawner;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeCount = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;

    private EnemySpawner enemySpawner;
    private float lastSpawnRateIncreaseTime = 0f; // To track the last spawn rate increase time
    private float spawnRateIncreaseInterval = 15f; // Interval for spawn rate increase
    private float spawnRateIncreasePercentage = 0.05f; // 5% increase

    private float lastStatIncreaseTime = 0f; // To track the last stat increase time

    private List<AIChase> allEnemies = new List<AIChase>(); // List to keep track of all enemies

    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
        enemySpawner = FindObjectOfType<EnemySpawner>(); // Ensure to find the EnemySpawner component
        allEnemies.AddRange(FindObjectsOfType<AIChase>()); // Initialize with current enemies in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            if (timeCount >= 0 && timeCount < 300)
            {
                timeCount += Time.deltaTime;
                DisplayTime(timeCount);

                if (timeCount - lastSpawnRateIncreaseTime >= spawnRateIncreaseInterval)
                {
                    IncreaseSpawnRate();
                    lastSpawnRateIncreaseTime = timeCount; // Update the last spawn rate increase time
                }

                if (timeCount - lastStatIncreaseTime >= 15f)
                {
                    IncreaseEnemyStats();
                    lastStatIncreaseTime = timeCount; // Update the last stat increase time
                }
            }
            else if (timeCount >= 300)
            {
                SceneManager.LoadScene("WinScene");
                Debug.Log("HAS LLEGADO A LOS 5 MINUTOS HA FINALIZADO LA PARTIDA");
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{00:00} : {01:00}", minutes, seconds);
    }

    void IncreaseSpawnRate()
    {
        enemySpawner.spawnRate *= (1 - spawnRateIncreasePercentage); // Decrease spawn rate interval by 5%
    }

    void IncreaseEnemyStats()
    {
        foreach (var enemy in FindObjectsOfType<EnemyBase>())
        {
            enemy.IncreaseStats(2, 2); // Increase both damage and health by 2
        }
    }

    // Method to add newly spawned enemies to the list
    public void AddEnemy(AIChase newEnemy)
    {
        allEnemies.Add(newEnemy);
    }
}
