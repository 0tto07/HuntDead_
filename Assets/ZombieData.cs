using UnityEngine;

public class ZombieData : MonoBehaviour
{
    public int hitPoints = 3; // Initial hitpoints of the zombie

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collider that hit the zombie is of the "Arrow" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            // Decrease hitpoint by 1
            hitPoints--;
            Debug.Log($"Hit by Arrow. Remaining hitpoints: {hitPoints}");

            // Check if hitpoints are 0 or less, then destroy the zombie
            if (hitPoints <= 0)
            {
                Debug.Log("Zombie destroyed.");
                Destroy(gameObject); // Destroy the zombie gameObject
            }
        }
    }
}
