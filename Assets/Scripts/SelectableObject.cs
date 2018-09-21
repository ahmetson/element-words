using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SelectableObject : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Battle battle = GameObject.FindGameObjectWithTag("Scripts").GetComponent<Battle>();

        Debug.Log("Clicked on the Item");

        // Right Button Click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            string message = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
            string key = message.Substring(0, message.LastIndexOf('+')-1);
            Debug.Log("Select the " + key);

            battle.SelectItem(gameObject, message);

        }

        
    }
}
