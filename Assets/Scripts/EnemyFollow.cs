using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;   // Assign player transform in Inspector
    public float moveSpeed = 3f; // Speed of enemy movement
    public float stopDistance = 1.5f; // Distance to stop from the player

    void Update()
    {
        if (player != null)
        {
            // Calculate the distance between enemy and player
            float distance = Vector3.Distance(transform.position, player.position);

            // If enemy is farther than stopDistance, move toward the player
            if (distance > stopDistance)
            {
                // Move toward the player
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    player.position,
                    moveSpeed * Time.deltaTime
                );

                // Make the enemy face the player
                Vector3 direction = (player.position - transform.position).normalized;
                transform.forward = new Vector3(direction.x, 0, direction.z);
            }
        }
    }
}
