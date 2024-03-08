using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private float horizontalMultiplier = 1f; // Added horizontal multiplier
    private bool isFacingRight = true;
    private bool isTouchingWall;
    private bool isWallSliding;
    public float wallSlidingSpeed;
    public float wallJumpingPower = 20f;
    private float wallJumpTime = 0.2f;
    private float timeSinceWallJump;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontalMultiplier = 1.5f;
        }
        else
        {
            horizontalMultiplier = 1f;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (Input.GetButtonDown("Jump") && isTouchingWall && !IsGrounded())
        {
            // Initiate wall jump
            isWallSliding = false;
            Vector2 wallJumpDirection = isFacingRight ? new Vector2(-1, 1) : new Vector2(1, 1);
            rb.velocity = new Vector2(wallJumpDirection.x * wallJumpingPower, wallJumpDirection.y * wallJumpingPower);
            timeSinceWallJump = Time.time;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (isTouchingWall && !IsGrounded() && horizontal != 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        WallSlide();

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed * horizontalMultiplier, rb.velocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void WallSlide()
    {
        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    private void Flip()
    {
        bool shouldFlip = isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f;
        if (shouldFlip && Time.time - timeSinceWallJump > wallJumpTime)
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
        Debug.Log("Special ability activated!");
    }
}
