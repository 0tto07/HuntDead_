using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class UIEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<GameObject> ObjectsToActivate = new List<GameObject>();
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hej");
        //Aktivera tänder
        foreach (GameObject obj in ObjectsToActivate)
        {
            obj.SetActive(true);
          
            
        }
        if (hoverSound != null && audioSource != null)
        {
            audioSource.clip = hoverSound;
            audioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hejda");

        foreach (GameObject obj in ObjectsToActivate)
        {
            obj.SetActive(false);
        }
    }

}
