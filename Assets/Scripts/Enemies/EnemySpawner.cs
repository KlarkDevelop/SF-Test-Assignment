using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemie[] enemiesPref;
    [SerializeField] private Transform[] spawnPoints;

    public float delay;
    private float lastSpawnTime;

    private bool isStopped = false;
    private void Awake()
    {
        Player.onDeath += StopSpawner;
        GameStatistic.onGoalReach += DecreasSpawnDelay;
    }

    private void Update()
    {
        if (!isStopped && Time.time - lastSpawnTime > delay)
        {
            SpawnEnemie();
        }
    }

    private void DecreasSpawnDelay()
    {
        if (delay - 0.5f >= 1)
        {
            delay -= 0.5f;
        }
    }

    private void SpawnEnemie()
    {
        int rndId = Random.Range(0, enemiesPref.Length);
        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        Instantiate(enemiesPref[rndId], spawnPoint, Quaternion.identity);

        lastSpawnTime = Time.time;
    }

    private void StopSpawner()
    {
        isStopped = true;
    }
}
