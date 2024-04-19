using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject key; // Reference to the key object
    public GameObject doorCollider; // Collider for the door
    public float openSpeed = 5f; // Speed at which the door opens
    public Vector3 openPosition; // Position to which the door opens

    private bool isOpen = false; // Flag to determine if the door is open
    private bool hasKey = false; // Flag to determine if the player has the key

    void Start()
    {
        // Ensure the door starts closed
        CloseDoor();
    }

    public void OpenDoor()
    {
        // Check if the door is closed and the player has the key
        if (!isOpen && hasKey)
        {
            // Open the door towards the open position
            doorCollider.transform.position = Vector3.MoveTowards(doorCollider.transform.position, openPosition, openSpeed * Time.deltaTime);

            // Check if the door has reached the open position
            if (doorCollider.transform.position == openPosition)
            {
                isOpen = true;
                // Optionally, disable the collider to allow the player to pass through
                doorCollider.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has collided with the key
        if (other.gameObject == key)
        {
            hasKey = true;
            // Optionally, you can disable or destroy the key after it's collected
            // Destroy(key);
            key.SetActive(false);
        }
    }

    void CloseDoor()
    {
        // Close the door by resetting its position
        doorCollider.transform.position = transform.position;
        isOpen = false;
        // Optionally, re-enable the collider
        doorCollider.SetActive(true);
    }
}