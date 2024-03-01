using UnityEngine;

public class RockDamage : MonoBehaviour
{
    public int damageAmount = 1;

    // Use Start for initialization
    void Start()
    {
        // Destroy the rock 5 seconds after it has been instantiated
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
            if (playerData != null)
            {
                playerData.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogError("PlayerData component not found on player object.");
            }

            // Destroy the rock immediately after it hits the player
            Destroy(gameObject);
        }
    }
}
