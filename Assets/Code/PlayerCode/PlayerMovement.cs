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
    public float MovementSpeedPerSecond;

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

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Debug.Log("Sprinting");
            MovementSpeedPerSecond *= SprintMultiplier;
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