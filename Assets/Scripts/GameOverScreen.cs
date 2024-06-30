using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartButton()
    {

        SceneManager.LoadScene("GameplayScene");
        Time.timeScale = 1;

    }

    public void QuitButton()
    {
        SceneManager.LoadScene("QuitScene");
        Application.Quit();
    }
}