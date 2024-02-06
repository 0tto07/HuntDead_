using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // Set this in the Unity editor to control how much the button moves down
    public float depressionAmount = 0.5f;

    private Vector2 initialPosition;
    private bool isDepressed = false;

    void Start()
    {
        // Store the initial position of the button
        initialPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is a box
        if (collision.gameObject.CompareTag("Box"))
        {
            // If the button is not already depressed, depress it
            if (!isDepressed)
            {
                transform.position -= Vector3.up * depressionAmount;
                isDepressed = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Reset the button when the box is removed
        if (collision.gameObject.CompareTag("Box"))
        {
            transform.position = initialPosition;
            isDepressed = false;
        }
    }
}

