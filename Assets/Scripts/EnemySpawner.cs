using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy prefab to spawn
    public Transform player;       // Player reference
    public float spawnInterval = 2f; // Seconds between spawns
    public int maxEnemies = 10;      // Max enemies at a time
    public float spawnRadius = 5f;   // Spawn distance from spawner

    private float spawnTimer;
    private int currentEnemies;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Random position around the spawner
        Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPos.y = transform.position.y; // Keep same height

        // Create enemy
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // Assign player to EnemyFollow script
        EnemyFollow followScript = newEnemy.GetComponent<EnemyFollow>();
        if (followScript != null)
        {
            followScript.player = player;
        }

        currentEnemies++;

        // Optional: Reduce count when enemy dies
        // Assuming your enemy has a health system and calls OnEnemyDeath
        EnemyDeathHandler deathHandler = newEnemy.AddComponent<EnemyDeathHandler>();
        deathHandler.spawner = this;
    }

    // Called by enemies when they die
    public void EnemyDied()
    {
        currentEnemies--;
    }
}
