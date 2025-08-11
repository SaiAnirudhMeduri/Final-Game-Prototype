using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private EnemyDeathHandler deathHandler;

    void Start()
    {
        currentHealth = maxHealth;
        deathHandler = GetComponent<EnemyDeathHandler>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathHandler != null)
            deathHandler.Die();
        else
            Destroy(gameObject);
    }
}
