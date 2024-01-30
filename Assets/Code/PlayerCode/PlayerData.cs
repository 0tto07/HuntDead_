using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool isInvincible = false;
    public float invincibilityDurationSeconds = 0.5f;

    void Start()
    {
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        isInvincible = false;
    }
}
