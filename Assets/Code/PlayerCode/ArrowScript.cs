using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    private int groundLayer;
    private int groundedArrowLayer;
    private int zombieLayer;
     
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.NameToLayer("Ground");
        groundedArrowLayer = LayerMask.NameToLayer("GroundedArrow");
        zombieLayer = LayerMask.NameToLayer("Zombie");
       

    }

    void Update()
    {
        if (!isGrounded)
        {
            TrackMovement();
        }
    }

    void TrackMovement()
    {
        Vector2 dir = rb.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            GroundArrow();
        }
        else if (collision.gameObject.layer == zombieLayer)
        {
            DamageZombie(collision.gameObject);
        }
        
    }

    private void GroundArrow()
    {
        isGrounded = true;
        gameObject.layer = groundedArrowLayer;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        rb.angularVelocity = 0;
    }

    private void DamageZombie(GameObject zombie)
    {
        ZombieData zombieData = zombie.GetComponent<ZombieData>();
        if (zombieData != null)
        {
            zombieData.hitPoints -= 1;
            if (zombieData.hitPoints <= 0)
            {
                Destroy(zombie);
            }
            GroundArrow();
        }
    }
}
