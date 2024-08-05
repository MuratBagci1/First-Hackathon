using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<KnightSpawner> KnightSpawners;
    public Attack attack;
    public SaveManager saveManager;
    public List<GameObject> Bases;
    public PopUp popUp;


    public int currentLevel = 1;
    public int currentWave = 1;


    public int totalWavesPerLevel = 1;
    public int enemyHealthMultiplier = 0;
    public int enemyDamageMultiplier = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadLevel();
    }

    public void NextWave()
    {
        if (currentWave > totalWavesPerLevel)
        {
            currentWave = 1;
            NextLevel();
        }

        currentWave++;
        Debug.Log("NextWave");

        if(!popUp.isRunning)
        {
            popUp.gameObject.SetActive(true);
            popUp.StartCoroutine(popUp.WavePopUp(currentWave.ToString()));
        }     
        //LoadWave(); //KnightSpawner.cs scriptindeki SpawnEnemy() metodu
        //calýsmaya devam ettýgý ýcýn bu fonksýyonu burada calýstýrmýyoruz.
    }

    private void NextLevel()
    {
        totalWavesPerLevel += 1;
        enemyHealthMultiplier += 1;
        enemyDamageMultiplier += 1;
        currentLevel++;
        saveManager.SaveGame();

        Debug.Log("NextLevel");
        
        if(!popUp.isRunning)
        {
            popUp.gameObject.SetActive(true);
            popUp.StartCoroutine(popUp.LevelPopUp(currentLevel.ToString(), currentWave.ToString()));
        }
        // basler yenýlenmelý mý
        //LoadLevel();
    }

    public void LoadLevel()
    {
     
        saveManager.LoadGame();
        if(!popUp.isRunning)
        {
            popUp.gameObject.SetActive(true);
            popUp.StartCoroutine(popUp.LevelPopUp(currentLevel.ToString(), currentWave.ToString()));
        }
        if (KnightSpawners.Count == 2)
        {
            KnightSpawners[0].StartSpawner();
            KnightSpawners[1].StartSpawner();
        }
        else
        {
            Debug.LogError("KnightSpawners listesi eksik.");
        }
    }

    private void LoadWave()
    {
        popUp.gameObject.SetActive(true); 
        popUp.StartCoroutine(popUp.WavePopUp(currentWave.ToString()));

    }


    // save butonu olmalý mý ??
    private void OnApplicationQuit()
    {
        saveManager.SaveGame();
    }
}
