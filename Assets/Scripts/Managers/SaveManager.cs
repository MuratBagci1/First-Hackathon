using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
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
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream file = File.Create(GetFilePath()))
        {
            PlayerDataSave data = new PlayerDataSave
            {
                currentLevel = GameManager.Instance.currentLevel,
                currentWave = GameManager.Instance.currentWave,
                gold = FindObjectOfType<PlayerData>().gold,
                armor = (int)FindObjectOfType<PlayerData>().damageable.Armor,
                health = (int)FindObjectOfType<PlayerData>().damageable.Health,
                weaponUpgrade = FindObjectOfType<PlayerData>().weaponUpgrade
                // Diðer oyuncu verilerini de ekleyin
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
                Debug.LogWarning("Save dosyasý boþ.");
                LoadDefaultValues();
                return;
            }

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream file = File.Open(filePath, FileMode.Open))
                {
                    PlayerDataSave data = (PlayerDataSave)formatter.Deserialize(file);
                    GameManager.Instance.currentLevel = data.currentLevel;
                    GameManager.Instance.currentWave = data.currentWave;
                    FindObjectOfType<PlayerData>().gold = data.gold;
                    FindObjectOfType<PlayerData>().damageable.Armor = data.armor;
                    FindObjectOfType<PlayerData>().damageable.Health = data.health;
                    FindObjectOfType<PlayerData>().weaponUpgrade = data.weaponUpgrade;
                    Debug.Log("Save dosyasý okundu.");
                    file.Close();
                }
            }
            catch (SerializationException e)
            {
                Debug.LogError("Save dosyasý okunamadý: " + e.Message);
                LoadDefaultValues();
            }
        }
        else
        {
            Debug.LogWarning("Save dosyasý bulunamadý.");
            LoadDefaultValues();
        }
    }

    public void deleteSave()
    {
        File.Delete(GetFilePath());
    }

    private void LoadDefaultValues()
    {
        GameManager.Instance.currentLevel = 1;
        GameManager.Instance.currentWave = 1;
        FindObjectOfType<PlayerData>().gold = 0;
        // Diðer varsayýlan deðerler buraya eklenir
    }

    private string GetFilePath()
    {
        return Application.persistentDataPath + "/player.save";
    }
}
