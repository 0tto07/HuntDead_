using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public LockpickGame lockpickGame; // Assign this via the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure the player has a tag "Player"
        {
            lockpickGame.ActivateGame();
        }
    }
}