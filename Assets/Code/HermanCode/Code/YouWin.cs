using UnityEngine;
using TMPro;

public class YouWin : MonoBehaviour
{
    public TextMeshProUGUI youWinText; 

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with the win object!"); 

            youWinText.gameObject.SetActive(true); 
            youWinText.text = "You Win!"; 

            Debug.Log("You Win!"); 
        }
    }
}




