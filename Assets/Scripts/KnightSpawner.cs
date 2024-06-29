using System.Collections;
using UnityEngine;

public class KnightSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Düþman prefablarýný içeren dizi
    public float spawnInterval = 5f; // Spawn interval süresi (saniye cinsinden)
    public Transform knightParent; // Düþman nesnelerinin ekleneceði parent nesne

    private void Start()
    {
        Debug.Log(gameObject.name);
        if (gameObject.name != "ArcherSpawner")
        {
        StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length); // Rastgele bir prefab seç
            GameObject randomEnemy = enemyPrefabs[randomIndex];
            Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public void SpawnSingleEnemy()
    {
        // Oyuncuyu bul ve altýn azalt
        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (playerData != null)            
        {
            if (playerData.gold >= 50)
            {
                playerData.AddGold(-50);

                GameObject randomEnemy = enemyPrefabs[0];
                Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);

            }
        }


         
    }
}
