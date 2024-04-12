using UnityEngine;

public class LockpickGame : MonoBehaviour
{
    public GameObject lockpick; // Assign in the editor
    public float sweetSpotStartAngle = 45f;
    public float sweetSpotEndAngle = 60f;
    private float currentAngle = 0f;
    private bool isBroken = false;
    private bool wasInSweetSpot = false; // New variable to track sweet spot interaction

    void Update()
    {
        if (isBroken) return; // Stop further interaction if broken

        float rotationStep = 20f * Time.deltaTime; // Adjust rotation speed as needed

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLockpick(-rotationStep); // Rotate left
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateLockpick(rotationStep); // Rotate right
        }
    }

    void RotateLockpick(float rotationStep)
    {
        currentAngle += rotationStep;

        // Wrap currentAngle to keep it within 0-360 degrees
        if (currentAngle < 0f) currentAngle += 360f;
        if (currentAngle >= 360f) currentAngle -= 360f;

        lockpick.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        bool isInSweetSpot = currentAngle >= sweetSpotStartAngle && currentAngle <= sweetSpotEndAngle;

        // Only break the lockpick if it was in the sweet spot and then moved too far out
        if (wasInSweetSpot && !isInSweetSpot)
        {
            // Check if moved too far out from the sweet spot
            if (Mathf.Abs(currentAngle - sweetSpotStartAngle) > 10f && Mathf.Abs(currentAngle - sweetSpotEndAngle) > 10f)
            {
                BreakLockpick();
            }
        }

        wasInSweetSpot = isInSweetSpot; // Update wasInSweetSpot for the next frame
    }

    void BreakLockpick()
    {
        if (!isBroken) // Additional check to ensure we don't trigger breakage multiple times
        {
            isBroken = true; // Mark as broken to stop interaction
            // Optionally play a breaking animation or sound effect here
            Destroy(lockpick); // Remove the lockpick from the game
            // Optionally, trigger a UI message or penalty for the player
            Debug.Log("Lockpick Broken!"); // Debug message
        }
    }
}
