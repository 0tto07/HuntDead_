using UnityEngine;

public class HealthVisibility3 : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject objectToControl;

    void Update()
    {
        if (playerData.GetCurrentHealth() > 2)
        {
            objectToControl.SetActive(true);
        }
        else
        {
            objectToControl.SetActive(false);
        }
    }
}