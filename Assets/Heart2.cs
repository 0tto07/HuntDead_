using UnityEngine;
using UnityEngine.UI;

public class HealthVisibility2 : MonoBehaviour
{
    public PlayerData playerData;
    public Image heartImage; // Assuming you're using an Image component for the heart UI item

    void Update()
    {
        if (playerData.GetCurrentHealth() > 1)
        {
            SetAlpha(heartImage, 1f); // Make it fully visible
        }
        else
        {
            SetAlpha(heartImage, 0f); // Make it fully transparent
        }
    }

    // Function to set the alpha value of an Image component
    private void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}