using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<KnightSpawner> KnightSpawners;
    public Attack attack;
    public SaveManager saveManager;


    public int currentLevel = 1;
    public int currentWave = 1;


    public int totalWavesPerLevel = 3;
    public int enemyHealthMultiplier = 1;
    public int enemyDamageMultiplier = 1;

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
        currentWave++;
        Debug.Log("NextWave");


        if (currentWave > totalWavesPerLevel)
        {
            currentWave = 1;
            NextLevel();

        }
        saveManager.SaveGame();
        //LoadWave(); //KnightSpawner.cs scriptindeki SpawnEnemy() metodu
        //calýsmaya devam ettýgý ýcýn bu fonksýyonu burada calýstýrmýyoruz.
    }

    private void NextLevel()
    {
        Debug.Log("NextLevel");
        currentLevel++;
        totalWavesPerLevel++;
        enemyHealthMultiplier += 1;
        enemyDamageMultiplier += 1;
        // basler yenýlenmelý mý
        //LoadLevel();
    }

    private void LoadLevel()
    {
        saveManager.LoadGame();
        if (KnightSpawners.Count == 2)
        {
            KnightSpawners[0].StartSpawner();
            KnightSpawners[1].StartSpawner();
        }else
        {
            Debug.LogError("KnightSpawners listesi eksik.");
        }

    }

    private void LoadWave()
    {

    }


    // save butonu olmalý mý ??
    private void OnApplicationQuit()
    {
        saveManager.SaveGame();
    }
}
