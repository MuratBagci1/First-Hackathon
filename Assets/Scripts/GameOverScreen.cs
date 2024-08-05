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
        Debug.Log("restart a týklandý");
        SaveManager.Instance.LoadDefaultValues();
        SaveManager.Instance.SaveGame();
        GameManager.Instance.LoadLevel();
        Time.timeScale = 1f;
        UIManager.isGamePaused = false;
        SceneManager.LoadScene("GameplayScene");
        Time.timeScale = 1f;


    }

    public void QuitButton()
    {
        Debug.Log("Quitting game...");
        SaveManager.Instance.SaveGame();
        Application.Quit();
    }
}