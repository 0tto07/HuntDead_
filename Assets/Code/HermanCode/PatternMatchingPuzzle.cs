using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatternMatchingPuzzle : MonoBehaviour
{
    public List<GameObject> patternButtons; // List of buttons representing the pattern
    public GameObject playerObject; // Reference to the player object
    public GameObject successEffect; // Effect to play when the puzzle is successfully solved

    private List<int> correctPattern = new List<int>(); // Correct pattern sequence
    private List<int> playerPattern = new List<int>(); // Player's input sequence

    void Start()
    {
        // Generate a random pattern
        GeneratePattern();
    }

    void GeneratePattern()
    {
        // Generate a random pattern of button presses (for demonstration, it's randomly generated here)
        for (int i = 0; i < patternButtons.Count; i++)
        {
            int buttonIndex = Random.Range(0, patternButtons.Count);
            correctPattern.Add(buttonIndex);
        }
    }

    public void PlayerPressedButton(int buttonIndex)
    {
        // Add the index of the pressed button to the player's pattern
        playerPattern.Add(buttonIndex);

        // Check if the player's pattern matches the correct pattern
        if (playerPattern.Count == correctPattern.Count)
        {
            bool isCorrect = true;
            for (int i = 0; i < correctPattern.Count; i++)
            {
                if (playerPattern[i] != correctPattern[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                // Puzzle is successfully solved
                Debug.Log("Puzzle Solved!");
                Instantiate(successEffect, transform.position, Quaternion.identity);

                // Perform actions related to the player object
                if (playerObject != null)
                {
                    // Example: Activate a component on the player object
                    playerObject.GetComponent<PlayerMovement>().EnableSpecialAbility();
                }

                // Reset the player's pattern for the next attempt
                playerPattern.Clear();

                // Generate a new pattern for the next attempt
                GeneratePattern();
            }
            else
            {
                // Incorrect pattern, reset the player's input
                Debug.Log("Incorrect Pattern! Try again.");
                playerPattern.Clear();
            }
        }
    }
}


