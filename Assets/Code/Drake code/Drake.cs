using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrakeController : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3f;
    public float killDistance = 2f;
    public int damageAmount = 3; // Amount of damage to inflict on the player

    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Check if the player is too slow
        if (Mathf.Abs(player.position.x - transform.position.x) < killDistance)
        {
            Debug.Log("You're too slow! Player takes damage!");

            // Inflict damage on the player
            InflictDamage();
        }
    }

    void InflictDamage()
    {
        // Assuming the player has a Health script attached
        PlayerData playerHealth = player.GetComponent<PlayerData>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }
        else
        {
            Debug.LogWarning("Player does not have a Health script attached!");
        }
    }
}


