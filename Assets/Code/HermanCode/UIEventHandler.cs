using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;// Required when using Event data.

public class UIEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<GameObject> ObjectsToActivate = new List<GameObject>();
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hej");
        //Aktivera tänder
        foreach (GameObject obj in ObjectsToActivate)
        {
            obj.SetActive(true);
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
