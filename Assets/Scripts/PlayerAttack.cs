using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;   // How far the player can hit
    public int attackDamage = 1;     // Damage per attack

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AttackNearestEnemy();
        }
    }

    void AttackNearestEnemy()
    {
        EnemyHealth nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        // Find all enemies in the scene
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();

        foreach (EnemyHealth enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance && distance <= attackRange)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        // If an enemy is found, damage it
        if (nearestEnemy != null)
        {
            nearestEnemy.TakeDamage(attackDamage);
        }
        else
        {
            Debug.Log("No enemy in range to attack!");
        }
    }
}
