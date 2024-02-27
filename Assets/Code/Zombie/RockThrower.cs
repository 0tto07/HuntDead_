using UnityEngine;

public class RockThrower : MonoBehaviour
{
    // Movement properties
    public float speed;
    private Transform target;
    private bool isFacingRight = true;
    private float horizontal;
    private float followRadius = 510.0f;

    // Rock throwing properties
    public GameObject RockPrefab;
    public float LaunchForce;
    public float throwRadius = 15.0f;
    private float lastShotTime;
    private const float Cooldown = 1.5f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        MoveAndThrowRock();
    }

    private void MoveAndThrowRock()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Move towards the player if within follow radius
        if (distanceToTarget <= followRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            horizontal = target.position.x - transform.position.x;
            Flip();
        }

        // Throw rock if within throw radius and cooldown has elapsed
        if (distanceToTarget <= throwRadius && Time.time - lastShotTime >= Cooldown)
        {
            ThrowRock();
        }
    }

    private void ThrowRock()
    {
        GameObject rockInstance = Instantiate(RockPrefab, transform.position, transform.rotation);
        Rigidbody2D rockRb = rockInstance.GetComponent<Rigidbody2D>();
        if (rockRb != null)
        {
            rockRb.AddForce(transform.right * LaunchForce);
        }

        lastShotTime = Time.time;
    }

    private void Flip()
    {
        float movementDirection = Mathf.Sign(horizontal);
        if ((movementDirection > 0f && !isFacingRight) || (movementDirection < 0f && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
