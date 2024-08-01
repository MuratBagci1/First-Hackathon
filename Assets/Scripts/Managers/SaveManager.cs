using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        FileStream file = File.Create(GetFilePath());

        PlayerDataSave data = new PlayerDataSave
        {
            currentLevel = GameManager.Instance.currentLevel,
            currentWave = GameManager.Instance.currentWave,
            gold = FindObjectOfType<PlayerData>().gold,
            // Diðer oyuncu verilerini de ekleyin
        };

        formatter.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(GetFilePath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(GetFilePath(), FileMode.Open);

            PlayerDataSave data = (PlayerDataSave)formatter.Deserialize(file);
            file.Close();

            GameManager.Instance.currentLevel = data.currentLevel;
            GameManager.Instance.currentWave = data.currentWave;
            FindObjectOfType<PlayerData>().gold = data.gold;
            // Diðer oyuncu verilerini de yükleyin
        }
    }

    private string GetFilePath()
    {
        return Application.persistentDataPath + "/player.save";
    }
}
