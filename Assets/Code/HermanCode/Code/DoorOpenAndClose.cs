using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenAndClose : MonoBehaviour
{
    public Animator doorAnimator;
    public Animator objectAnimator;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doorAnimator.SetBool("isOpen", true);
        }

        if (collision.gameObject.CompareTag("BOX"))
        {
            doorAnimator.SetBool("isOpen", true);
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
     

        if (collision.gameObject.CompareTag("Player"))
        {
            doorAnimator.SetBool("isOpen", false);
        }
        if (collision.gameObject.CompareTag("BOX"))
        {
            doorAnimator.SetBool("isOpen", false);
        }
    }
}


