using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private int collisionCount = 0;
    private bool hasCollided = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            gameObject.SetActive(false);
            hasCollided = true;
            collisionCount++;
            Debug.Log("Collision count: " + collisionCount);
        }
    }

    void Update()
    {
        Debug.Log("Update method called");
        if (Input.GetKeyDown(KeyCode.H))
        {
            ResetCollisionCount();
        }
    }

    void ResetCollisionCount()
    {
        Debug.Log("Collision count before reset: " + collisionCount);
        collisionCount = 0;
        Debug.Log("Collision count reset to 0");
    }

    public void ResetItem()
    {
        gameObject.SetActive(true);
        hasCollided = false;
        collisionCount = 0;
    }

    public int GetCollisionCount()
    {
        return collisionCount;
    }
}