using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrakeController : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3f;
    public float killDistance = 2f;
    public int damageAmount = 3;

    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You're too close! Player takes damage!");
            InflictDamage(other.gameObject);
        }
    }

    void InflictDamage(GameObject playerObject)
    {
        PlayerData playerHealth = playerObject.GetComponent<PlayerData>();

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




