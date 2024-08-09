using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [SerializeField] public PlayerData playerData;

    private void Awake()
    {
        //deleteSave();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        GameManager gameManager = GameManager.Instance;

        PlayerPrefs.SetInt("currentLevel", gameManager.currentLevel);
        PlayerPrefs.SetInt("currentWave", gameManager.currentWave);
        PlayerPrefs.SetInt("gold", playerData.gold);
        PlayerPrefs.SetInt("armor", playerData.damageable.Armor);
        PlayerPrefs.SetInt("health", playerData.damageable.Health);
        PlayerPrefs.SetInt("baseHealth", gameManager.Bases[0].GetComponent<Damageable>().Health);
        PlayerPrefs.SetInt("base1Health", gameManager.Bases[1].GetComponent<Damageable>().Health);
        PlayerPrefs.SetInt("base2Health", gameManager.Bases[2].GetComponent<Damageable>().Health);
        PlayerPrefs.SetInt("weaponUpgrade", playerData.weaponUpgrade);
        PlayerPrefs.SetInt("archerCount", gameManager.archerCount);
        // Diðer oyuncu verilerini de ekleyin

        PlayerPrefs.Save();
        Debug.Log("Oyun kaydedildi.");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("currentLevel"))
        {
            GameManager gameManager = GameManager.Instance;

            gameManager.currentLevel = PlayerPrefs.GetInt("currentLevel");
            gameManager.currentWave = PlayerPrefs.GetInt("currentWave");
            playerData.gold = PlayerPrefs.GetInt("gold");
            playerData.damageable.Armor = PlayerPrefs.GetInt("armor");
            playerData.damageable.Health = PlayerPrefs.GetInt("health");
            gameManager.Bases[0].GetComponent<Damageable>().Health = PlayerPrefs.GetInt("baseHealth");
            gameManager.Bases[1].GetComponent<Damageable>().Health = PlayerPrefs.GetInt("base1Health");
            gameManager.Bases[2].GetComponent<Damageable>().Health = PlayerPrefs.GetInt("base2Health");
            playerData.weaponUpgrade = PlayerPrefs.GetInt("weaponUpgrade");
           gameManager.archerCount = PlayerPrefs.GetInt("archerCount");


            Debug.Log("Oyun yüklendi.");
        }
        else
        {
            Debug.LogWarning("Save dosyasý bulunamadý.");
            LoadDefaultValues();
        }
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("currentLevel");
        PlayerPrefs.DeleteKey("currentWave");
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("armor");
        PlayerPrefs.DeleteKey("health");
        PlayerPrefs.DeleteKey("baseHealth");
        PlayerPrefs.DeleteKey("base1Health");
        PlayerPrefs.DeleteKey("base2Health");
        PlayerPrefs.DeleteKey("weaponUpgrade");
        PlayerPrefs.DeleteKey("archerCount");
        // Diðer oyuncu verilerini de silin

        PlayerPrefs.Save();
        Debug.Log("Save dosyasý silindi.");
    }

    public void LoadDefaultValues()
    {
        GameManager.Instance.currentLevel = 1;
        GameManager.Instance.currentWave = 1;
        playerData.gold = 0;
        playerData.damageable.Health = 1000;
        playerData.damageable.Armor = 100;
        GameManager.Instance.Bases[0].GetComponent<Damageable>().Health = 1000;
        GameManager.Instance.Bases[1].GetComponent<Damageable>().Health = 500;
        GameManager.Instance.Bases[2].GetComponent<Damageable>().Health = 500;
        GameManager.Instance.archerCount  = 1;
        Debug.Log("Varsayýlan deðerler yüklendi.");
        // Diðer varsayýlan deðerler buraya eklenir
    }
}
