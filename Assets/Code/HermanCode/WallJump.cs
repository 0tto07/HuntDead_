using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private float wallJumpingPower = 10f;
    [SerializeField] private float wallJumpUpwardsPower = 5f;
    private Rigidbody2D rb;
    private bool isFacingRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isFacingRight = transform.localScale.x > 0;

        bool isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, wallLayer);
        if (isTouchingWall && Input.GetButtonDown("Jump"))
        {
            PerformWallJump();
        }
    }

    private void PerformWallJump()
    {
        // Disable horizontal movement when performing a wall jump
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetCanMoveHorizontally(false);
        }

        float horizontalForce = isFacingRight ? -wallJumpingPower : wallJumpingPower;
        float verticalForce = wallJumpUpwardsPower;

        rb.velocity = new Vector2(horizontalForce, verticalForce);

        // Optionally, re-enable horizontal movement after a delay or under certain conditions
        // For example, you could re-enable it after a short time or when the player is grounded again.
        // This code is just an example; adjust it according to your game's mechanics.
        Invoke("EnableHorizontalMovement", 0.5f); // Re-enable after 0.5 seconds
    }

    private void EnableHorizontalMovement()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetCanMoveHorizontally(true);
        }
    }
}
