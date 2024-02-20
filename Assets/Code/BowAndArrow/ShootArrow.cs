using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public float LaunchForce;
    public GameObject Arrow;
    private float lastShotTime;
    private const float Cooldown = 1.5f; // Cooldown time in seconds

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time - lastShotTime >= Cooldown)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject ArrowIns = Instantiate(Arrow, transform.position, transform.rotation);
        ArrowIns.GetComponent<Rigidbody2D>().AddForce(transform.right * LaunchForce);
        lastShotTime = Time.time; // Update the last shot time
    }
}
