using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // Jump parameters
    public float JumpSpeedFactor = 3.0f;
    public float JumpMaxHeight = 4.0f;
    private bool isJumping = false;

    // Movement parameters
    public float BaseMovementSpeed = 10.0f;
    public float SprintMultiplier = 1.5f;
    private float MovementSpeedPerSecond;

    // Slide parameters
    public float SlideDuration = 0.5f;
    private bool isSliding = false;

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Start()
    {
        MovementSpeedPerSecond = BaseMovementSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckForSprint();
        HandleMovement();
        if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            StartJump();
        }
        if (isGrounded && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)))
        {
            StartSlide();
        }
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }

    private void CheckForSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            MovementSpeedPerSecond = BaseMovementSpeed * SprintMultiplier;
        }
        else
        {
            MovementSpeedPerSecond = BaseMovementSpeed;
        }
    }

    private void HandleMovement()
    {
        if (!isSliding && !isJumping)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            // Move the player based on the input
            transform.Translate(Vector2.right * horizontalInput * MovementSpeedPerSecond * Time.deltaTime, Space.World);

            // Flip the player's sprite if moving left
            if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Flip to face left
            }
            // Flip the player's sprite if moving right
            else if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Face right
            }
        }
    }

    private void StartJump()
    {
        if (!isJumping && isGrounded)
        {
            isJumping = true;
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        float time = 0.0f;
        Vector2 initialPosition = transform.position;

        while (time < 1.0f)
        {
            float jumpHeight = Mathf.Lerp(0, JumpMaxHeight, time);

            // Only apply vertical movement during the jump
            transform.position = initialPosition + Vector2.up * jumpHeight;

            time += Time.deltaTime * JumpSpeedFactor;
            yield return null;
        }

        isJumping = false;
    }

    private void StartSlide()
    {
        if (!isSliding && isGrounded)
        {
            StartCoroutine(SlideCoroutine());
        }
    }

    private IEnumerator SlideCoroutine()
    {
        isSliding = true;

        // Get the initial direction of the slide
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 slideDirection = new Vector2(horizontalInput, 0f).normalized;

        // Rotate player only if there's horizontal input
        if (horizontalInput != 0)
        {
            float slideAngle = 60f * Mathf.Sign(horizontalInput);
            transform.rotation = Quaternion.Euler(0f, 0f, slideAngle);
        }

        // Increase the movement speed by 1.5x
        float initialSpeed = MovementSpeedPerSecond * 1.5f; // Set initial speed 1.5x the current speed

        // Glide in the direction
        float slideTimer = 1.2f;
        while (slideTimer < SlideDuration)
        {
            // Move the player in the initial slide direction
            transform.Translate(slideDirection * initialSpeed * Time.deltaTime, Space.World);
            slideTimer += Time.deltaTime;

            yield return null;
        }

        // Reset rotation and sliding
        transform.rotation = Quaternion.identity;
        isSliding = false;
    }
}