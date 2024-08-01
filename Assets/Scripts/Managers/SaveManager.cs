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
                // Di�er oyuncu verilerini de ekleyin
            };

            formatter.Serialize(file, data);
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
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream file = File.Open(filePath, FileMode.Open))
                {
                    PlayerDataSave data = (PlayerDataSave)formatter.Deserialize(file);
                    GameManager.Instance.currentLevel = data.currentLevel;
                    GameManager.Instance.currentWave = data.currentWave;
                    FindObjectOfType<PlayerData>().gold = data.gold;
                    Debug.Log("Save dosyas� okundu.");
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

    private void LoadDefaultValues()
    {
        GameManager.Instance.currentLevel = 1;
        GameManager.Instance.currentWave = 1;
        FindObjectOfType<PlayerData>().gold = 0;
        // Di�er varsay�lan de�erler buraya eklenir
    }

    private string GetFilePath()
    {
        return Application.persistentDataPath + "/player.save";
    }
}
