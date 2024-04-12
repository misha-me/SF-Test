using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform leftspawnPosition;
    [SerializeField] Transform rightspawnPosition;

    [SerializeField] List<GameObject> enemyPrefabs;

    [SerializeField] float defaultSpawnDelay;
    float spawnDelay;
    [SerializeField] float minSpawnDelay;

    Coroutine spawnerCoroutine;

    private void Start()
    {
        StartSpawner();
    }

    public void StartSpawner()
    {
        StopSpawner();
        spawnerCoroutine = StartCoroutine(SpawnerCoroutine());
        spawnDelay = defaultSpawnDelay;
    }

    public void StopSpawner()
    {
        if (spawnerCoroutine != null)
        {
            StopCoroutine(spawnerCoroutine);
        }
    }

    private GameObject GetRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);

        return enemyPrefabs[randomIndex];
    }

    private Vector2 GetRandomSpawnPosition()
    {
        int randomInt = Random.Range(0, 2);

        if(randomInt == 0)
            return leftspawnPosition.position;

        return rightspawnPosition.position;
    }

    private void SpawnEnemy()
    {
        Instantiate(GetRandomEnemy(), GetRandomSpawnPosition(), Quaternion.identity);
    }

    IEnumerator SpawnerCoroutine()
    {
        while(true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(spawnDelay);

            if(spawnDelay > minSpawnDelay)
                spawnDelay -= .1f;
        }
    }
}
