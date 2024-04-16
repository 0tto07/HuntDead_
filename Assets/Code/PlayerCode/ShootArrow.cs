using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public float LaunchForce;
    public GameObject Arrow;

    [SerializeField] Transform theRotationOfTheBow;
    [SerializeField] Transform thePlayerPosition;

    private float cooldown = 2f; // 2 seconds cooldown
    private float nextShootTime = 0f; // Timestamp of when you can shoot next
    public float BowOffset = 0.0f;

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

        // Restrict the aiming to 180 degrees on the right side
        angle = Mathf.Clamp(angle, -90f, 90f); // Clamps the angle to be within -90 and 90 degrees

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = thePlayerPosition.position + transform.right * BowOffset; // Correctly place the bow with the offset
    }

    void Shoot()
    {
        GameObject ArrowIns = Instantiate(Arrow, transform.position, theRotationOfTheBow.rotation);
        ArrowIns.GetComponent<Rigidbody2D>().AddForce(transform.right * LaunchForce); // Ensures the arrow is always shot to the right
    }
}
