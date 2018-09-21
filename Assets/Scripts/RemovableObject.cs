using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class RemovableObject : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Profile profile = GameObject.FindGameObjectWithTag("Scripts").GetComponent<Profile>();

        // Right Button Click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Delete Object");
            string message = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
            string key = message.Substring(0, message.LastIndexOf('+')-1);
            Debug.Log("Remove the " + key);

            profile.RemoveItem(key);

            DestroyImmediate(gameObject);
        }

        
    }
}
