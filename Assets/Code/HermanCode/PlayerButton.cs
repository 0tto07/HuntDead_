using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPressButton : MonoBehaviour
{
    public int buttonIndex; // Index of the button (assuming each button has a unique index)
    public PatternMatchingPuzzle patternMatchingPuzzle; // Reference to the pattern matching puzzle script

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Notify the pattern matching puzzle that this button is pressed
            patternMatchingPuzzle.PlayerPressedButton(buttonIndex);
        }
    }
}


