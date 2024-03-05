using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Animator animator;
    public Transform objectPickupPoint;
    public float moveSpeed = 5f;

    private GameObject carriedObject;
    private Rigidbody2D carriedRigidbody;
    private BoxCollider2D carriedCollider;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Raycast to detect object click
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Box"))
                {
                    // Pick up object
                    PickUpObject(hit.collider.gameObject);
                }
            }
        }

        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void PickUpObject(GameObject obj)
    {
        carriedObject = obj;
        carriedRigidbody = carriedObject.GetComponent<Rigidbody2D>();
        carriedRigidbody.isKinematic = true;
        carriedCollider = carriedObject.GetComponent<BoxCollider2D>();
        carriedCollider.enabled = false;
        carriedObject.transform.SetParent(objectPickupPoint);
        carriedObject.transform.localPosition = Vector3.zero;
        animator.SetBool("PickUp", true);
    }

    void DropObject()
    {
        if (carriedObject != null)
        {
            carriedObject.transform.SetParent(null);
            carriedRigidbody.isKinematic = false;
            carriedCollider.enabled = true;
            carriedObject = null;
            animator.SetBool("PickUp", false);
        }
    }
}

