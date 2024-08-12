using System.Collections;
using UnityEngine;

public class KnightSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Düþman prefablarýný içeren dizi
    public float spawnInterval = 5f; // Spawn interval süresi (saniye cinsinden)
    public Transform knightParent; // Düþman nesnelerinin ekleneceði parent nesne
    public int quantity;
    public UIManager uiManager;

    public void StartSpawner(bool set)
    {
        quantity = 0;
        if (set)
        {
            if (gameObject.name != "ArcherSpawner")
            {
                StartCoroutine(SpawnEnemy());
            }
        }
        else
        {
            if (gameObject.name != "ArcherSpawner")
            {
                StopCoroutine(SpawnEnemy());
            }
        }
    }
    private IEnumerator SpawnEnemy()
    {
        spawnInterval = 5f;
        GameManager.Instance.unlockShop = true;
        yield return new WaitForSeconds(spawnInterval);
        GameManager.Instance.unlockShop = false;

        while (true)
        {
            quantity++;
            int randomIndex = Random.Range(0, enemyPrefabs.Length); // Rastgele bir prefab seç
            GameObject randomEnemy = enemyPrefabs[randomIndex];
            Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);
            if (quantity >= 5)
            {
                yield return new WaitUntil(() => knightParent.childCount == 0);
                yield return new WaitForSeconds(spawnInterval);
                quantity = 0;
                if (gameObject.name == "RightSpawner")
                {
                    GameManager.Instance.NextWave();
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnSingleEnemy()
    {
        GameObject randomEnemy = enemyPrefabs[0];
        Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);

    }

}
