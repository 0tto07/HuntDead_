using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenAndClose : MonoBehaviour
{
    public Animator doorAnimator;
    public Animator objectAnimator;

    private bool playerInside = false;
    private float exitTime = 0f;
    private float delayTime = 1f; // Adjust the delay time as needed

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger animation for the door opening
            doorAnimator.SetTrigger("TRdooropen");

            // Player is inside the trigger area
            playerInside = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player is still inside the trigger area
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player is no longer inside the trigger area
            playerInside = false;
            exitTime = Time.time; // Record the time when the player exits the trigger area
        }
    }

    private void Update()
    {
        // If the player is no longer inside the trigger area and enough time has passed
        if (!playerInside && Time.time - exitTime >= delayTime)
        {
            // Trigger animation for the door closing
            doorAnimator.SetTrigger("TRdoorclose");
        }
    }
}

