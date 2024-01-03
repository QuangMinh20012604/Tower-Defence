using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image HealthBar;
    public float healthAmount = 100f;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        HealthBar.fillAmount = healthAmount / 100f;
    }
}
