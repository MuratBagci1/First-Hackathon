using System.Collections;
using UnityEngine;

public class KnightSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // D��man prefablar�n� i�eren dizi
    public float spawnInterval = 5f; // Spawn interval s�resi (saniye cinsinden)
    public Transform knightParent; // D��man nesnelerinin eklenece�i parent nesne
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
            int randomIndex = Random.Range(0, enemyPrefabs.Length); // Rastgele bir prefab se�
            GameObject randomEnemy = enemyPrefabs[randomIndex];
            Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);
            //if (quantity>=5)
            if (quantity >= 2)
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
    public void SpawnSingleEnemy()
    {

        PlayerData playerData = FindObjectOfType<PlayerData>();
        if (playerData != null)
        {
            if (playerData.gold >= 50)
            {
                quantity++;
                playerData.AddGold(-50);

                GameObject randomEnemy = enemyPrefabs[0];
                Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);

            }
        }
    }
}
