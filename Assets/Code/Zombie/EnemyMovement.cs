using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public float speed;
    private float followRadius = 510.0f;
    public int health = 3; // Adding a health property for the zombie

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between enemy and player
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Check if the distance is within the follow radius
        if (distanceToTarget <= followRadius)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
            if (playerData != null)
            {
                playerData.TakeDamage(1);
            }
            else
            {
                Debug.LogError("PlayerData component not found on player object.");
            }
        }
        else if (collision.gameObject.CompareTag("Arrow")) // Check for collision with Arrow
        {
            TakeDamage(1); // Calling the TakeDamage method
        }
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    // Method to handle the zombie's death
    private void Die()
    {
        Destroy(gameObject); // Destroys the zombie game object
    }
}
