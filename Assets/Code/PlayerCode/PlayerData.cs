using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health
    private int currentHealth;  // Current health

    void Start()
    {
        currentHealth = maxHealth; // Initialize health to maxHealth
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1); // When 'T' key is pressed, take 10 damage (you can adjust this value)
        }
    }

    // Function to apply damage to the player
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Check if the player's health has reached zero or below
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Ensure health doesn't go below zero
            // Reset the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}