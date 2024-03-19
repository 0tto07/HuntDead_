using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Configurable parameters
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    // Private variables
    Vector2 moveInput;
    Vector2 runVelocity;
    bool jumpPressed = false;

    float horizontalMultiplier = 1f;
    bool canMoveHorizontally = true; 

    // Cached references
    Animator myAnimator;
    Rigidbody2D myRigidbody;

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() // Updates every frame
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate() // Updates every 0.02 second
    {
        Run();
        Jump();
        Fall();
        FlipSprite();
    }

    void Run()
    {
        runVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = runVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void Jump()
    {
        if (jumpPressed && IsGrounded())
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            jumpPressed = false;
        }
    }

    // TODO integrate with the new jumping and movement system
    public void SetCanMoveHorizontally(bool canMove)
    {
        canMoveHorizontally = canMove;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void Fall()
    {
        bool playerHasNegativeVerticalSpeed = myRigidbody.velocity.y < Mathf.Epsilon;

        if (playerHasNegativeVerticalSpeed && !IsGrounded())
        {
            myAnimator.SetBool("isFalling", true);
        }
        else
        {
            myAnimator.SetBool("isFalling", false);
        }
    }
}
