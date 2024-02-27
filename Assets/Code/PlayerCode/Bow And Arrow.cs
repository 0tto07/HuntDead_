using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BowAndArrow : MonoBehaviour
{
    public Vector2 direction;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 bowPos = transform.position;

        direction = MousePos - bowPos;



        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)playerTransform.position;

        FaceMouse();
        PositionAroundPlayer();
    }

    void FaceMouse()
    {
        transform.right = direction;
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

