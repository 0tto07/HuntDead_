using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObjects : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;
    [SerializeField]
    private Transform rayPoint;
    [SerializeField]
    private float rayDistance;
    [SerializeField]
    private float throwForce = 10f; // Adjust this value as needed

    private GameObject grabbedObject;
    private int layerIndex;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Box ");
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame && grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.SetParent(transform);
            }
            else if (Mouse.current.rightButton.wasPressedThisFrame && grabbedObject != null)
            {
                Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
                rb.isKinematic = false;
                grabbedObject.transform.SetParent(null);
                Vector2 throwDirection = (hitInfo.point - (Vector2)transform.position).normalized;
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
                grabbedObject = null;
            }

            Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
        }
    }
}









