using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlock : MonoBehaviour
{
    public float initialBounceForce = 10f;
    public float bounceForceIncrement = 5f;
    public float minVelocityY = 0.1f;

    private float currentBounceForce;
    private bool isBouncing;

    private void Start()
    {
        currentBounceForce = initialBounceForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 bounceDirection = Vector2.up * currentBounceForce;
            rb.velocity = bounceDirection;


            currentBounceForce += bounceForceIncrement;


            isBouncing = true;
        }


        //Get the airtime value from player to calculate the jumpforce
        if (collision.gameObject.CompareTag("Player"))
        {
            float thePlayerAirTime = collision.gameObject.GetComponent<PlayerMovement>().airTime;
            Debug.Log(thePlayerAirTime);
        }
    }
}





