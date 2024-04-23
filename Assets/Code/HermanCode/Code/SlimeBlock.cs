using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
   
    public float shortAirTimeBounceForce = 300f;
    public float mediumAirTimeBounceForce = 500f;
    public float longAirTimeBounceForce = 700f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            float thePlayerAirTime = playerMovement.GetAirTime();

            float bounceForce = 0f;

            if (thePlayerAirTime < 0.25f) 
            {
                Debug.Log("Shortest bounce");
                bounceForce = shortAirTimeBounceForce;
            }
            else if (thePlayerAirTime < 0.5f) 
            {
                Debug.Log("Little longer bounce");
                bounceForce = mediumAirTimeBounceForce;
            }
            else 
            {
                Debug.Log("Longest bounce");
                bounceForce = longAirTimeBounceForce;
            }

            
            Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 bounceDirection = Vector2.up; 
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f); 
            playerRigidbody.AddForce(bounceDirection * bounceForce);
        }
    }
}





