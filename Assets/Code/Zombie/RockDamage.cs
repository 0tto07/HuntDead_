using UnityEngine;

public class RockDamage : MonoBehaviour
{
    public int damageAmount = 1;

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

            Destroy(gameObject); // Destroy the rock after it hits something
        }
    }
}
