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
            isGrounded = true;
            gameObject.layer = groundedArrowLayer;
            rb.velocity = Vector2.zero; // Stop the arrow
            rb.isKinematic = true; // Make the Rigidbody kinematic
            rb.angularVelocity = 0; // Stop any rotational movement
            // Optionally, disable any other arrow-specific behaviors now that it's grounded
        }
    }
}
