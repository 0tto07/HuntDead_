using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Jump parameters
    public float SprintMultiplier = 1.5f;
    public float JumpSpeedFactor = 3.0f;
    public float JumpMaxHeight = 4.0f;
    private float JumpHeightDelta = 0.0f;
    private bool isJumping = false;

    // Movement parameters
    public float MovementSpeedPerSecond = 10.0f;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float speed = MovementSpeedPerSecond * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift))
        {
            speed *= SprintMultiplier;
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Move Left
            transform.Translate(-MovementSpeedPerSecond * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Move Right
            transform.Translate(MovementSpeedPerSecond * Time.deltaTime, 0, 0);

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
            transform.Translate(0, Time.deltaTime * JumpSpeedFactor * MovementSpeedPerSecond, 0);

            // Check if the jump has reached its max height
            if (JumpHeightDelta >= JumpMaxHeight)
            {
                JumpHeightDelta = 0.0f;
                isJumping = false;
            }
        }
    }
}