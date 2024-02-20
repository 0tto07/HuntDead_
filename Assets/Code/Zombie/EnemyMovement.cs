using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public float speed;
    private float followRadius = 510.0f;
    private bool isFacingRight = true;
    private float horizontal;

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

            // Calculate horizontal movement
            horizontal = target.position.x - transform.position.x;

            // Flip the enemy if necessary
            Flip();
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
    }

    private void Flip()
    {
        // Determine the direction the enemy is moving in based on horizontal input
        float movementDirection = Mathf.Sign(horizontal);

        // If the movement direction is positive and the enemy is not facing right, or
        // if the movement direction is negative and the enemy is facing right
        if ((movementDirection > 0f && !isFacingRight) || (movementDirection < 0f && isFacingRight))
        {
            // Toggle the facing direction
            isFacingRight = !isFacingRight;

            // Flip the local scale along the x-axis to change direction
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}