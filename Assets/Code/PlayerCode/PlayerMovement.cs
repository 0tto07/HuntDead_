using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float bufferTime = 0.05f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    Vector2 moveInput;
    Vector2 runVelocity;
    bool jumpPressed = false;

    float horizontalMultiplier = 1.001f;
    bool canMoveHorizontally = true;
    private bool overrideFlipDirection = false;
    private float flipDirection = 1f; // 1 for right, -1 for left
    public float FacingDirection => transform.localScale.x;  // 1 is right and -1 is left

    float airTime = 0.0f; 

    Animator myAnimator;
    Rigidbody2D myRigidbody;
    AudioManager myAudioManager;

    private bool walkSoundIsPlaying;

    private void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        myAudioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");

       Debug.Log("Player airtime is: " + airTime);

        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(JumpBuffer());
        }

      
        if(!IsGrounded())
        {
            airTime += Time.deltaTime;
        }   
        else
        {
            airTime = 0;
        }
    }

    void FixedUpdate()
    {
        Run();
        Jump();
        Fall();
        FlipSprite();
    }

    void Run()
    {
        if (!canMoveHorizontally) return;

        runVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = runVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

        if (playerHasHorizontalSpeed && !walkSoundIsPlaying && IsGrounded())
        {
            Debug.Log("hej");
            walkSoundIsPlaying = true;
            AudioManager.Instance.PlaySFX("PlayerWalk");
        }   


        if(!playerHasHorizontalSpeed || !IsGrounded())
        {
            walkSoundIsPlaying = false;
            AudioManager.Instance.StopSoundEffect();
        }
    }

    void Jump()
    {
        if (jumpPressed && IsGrounded())
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            jumpPressed = false;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void FlipSprite()
    {
        if (overrideFlipDirection)
        {
            transform.localScale = new Vector2(flipDirection, 1f);
        }
        else
        {
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            if (playerHasHorizontalSpeed)
            {
               transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
            }
        }
    }

    void Fall()
    {
        bool playerHasNegativeVerticalSpeed = myRigidbody.velocity.y < 0;
        myAnimator.SetBool("isFalling", playerHasNegativeVerticalSpeed && !IsGrounded());
    }

    public void SetCanMoveHorizontally(bool canMove)
    {
        canMoveHorizontally = canMove;
    }

    public void SimulateHorizontalInput(float direction)
    {
        moveInput.x = direction;
    }

    public void SetOverrideFlipDirection(bool overrideFlip, float direction = 1f)
    {
        overrideFlipDirection = overrideFlip;
        flipDirection = direction;
        FlipSprite(); // Apply the flip immediately if needed
    }

    public float GetAirTime()
    {
        return airTime;
    }

    private IEnumerator JumpBuffer()
    {
        jumpPressed = true;
        yield return new WaitForSeconds(bufferTime);
        jumpPressed = false;
    }
}
