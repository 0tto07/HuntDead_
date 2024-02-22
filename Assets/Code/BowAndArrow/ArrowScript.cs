using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    private int groundLayer;
    private int groundedArrowLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.NameToLayer("Ground");
        groundedArrowLayer = LayerMask.NameToLayer("GroundedArrow");

        // Ensure the arrow starts on a different layer than GroundedArrow
        // For example, it could be on the default layer or a custom "Arrow" layer
        // gameObject.layer = LayerMask.NameToLayer("Arrow"); // Uncomment if using a custom "Arrow" layer
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
        if (collision.gameObject.layer == groundLayer && !isGrounded)
        {
            isGrounded = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            // Change the layer to GroundedArrow upon grounding
            gameObject.layer = groundedArrowLayer;
        }
    }
}
