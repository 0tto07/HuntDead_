using UnityEngine;

public class HealthVisibility2 : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject objectToControl;

    void Update()
    {
        if (playerData.GetCurrentHealth() > 1)
        {
            objectToControl.SetActive(true);
        }
        else
        {
            objectToControl.SetActive(false);
        }
    }
}