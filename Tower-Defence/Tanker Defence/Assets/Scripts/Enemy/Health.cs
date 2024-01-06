using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    AudioManager audioManager;


    [Header("References")]
    public Image HealthBar;

    [Header("Attributes")]
    [SerializeField] private int hitPoint = 2;
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;
    private float healthAmount;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        healthAmount = hitPoint;
    }

    public void TakeDamage(int dmg)
    {
        hitPoint -= dmg;
        HealthBar.fillAmount = hitPoint / healthAmount;

        if (hitPoint <= 0 && !isDestroyed)
        {

            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
            audioManager.PlaySFX(audioManager.death);
        }
    }
}
