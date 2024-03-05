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

    private GameObject grabbedObject;
    private int layerIndex;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Box");
        Debug.Log(layerIndex);
    }
    
     void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider !=null && hitInfo.collider.gameObject.layer == layerIndex) 
        {
            if (Keyboard.current.eKey.wasPressedThisFrame && grabbedObject == null) 
            {
                Debug.Log("pressed 1");
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.GetComponent<BoxCollider2D>().enabled = false;
                grabbedObject.GetComponentInChildren<Animator>().SetBool("PickUp", true);
                grabbedObject.transform.SetParent(transform);
            }



        }
        else if (Keyboard.current.eKey.wasPressedThisFrame && grabbedObject)
        {
            Debug.Log("pressed 2");
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.GetComponent<BoxCollider2D>().enabled = true;
            grabbedObject.GetComponentInChildren<Animator>().SetBool("PickUp", false);
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
        }
        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);

    }
}
