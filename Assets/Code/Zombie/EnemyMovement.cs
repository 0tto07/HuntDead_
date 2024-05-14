using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    public float speed;
    private float followRadius = 10.0f;

    Animator myAnimator;
    AudioManager myAudioManager;

    private bool facingRight = true;

    [SerializeField] bool isNearToPlayer;

    Rigidbody2D enemyRb;

    CircleCollider2D circle;

    private bool hasFoundPlayer;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
       
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if(!hasFoundPlayer)
        {
            if (distanceToTarget <= followRadius)
            {

                if (distanceToTarget <= 1)
                {
                    hasFoundPlayer = true;
                    Debug.Log("I found the player");
                    isNearToPlayer = true;
                    myAnimator.SetBool("isPunching", true);


                    Invoke(nameof(Punch), 0.5f);


                }
                else
                {
                    isNearToPlayer = false;
                    myAnimator.SetBool("isPunching", false);
                    GetComponent<CircleCollider2D>().enabled = false;
                    Rigidbody2D enemyRb = GetComponent<Rigidbody2D>();
                    if (!isNearToPlayer)
                    {
                        enemyRb.AddForce((target.position - transform.position).normalized * speed * Time.deltaTime, ForceMode2D.Impulse); ;
                        myAnimator.SetBool("isWalking", true);
                    }
                    else
                    {
                        myAnimator.SetBool("isWalking", false);
                    }



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

        
    }

    void Flip()
    {
      
        facingRight = !facingRight;

      
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
    //        if (playerData != null)
    //        {
    //            playerData.TakeDamage(1);
    //        }
    //    }
    //}
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
          
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
            if (playerData != null)
            {
                playerData.TakeDamage(1);
                Debug.Log("GET HIT C");
            }

        }
    }

    public void Punch()
    {
        enemyRb.constraints = RigidbodyConstraints2D.FreezePosition;

        enemyRb.freezeRotation = true;
        circle = GetComponent<CircleCollider2D>();
        circle.enabled = true;
        circle.isTrigger = true;
        Physics2D.SyncTransforms();

        Invoke(nameof(UnFreeze), 0.1f);
        
        Debug.Log("circle is: " + circle.enabled);
        Invoke(nameof(ResetPunchColider), 0.2f);
    }

    void ResetPunchColider()
    {
        circle.enabled = false;
        hasFoundPlayer = false;
    }

    public void UnFreeze()
    {
        enemyRb.constraints = RigidbodyConstraints2D.None;

        enemyRb.freezeRotation = true;

    }
}


