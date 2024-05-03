using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public float speed;
    private float followRadius = 10.0f;

    Animator myAnimator;
    AudioManager myAudioManager;

    private bool facingRight = true;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between enemy and player
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= followRadius)
        {
            // Move towards the player
            myAnimator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Check if the enemy is moving left or right and flip accordingly
            if (transform.position.x < target.position.x && !facingRight)
            {
                Flip();
            }
            else if (transform.position.x > target.position.x && facingRight)
            {
                Flip();
            }
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }
    }

    void Flip()
    {
      
        facingRight = !facingRight;

      
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
            if (playerData != null)
            {
                playerData.TakeDamage(1);
            }
        }
    }
}


