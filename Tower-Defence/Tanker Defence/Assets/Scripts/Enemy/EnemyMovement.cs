using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement main;

    AudioManager audioManager;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    public Transform currentEnemyTarget;
    public Transform EnemyTarget;
    private int pathIndex = 0;
    private float speedFollowerWaves = 0.05f;

    private float baseSpeed;

    private void Awake()
    {
        main = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
 
    }

    private void Start()
    {
        baseSpeed = moveSpeed;
        currentEnemyTarget = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if(Vector2.Distance(currentEnemyTarget.position, transform.position) < 0.1f) 
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length) 
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                LevelManager.main.Lives--;
                audioManager.PlaySFX(audioManager.enemyToPortalOut);
                return;
            }
            else
            {
                currentEnemyTarget = LevelManager.main.path[pathIndex];

            }
            currentEnemyTarget.position = currentEnemyTarget.position;
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (currentEnemyTarget.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
            moveSpeed = newSpeed;
    }

    public void resetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
