using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public GameObject key; // Reference to the key GameObject
    public float interactDistance = 2f; // Distance at which the player can interact with the door

    private bool hasKey = false;
    private bool isPlayerClose = false;
    private bool isSolid = true;
    private bool canCollide = true; // Flag to determine if the door can collide with other objects
    private Collider2D doorCollider;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        if (doorCollider == null)
        {
            Debug.LogError("Door script requires a Collider2D component.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Check if the player is close enough to the door
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            isPlayerClose = distanceToPlayer <= interactDistance;

            // Check if the player has the key
            hasKey = key != null && key.activeSelf;
        }

        // If player has the key and is close to the door, make it non-solid (open)
        if (hasKey && isPlayerClose)
        {
            SetSolid(false);
        }
        else
        {
            SetSolid(true);
        }
    }

    void SetSolid(bool solid)
    {
        isSolid = solid;
        doorCollider.isTrigger = !solid; // If solid, use collider for collision. Otherwise, use it as a trigger.
        canCollide = solid; // Update the flag to allow or disallow collision with other objects
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canCollide)
        {
            // If the door should not collide with anything, return without processing collision
            return;
        }

        // Handle collision with other objects normally
        // Add your collision handling logic here
    }
}