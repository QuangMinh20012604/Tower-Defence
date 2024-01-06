using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public static bool GameIsPause = false;

    //public GameObject pauseMusic;

    void Start()
    {
        GameIsPause = false;
        Time.timeScale = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResumeGame()
    {
        //pauseMusic.SetActive(true);

        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void PauseGame()
    {
        //pauseMusic.SetActive(false);

        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void ReplayGameInfo()
    {
        //pauseMusic.SetActive(false);

        Time.timeScale = 0f;
        GameIsPause = true;
    }
    public void VolGameInfo()
    {
        //pauseMusic.SetActive(false);

        Time.timeScale = 0f;
        GameIsPause = true;
    }
    public void HomeInfo()
    {
        //pauseMusic.SetActive(false);

        Time.timeScale = 0f;
        GameIsPause = true;
    }
}
