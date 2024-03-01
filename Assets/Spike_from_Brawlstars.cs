using UnityEngine;

public class Spike_from_Brawlstars : MonoBehaviour
{
    // Ensure OnCollisionEnter2D is a method of the MonoBehaviour class
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
            if (playerData != null)
            {
                playerData.Heal(-1); // Assuming '1' is the healing amount
            }
            else
            {
                Debug.LogError("PlayerData component not found on player object.");
            }
        }
    }

    // Other methods or logic for the Healing class
}