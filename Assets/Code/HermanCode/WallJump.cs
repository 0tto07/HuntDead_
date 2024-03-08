using UnityEngine;

public class WallJump : MonoBehaviour
{
    public LayerMask wallLayer;
    public Transform wallCheck;
    private bool isTouchingWall;
    private Rigidbody2D rb;

    public float wallJumpingPower = 10f; // Adjust as needed for desired jump strength
    private float jumpAngle = 145f; // Angle in degrees for backward upward jump

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);

        if (Input.GetButtonDown("Jump") && isTouchingWall)
        {
            PerformWallJump();
        }
    }

    private void PerformWallJump()
    {
        // Calculate jump direction based on angle
        Vector2 jumpDirection = new Vector2(Mathf.Cos(jumpAngle * Mathf.Deg2Rad), Mathf.Sin(jumpAngle * Mathf.Deg2Rad));
        rb.velocity = jumpDirection * wallJumpingPower;
    }
}
