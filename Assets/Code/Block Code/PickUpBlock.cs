using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBlocks : MonoBehaviour
{
    private bool isCarrying = false;
    private GameObject carriedBlock;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isCarrying)
            {
                DropBlock();
            }
            else
            {
                PickUpBlock();
            }
        }
    }

    void PickUpBlock()
    {
        // Raycast forward to check for blocks
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1f);

        if (hit.collider != null && hit.collider.CompareTag("Block"))
        {
            // Pick up the block
            isCarrying = true;
            carriedBlock = hit.collider.gameObject;
            carriedBlock.GetComponent<Rigidbody2D>().isKinematic = true;
            carriedBlock.transform.parent = transform;
        }
    }

    void DropBlock()
    {
        // Drop the carried block
        isCarrying = false;

        if (carriedBlock != null)
        {
            carriedBlock.GetComponent<Rigidbody2D>().isKinematic = false;
            carriedBlock.transform.parent = null;
            carriedBlock = null;
        }
    }
}
