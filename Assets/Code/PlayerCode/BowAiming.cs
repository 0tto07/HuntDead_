using UnityEngine;

public class BowAiming : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        AimTowardsMouse();
    }

    void AimTowardsMouse()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z));
        Debug.Log(mouseWorldPosition);
        mouseWorldPosition.z = transform.position.z; // Ensure the z-position matches the bow to keep the aiming in 2D space.
        Debug.Log(transform.position);

        Vector2 directionToMouse = -1*(mouseWorldPosition - transform.position);
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
