using System.Collections;
using UnityEngine;

public class KnightSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Düþman prefablarýný içeren dizi
    public float spawnInterval = 5f; // Spawn interval süresi (saniye cinsinden)
    public Transform knightParent; // Düþman nesnelerinin ekleneceði parent nesne
    public int quantity;

    public void StartSpawner()
    {
        quantity = 0;
        if (gameObject.name != "ArcherSpawner")
        {
            StartCoroutine(SpawnEnemy());
        }
    }
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            quantity++;
            int randomIndex = Random.Range(0, enemyPrefabs.Length); // Rastgele bir prefab seç
            GameObject randomEnemy = enemyPrefabs[randomIndex];
            Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);
            if (quantity >= 5)
            {
                spawnInterval = 20f;
                yield return new WaitForSeconds(spawnInterval);
                spawnInterval = 5f;
                quantity = 0;
                if (gameObject.name == "RightSpawner")
                {
                    GameManager.Instance.NextWave();
                }
            }
            else
            {
                spawnInterval = 5f;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnSingleEnemy(bool buy = true, int price = 0)
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (playerData != null)
        {
            if (!buy)
            {
                GameObject randomEnemy = enemyPrefabs[0];
                Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);
            }
            else
            {
                if (playerData.gold >= price)
                {
                    GameManager.Instance.archerCount++;
                    playerData.AddGold(-price);

                    GameObject randomEnemy = enemyPrefabs[0];
                    Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);

                }
            }

        }
    }
    
}
