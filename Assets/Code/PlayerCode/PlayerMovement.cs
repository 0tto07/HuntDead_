using UnityEngine;
using System.Collections; // Required for IEnumerator

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float jumpingPower = 16f;
    private float horizontal;
    private float horizontalMultiplier = 1f;
    private bool isFacingRight = true;
    private bool canMoveHorizontally = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        // Only read input if horizontal is not being simulated
        if (canMoveHorizontally)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

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

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (!canMoveHorizontally)
        {
            FlipBasedOnVelocity();
        }
        else
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (canMoveHorizontally)
        {
            rb.velocity = new Vector2(horizontal * speed * horizontalMultiplier, rb.velocity.y);
        }
    }

    public void SetCanMoveHorizontally(bool canMove)
    {
        canMoveHorizontally = canMove;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        bool shouldFlip = isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f;
        if (shouldFlip)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void FlipBasedOnVelocity()
    {
        if ((rb.velocity.x < 0f && isFacingRight) || (rb.velocity.x > 0f && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void SimulateHorizontalInput(float simulatedInput)
    {
        horizontal = simulatedInput;
        StartCoroutine(ResetSimulatedInputAfterDelay(0.1f)); // Reset after 0.1 seconds
    }

    private IEnumerator ResetSimulatedInputAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        horizontal = 0;
    }

    public bool IsFacingRight()
    {
        return isFacingRight;
    }
}
