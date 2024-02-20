using UnityEngine;

public class BowAndArrow : MonoBehaviour
{
    private Vector2 direction;
    private SpriteRenderer mySprite; // Variable for the SpriteRenderer

    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    private float distanceFromPlayer = 1.0f; // Distance from the player

    void Start()
    {
        // Initialize the SpriteRenderer
        mySprite = GetComponent<SpriteRenderer>();
        if (mySprite == null)
        {
            Debug.LogError("SpriteRenderer not found on the GameObject.");
        }
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)playerTransform.position;

        FaceMouse();
        PositionAroundPlayer();
    }

    void FaceMouse()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    void PositionAroundPlayer()
    {
        // Calculate the normalized direction
        Vector2 normalizedDirection = direction.normalized;

        // Set the bow's position to be at the specified distance from the player
        transform.position = (Vector2)playerTransform.position + normalizedDirection * distanceFromPlayer;
    }
}
