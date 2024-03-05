using UnityEngine;

public class HealthVisibility : MonoBehaviour
{
    public GameObject healthObject; // Reference to the object you want to control visibility for

    private void Update()
    {
        // Check if health is greater than 0
        if (currentHealth > 0)
        {
            healthObject.SetActive(true); // Make the object visible
        }
        else
        {
            healthObject.SetActive(false); // Make the object invisible
        }
    }
}