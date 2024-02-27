using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private float horizontalMultiplier = 1f; // Added horizontal multiplier
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Check if left shift is held down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontalMultiplier = 1.5f; // If left shift is held down, double the horizontal movement
        }
        else
        {
            horizontalMultiplier = 1f; // Otherwise, use the default horizontal movement
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower); // Apply the jumping power
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed * horizontalMultiplier, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Called when the puzzle is solved
    public void EnableSpecialAbility()
    {
        // Add your custom behavior here
        Debug.Log("Special ability activated!");
        // For example, you might enable a special ability or perform any other action.
    }
}
