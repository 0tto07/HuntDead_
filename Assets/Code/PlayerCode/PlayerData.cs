using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool isInvincible = false;
    public float invincibilityDurationSeconds = 0.2f;
    public GameManger gameManger;
    private AudioSource SFX_hitSound;

    // Getter method for currentHealth
    public int GetCurrentHealth()
    {
        Debug.Log($"Current health: {currentHealth}");
        return currentHealth;
    }

    void Start()
    {
        SFX_hitSound = GameObject.Find("SFX_hitSound").GetComponent<AudioSource>(); 
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvincible) return;

        currentHealth -= damageAmount;

        // Implement invincibility frames
        if (currentHealth > 0)
        {
            StartCoroutine(BecomeInvincible());
        }
        else
        {
            // Handle player death, such as reloading the scene
            currentHealth = 0;
            gameManger.gameOver();
            Debug.Log("Dead");
        }
    }

    public void Heal(int healAmount)
    {
        // Increase health, but not beyond the maximum
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        // You can add any additional logic related to healing here
    }

    private IEnumerator BecomeInvincible()
    {
        if(SFX_hitSound != null) {
            SFX_hitSound.Play();
        }
            
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        isInvincible = false;
        
        Debug.Log("GetHit");
    }
}