using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{


    private void Start()
    {
        
    }

    void Update()
    {

    }


    public void RePlay()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void SceneMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SceneMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
