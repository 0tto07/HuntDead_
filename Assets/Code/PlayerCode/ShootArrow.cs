using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public float LaunchForce;
    public GameObject Arrow;

    [SerializeField] Transform thePlayerPosition;
    public float BowOffset = 0.0f;

    private float cooldown = 2f; // 2 seconds cooldown
    private float nextShootTime = 0f; // Timestamp of when you can shoot next
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        AimTowardsMouse();

        if (Time.time >= nextShootTime && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            nextShootTime = Time.time + cooldown; // Set the next shoot time
        }
    }

    void AimTowardsMouse()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z - transform.position.z));
        mouseWorldPosition.z = transform.position.z; // Ensure the z-position is fixed to keep the aiming in 2D plane.

        Vector2 directionToMouse = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Adjust the transform rotation directly to the angle calculated
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Adjust the position of the bow to maintain an offset relative to the player
        Vector3 offset = new Vector3(BowOffset, 0, 0);
        offset = Quaternion.Euler(0, 0, angle) * offset;  // Rotate the offset to align with the bow's rotation
        transform.position = thePlayerPosition.position + offset;
    }

    void Shoot()
    {
        // Instantiate arrow at the position and rotation of the bow
        GameObject ArrowIns = Instantiate(Arrow, transform.position, transform.rotation);
        // Add force in the opposite direction the bow is currently facing
        ArrowIns.GetComponent<Rigidbody2D>().AddForce(-transform.right * LaunchForce);
    }
}
