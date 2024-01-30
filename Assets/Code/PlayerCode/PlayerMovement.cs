using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Jump parameters
    public float JumpSpeedFactor = 3.0f;
    public float JumpMaxHeight = 4.0f;
    private float JumpHeightDelta = 0.0f;
    private bool isJumping = false;

    // Movement parameters
    public float BaseMovementSpeed = 10.0f;
    public float SprintMultiplier = 1.5f;
    private float MovementSpeedPerSecond;

    private void Start()
    {
        MovementSpeedPerSecond = BaseMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSprint();
        HandleMovement();
        HandleJump();
    }

    void CheckForSprint()
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

    void HandleMovement()
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

    void HandleJump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && !isJumping)
        {
            isJumping = true;
        }

        if (isJumping)
        {
            // Calculate the jump height
            JumpHeightDelta += Time.deltaTime * JumpSpeedFactor * MovementSpeedPerSecond;

            // Move the player up
            transform.Translate(Vector2.up * Time.deltaTime * JumpSpeedFactor * MovementSpeedPerSecond);

            // Check if the jump has reached its max height
            if (JumpHeightDelta >= JumpMaxHeight)
            {
                JumpHeightDelta = 0.0f;
                isJumping = false;
            }
        }
    }
}