using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followSpeed = 5f; // Base speed of following
    public float maxSpeed = 10f; // Maximum speed when far away from the player
    public float minSpeed = 2f; // Minimum speed when close to the player
    public float distanceThreshold = 10f; // Distance threshold for adjusting speed

    private Rigidbody2D rb;

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
        if (player == null)
            return;

        // Calculate the distance between object and player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Calculate the speed based on distance
        float speed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.InverseLerp(0, distanceThreshold, distanceToPlayer));

        // Direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Apply force towards the player
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start following when the player touches the object
            // (Optional: you can add effects or sounds here)
        }
    }
}