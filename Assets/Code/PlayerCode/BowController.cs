using UnityEngine;

public class BowController : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        // Cache the main camera at start
        mainCamera = Camera.main;
    }

    private void Update()
    {
        AimTowardsMouse();
    }

    void AimTowardsMouse()
    {
        // Convert mouse position into world coordinates
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z));

        // Adjust mouse world position to have the same z as the bow for proper calculation
        mouseWorldPosition.z = transform.position.z;

        // Find the direction from the bow to the mouse
        Vector2 directionToMouse = mouseWorldPosition - transform.position;

        // Calculate the angle between the direction to the mouse and the up vector
        // Note: Depending on your bow sprite orientation, you might need to adjust the reference axis (here it's assumed the sprite faces right at 0 rotation)
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Apply the rotation
        // Quaternion.Euler creates a rotation from the specified Euler angles in degrees
        // Here, we're only rotating on the Z axis to keep the bow flat on the 2D plane
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
