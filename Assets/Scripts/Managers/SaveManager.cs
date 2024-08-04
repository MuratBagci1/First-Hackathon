using DG.Tweening.Core.Easing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }


    [SerializeField] private PlayerData playerData;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SaveGame()
    {
        GameManager gameManager = GameManager.Instance;
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream file = File.Create(GetFilePath()))
        {
            PlayerDataSave data = new PlayerDataSave
            {
                currentLevel = gameManager.currentLevel,
                currentWave = gameManager.currentWave,
                gold = playerData.gold,
                armor = playerData.damageable.Armor,
                health = playerData.damageable.Health,
                baseHealth = gameManager.Bases[0].GetComponent<Damageable>().Health,
                base1Health = gameManager.Bases[1].GetComponent<Damageable>().Health,
                base2Health = gameManager.Bases[2].GetComponent<Damageable>().Health,
                weaponUpgrade = playerData.weaponUpgrade
                // Di�er oyuncu verilerini de ekleyin
            };

            formatter.Serialize(file, data);
            file.Close();
            Debug.Log("Oyun kaydedildi.");
        }
    }

    public void LoadGame()
    {
        string filePath = GetFilePath();
        if (File.Exists(filePath))
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Length == 0)
            {
                Debug.LogWarning("Save dosyas� bo�.");
                LoadDefaultValues();
                return;
            }

            try
            {
                GameManager gameManager = GameManager.Instance;
                 
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream file = File.Open(filePath, FileMode.Open))
                {
                    PlayerDataSave data = (PlayerDataSave)formatter.Deserialize(file);
                    gameManager.currentLevel = data.currentLevel;
                    gameManager.totalWavesPerLevel += data.currentLevel;
                    gameManager.enemyHealthMultiplier += data.currentLevel;
                    gameManager.enemyDamageMultiplier += data.currentLevel;
                    gameManager.currentWave = data.currentWave;
                    playerData.gold = data.gold;
                    playerData.damageable.Armor = data.armor;
                    playerData.damageable.Health = data.health; 
                    gameManager.Bases[0].GetComponent<Damageable>().Health = data.baseHealth;
                    gameManager.Bases[1].GetComponent<Damageable>().Health = data.base1Health;
                    gameManager.Bases[2].GetComponent<Damageable>().Health = data.base2Health;
                    playerData.weaponUpgrade = data.weaponUpgrade;

                    Debug.Log("Save dosyas� okundu.");
                    file.Close();
                }
            }
            catch (SerializationException e)
            {
                Debug.LogError("Save dosyas� okunamad�: " + e.Message);
                LoadDefaultValues();
            }
        }
        else
        {
            Debug.LogWarning("Save dosyas� bulunamad�.");
            LoadDefaultValues();
        }
    }

    public void deleteSave()
    {
        File.Delete(GetFilePath());
    }

    public void LoadDefaultValues()
    {
        GameManager.Instance.currentLevel = 1;
        GameManager.Instance.currentWave = 1;
        playerData.gold = 0;
        GameManager.Instance.Bases[0].GetComponent<Damageable>().Health = 1000;
        GameManager.Instance.Bases[1].GetComponent<Damageable>().Health = 500;
        GameManager.Instance.Bases[2].GetComponent<Damageable>().Health = 500;
        Debug.Log("LoadDefaultValues fonks�yonu cal�st�!!! ");
        // Di�er varsay�lan de�erler buraya eklenir
    }

    private string GetFilePath()
    {
        return Application.persistentDataPath + "/player.save";
    }
}
