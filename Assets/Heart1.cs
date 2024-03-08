using UnityEngine;

public class HealthVisibility : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject objectToControl;

    void Update()
    {
        if (playerData.GetCurrentHealth() > 0)
        {
            objectToControl.SetActive(true);
        }
        else
        {
            objectToControl.SetActive(false);
        }
    }
}