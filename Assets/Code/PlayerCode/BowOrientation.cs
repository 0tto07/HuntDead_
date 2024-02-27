using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowOrientation : MonoBehaviour
{
    private Vector2 direction;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GetComponentInParent<PlayerMovement>().transform;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - new Vector2(playerTransform.position.x, playerTransform.position.y);

        FaceMouse();
    }

    void FaceMouse()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;

        if ((direction.x < 0 && playerTransform.localScale.x > 0) || (direction.x > 0 && playerTransform.localScale.x < 0))
        {
            Vector3 localScale = transform.localScale;
            localScale.y *= -1;
            transform.localScale = localScale;
        }
    }
}
