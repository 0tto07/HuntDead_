using UnityEngine;

public class WallJump : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private float wallJumpingPower = 7.5f;
    [SerializeField] private float wallJumpUpwardsPower = 15f;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        bool isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, wallLayer);
        if (isTouchingWall && Input.GetButtonDown("Jump"))
        {
            PerformWallJump();
        }
    }

    private void PerformWallJump()
    {
        if (playerMovement != null)
        {
            playerMovement.SetCanMoveHorizontally(false);

            float directionMultiplier = playerMovement.transform.localScale.x > 0 ? -1 : 1;
            float horizontalForce = directionMultiplier * wallJumpingPower;
            rb.velocity = new Vector2(horizontalForce, wallJumpUpwardsPower);

            // Manually set the flip direction based on the wall jump direction
            playerMovement.SetOverrideFlipDirection(true, directionMultiplier);

            Invoke("EnableHorizontalMovement", 0.5f);
        }
    }

    private void EnableHorizontalMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.SetCanMoveHorizontally(true);
            // Reset the flip direction to automatic based on velocity
            playerMovement.SetOverrideFlipDirection(false);
        }
    }
}
