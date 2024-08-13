using System.Collections;
using UnityEngine;

public class KnightSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Düþman prefablarýný içeren dizi
    public float spawnInterval = 5f; // Spawn interval süresi (saniye cinsinden)
    public Transform knightParent; // Düþman nesnelerinin ekleneceði parent nesne
    public int quantity;
    public UIManager uiManager;

    bool isSpawning = false;

    public void StartSpawner(bool set)
    {
        quantity = 0;
        if (set)
        {
            if (gameObject.name != "ArcherSpawner")
            {
                isSpawning = false;
                StartCoroutine(SpawnEnemy());
            }
        }
        else
        {
            if (gameObject.name != "ArcherSpawner")
            {
                isSpawning = false;
                StopCoroutine(SpawnEnemy());
            }
        }
    }
    private IEnumerator SpawnEnemy()
    {
        if (isSpawning)
        {
            yield break;
        }
        
        isSpawning = true;
        spawnInterval = 7f;

        GameManager.Instance.unlockShop = true;
        yield return new WaitForSeconds(spawnInterval);
        GameManager.Instance.unlockShop = false;
        
        spawnInterval = 3f;

        while (true)
        {

            yield return new WaitForSeconds(spawnInterval);
            quantity++;
            int randomIndex = Random.Range(0, enemyPrefabs.Length); // Rastgele bir prefab seç
            GameObject randomEnemy = enemyPrefabs[randomIndex];
            Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);
            if (quantity >= GameManager.Instance.enemyPerWave || quantity >= 10)
            {
                yield return new WaitUntil(() => knightParent.childCount == 0);
                quantity = 0;
                if (gameObject.name == "RightSpawner")
                {
                    GameManager.Instance.NextWave();
                }
                break;
            }
        }
    }

    public void SpawnSingleEnemy()
    {
        GameObject randomEnemy = enemyPrefabs[0];
        Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);

    }

}
