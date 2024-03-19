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
    private GameObject[] bowObjects; // Array to hold objects with the "Bow" tag

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("BOX");
        // Find all objects with the "Bow" tag at the start and store them in the bowObjects array
        bowObjects = GameObject.FindGameObjectsWithTag("Bow");
        Debug.Log(layerIndex);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if (Keyboard.current.qKey.wasPressedThisFrame && grabbedObject == null)
            {
                Debug.Log("Grabbed");
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.GetComponent<BoxCollider2D>().enabled = false;
                grabbedObject.GetComponentInChildren<Animator>().SetBool("PickUp", true);
                grabbedObject.transform.SetParent(grabPoint);
                // Deactivate all objects with the "Bow" tag
                SetBowObjectsActive(false);
            }
        }
        else if (Keyboard.current.qKey.wasPressedThisFrame && grabbedObject != null)
        {
            Debug.Log("Released");
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.GetComponent<BoxCollider2D>().enabled = true;
            grabbedObject.GetComponentInChildren<Animator>().SetBool("PickUp", false);
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
            // Reactivate all objects with the "Bow" tag
            SetBowObjectsActive(true);
        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }

    // Method to activate/deactivate all objects with the "Bow" tag
    private void SetBowObjectsActive(bool active)
    {
        foreach (GameObject bowObject in bowObjects)
        {
            bowObject.SetActive(active);
        }
    }
}
