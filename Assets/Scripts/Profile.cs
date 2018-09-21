using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Profile : MonoBehaviour {

    public GameObject ItemListPanel;
    public GameObject CreatePanel;
    public Transform ItemList;

    public GameObject GroundPrefab;
    public GameObject WindPrefab;
    public GameObject FirePrefab;
    public GameObject WaterPrefab;

    private Data data;

    public TMP_InputField NewItemName;
    public TMP_Dropdown NewItemElement;

	// Use this for initialization
	void Start () {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<Data>();

        // Clear and show everything from the Item's List
        ShowItemListPanel();
	}

    public void ShowBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
    }

    public void ShowItemListPanel()
    {
        var children = new List<GameObject>();
        foreach (Transform child in ItemList) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        foreach (var item in data.Items)
        {
            var message = item.Key + " +" + item.Value;

            GameObject element = null;
            if (item.Key.StartsWith("Wind"))
            {
                element = Instantiate(WindPrefab, ItemList);
            }
            else if (item.Key.StartsWith("Water"))
            {
                element = Instantiate(WaterPrefab, ItemList);
            }
            else if (item.Key.StartsWith("Fire"))
            {
                element = Instantiate(FirePrefab, ItemList);
            }
            else
            {
                element = Instantiate(GroundPrefab, ItemList);
            }

            element.AddComponent<RemovableObject>();

            element.transform.GetComponentInChildren<TextMeshProUGUI>().SetText(message);
        }

        ItemListPanel.SetActive(true);
        CreatePanel.SetActive(false);
    }

    public void ShowCreatePanel()
    {
        ItemListPanel.SetActive(false);
        CreatePanel.SetActive(true);
    }

    public void RemoveItem(string key)
    {
        data.RemoveItem(key);

        ShowItemListPanel();
    }

}
