using UnityEngine;

public class Healing : MonoBehaviour
{
    // Ensure OnCollisionEnter2D is a method of the MonoBehaviour class
    private void OnCollisionEnter2D(Collision2D collision)
    {

        
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
            if (playerData != null)
            {
                playerData.Heal(1); // Assuming '1' is the healing amount
                Destroy(gameObject); // Remove the healing object
            }
            else
            {
                Debug.LogError("PlayerData component not found on player object.");
            }
        
    }

    // Other methods or logic for the Healing class
}