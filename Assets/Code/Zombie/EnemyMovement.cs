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

    private bool isNearToPlayer;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
       
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= followRadius)
        {

            if(distanceToTarget <= 1) {

                isNearToPlayer = true;
                myAnimator.SetBool("isPunching", true);
                // GetComponent<CircleCollider2D>().enabled = true;

                Invoke("Punch", 0.5f);


            }   
            else
            {
                isNearToPlayer = false;
                myAnimator.SetBool("isPunching", false);
                GetComponent<CircleCollider2D>().enabled = false;
            }
           
           

            if (!isNearToPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                myAnimator.SetBool("isWalking", true);
            }   else
            {
                myAnimator.SetBool("isWalking", false);
            }


           
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

    public void Punch()
    {
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        circle.enabled = true;
        Debug.Log("circle is: " + circle.enabled);
    }
}


