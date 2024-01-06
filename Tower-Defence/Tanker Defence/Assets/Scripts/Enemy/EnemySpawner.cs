using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    AudioManager audioManager;

    [Header("References")]
    [SerializeField] private Text waveText;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject[] enemyPreFabs;


    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 1.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 10f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();


    public int currentWave = 1;
    private float timeSinceLastSpawn;
    public int enemiesAlive;
    public int enemiesLeftToSpawn;
    private float eps; //Enemies Per Second
    private bool isSpawning = false;

    private float currentTime;

    bool timerStarted = false;


    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        WaveText();
        currentTime = timeBetweenWaves;
        timerStarted = true;
        UpdateTimer();
        StartCoroutine(StartWave());
    }

    private void Update()   
    {
        UpdateTimer();
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0) 
        {

            currentTime = timeBetweenWaves;
            EndWave();

        }

        
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        WaveText();
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        int index;
        if (currentWave <= 3)
        {
            index = Random.Range(0, enemyPreFabs.Length - (enemyPreFabs.Length - 1));
        }
        else if (currentWave > 3 && currentWave <= 6)
        {
            index = Random.Range(0, enemyPreFabs.Length - (enemyPreFabs.Length- 2));
        }
        else if (currentWave > 3 && currentWave <= 6)
        {
            index = Random.Range(0, enemyPreFabs.Length - (enemyPreFabs.Length - 3));
        }
        else if (currentWave > 6 && currentWave <= 10)
        {
            index = Random.Range(0, enemyPreFabs.Length - (enemyPreFabs.Length - 4));
        }
        else if (currentWave > 10 && currentWave <= 15)
        {
            index = Random.Range(0, enemyPreFabs.Length - (enemyPreFabs.Length - 5));
        }
        else
        {
            index = Random.Range(0, enemyPreFabs.Length );
        }

        GameObject prefabToSpawn = enemyPreFabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private void WaveText()
    {
        waveText.text = $"Wave: {currentWave}";
    }

    private void UpdateTimer()
    {
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            timerStarted = true;
        }
        if (timerStarted)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                //Debug.Log("timer reached zero");
                timerStarted = false;
                currentTime = 0;
                audioManager.PlaySFX(audioManager.enemyAppear);

            }

            timerText.text = currentTime.ToString("f0");
        }
    }
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(Mathf.RoundToInt(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor)), 0f, enemiesPerSecondCap);
    }

}
