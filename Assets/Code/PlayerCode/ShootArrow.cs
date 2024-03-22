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

    // Update is called once per frame
    void Update()
    {
        transform.position = thePlayerPosition.position;
        if (Time.time >= nextShootTime && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            nextShootTime = Time.time + cooldown; // Set the next shoot time
        }
    }

    void Shoot()
    {
        GameObject ArrowIns = Instantiate(Arrow, transform.position, theRotationOfTheBow.rotation);
        ArrowIns.GetComponent<Rigidbody2D>().AddForce(-transform.right * LaunchForce);
    }
}
