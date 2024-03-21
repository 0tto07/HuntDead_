using UnityEngine;

public class Spike_from_Brawlstars : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerData playerData = other.GetComponent<PlayerData>();

          
            if (playerData != null)
            {
            
                playerData.TakeDamage(1);
            }
        }
    }
}
