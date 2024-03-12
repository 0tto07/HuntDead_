using UnityEngine;

public class HealthVisibility69 : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject objectToControl;
    private bool wasVisible = true;

    void Update()
    {
        if (playerData.GetCurrentHealth() > 1)
        {
            // If the object was previously invisible, set it to active
            if (!wasVisible)
            {
                objectToControl.SetActive(true);
                wasVisible = true;
            }
        }
        else
        {
            // If the object was visible and the health is now not above 1, set it to inactive
            if (wasVisible)
            {
                objectToControl.SetActive(false);
                wasVisible = false;
            }
        }
    }
}