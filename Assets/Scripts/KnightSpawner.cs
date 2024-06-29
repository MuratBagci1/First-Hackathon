using System.Collections;
using UnityEngine;

public class KnightSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // D��man prefablar�n� i�eren dizi
    public float spawnInterval = 5f; // Spawn interval s�resi (saniye cinsinden)
    public Transform knightParent; // D��man nesnelerinin eklenece�i parent nesne

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length); // Rastgele bir prefab se�
            GameObject randomEnemy = enemyPrefabs[randomIndex];
            Instantiate(randomEnemy, transform.position, Quaternion.identity, knightParent);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
