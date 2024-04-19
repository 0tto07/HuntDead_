using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followSpeed = 5f; // Base speed of following
    public float maxSpeed = 10f; // Maximum speed when far away from the player
    public float minSpeed = 2f; // Minimum speed when close to the player
    public float distanceThreshold = 10f; // Distance threshold for adjusting speed
    public Door door; // Reference to the door object

    private Rigidbody2D rb;
    private bool startFollowing = false; // Flag to determine if following should start

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            // If player is not assigned, try to find it by tag
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        if (!startFollowing || player == null)
            return;

        // Calculate the distance between object and player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Calculate the speed based on distance
        float speed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.InverseLerp(0, distanceThreshold, distanceToPlayer));

        // Direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Apply force towards the player
        rb.velocity = direction * speed;

        // Check if the door reference is not null and following has started
        if (door != null && startFollowing)
        {
            // Open the door when following starts
            door.OpenDoor();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Start following when the player collides with the object
            startFollowing = true;
        }
    }
}