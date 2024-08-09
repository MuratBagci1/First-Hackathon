using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<KnightSpawner> KnightSpawners;
    public KnightSpawner ArcherSpawner;
    public Attack attack;
    public SaveManager saveManager;
    public List<GameObject> Bases;
    public PopUp popUp;

    public bool gameRestarted = false;

    public int currentLevel = 1;
    public int currentWave = 1;


    public int totalWavesPerLevel = 1;
    public int enemyHealthMultiplier = 0;
    public int enemyDamageMultiplier = 0;

    public int archerCount = 1;

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

    public void Initializer()
    {
        KnightSpawners = FindObjectsOfType<KnightSpawner>().ToList<KnightSpawner>();
        for (int i = 0; i < KnightSpawners.Count; i++)
        {
            if (KnightSpawners[i].name == "ArcherSpawner")
            {
                ArcherSpawner = KnightSpawners[i];
                KnightSpawners.RemoveAt(i);
            }
        }
        Bases = GameObject.FindGameObjectsWithTag("Base").ToList<GameObject>();
        for (int i = 0; i < Bases.Count; i++)
        {
            if (Bases[i].name == "Base")
            {
                GameObject temp = Bases[i];
                Bases[i] = Bases[0];
                Bases[0] = temp;
            }
        }
       

        //gameRestarted = false;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (gameRestarted)
        {
            Initializer();
            LoadLevel();

            if (totalWavesPerLevel != 1)
            {
                if (currentLevel > 1)
                {
                    totalWavesPerLevel += currentLevel;
                    enemyHealthMultiplier += currentLevel;
                    enemyDamageMultiplier += currentLevel;
                }
            }
            saveManager.playerData.GameLoaded();
            for (int i = 1; i < archerCount; i++)
            {
                ArcherSpawner.SpawnSingleEnemy(buy: false);
            }
            gameRestarted = false;
            Time.timeScale = 1f;
        }
    }

    public void NextWave()
    {
        if (currentWave > totalWavesPerLevel)
        {
            currentWave = 1;
            NextLevel();
        }
        else
        {
            currentWave++;
        }

        Debug.Log("NextWave");

        if (!popUp.isRunning)
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
        currentLevel += 1;
        saveManager.SaveGame();

        Debug.Log("NextLevel");

        if (!popUp.isRunning)
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
        if (!popUp.isRunning)
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
