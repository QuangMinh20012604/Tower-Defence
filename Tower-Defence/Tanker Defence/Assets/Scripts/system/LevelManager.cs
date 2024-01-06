using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    public Transform startPoint;
    public Transform[] path;
    [SerializeField] private Text livesText;

    [Header("Attribute")]
    [SerializeField] private int livesBase = 5;
    public int currency = 250;

    private int lives;
    private bool gameOver = false;
    

    public int Lives 
    {
        get 
        {
            return lives; 
        } 
        set 
        {
            this.lives = value;

            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }

            livesText.text = value.ToString() + "/" + livesBase.ToString();
        } 
    }

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        lives = livesBase;
        livesText.text = lives.ToString() + "/" + livesBase.ToString();
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough to purchase this item");
            return false;
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            SceneManager.LoadScene("GameOver");
        }

    }
}
