using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public EnemySpawner spawner;

    // Example method to simulate enemy death
    public void Die()
    {
        if (spawner != null)
            spawner.EnemyDied();

        Destroy(gameObject);
    }
}
