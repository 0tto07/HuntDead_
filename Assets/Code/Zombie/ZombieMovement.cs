using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust this to control the speed of the enemy

    private Transform player;  // Reference to the Player's transform

    private void Start()
    {
        // Find the GameObject with the "Player" tag at the start
        player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Move the enemy towards the player
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}