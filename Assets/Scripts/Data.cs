using UnityEngine;
using System.Collections.Generic;
using Neo.Lux.Cryptography;


// To Store the Element Items
public class Data : MonoBehaviour {

    // Name, Value
    [SerializeField]
    public Dictionary<string, int> Items;

    public KeyPair KeyPair;

    public string UserAddress;

    public enum NETWORK_TYPE
    {
        ETHEREUM,
        NEO,
        CENTRALIZED
    }

    public NETWORK_TYPE NetworkType;

    // Use this for initialization
    void Awake() {
        Items = new Dictionary<string, int>();

        LoadData();
        DontDestroyOnLoad(gameObject);

        // Default User Address
        UserAddress = "Player01";
	}

    private void OnDestroy()
    {
        SaveData();
    }

    void LoadData()
    {
        var data = PlayerPrefs.GetString(GetItemsKey(), "");

        if (string.IsNullOrEmpty(data))
        {
            return;
        }

        var items = data.Split(';');

        foreach(var name in items)
        {
            var value = PlayerPrefs.GetInt(name, 0);

            Items.Add(name, value);
        }
    }

    void SaveData()
    {
        var items = string.Join(";", Items.Keys);

        PlayerPrefs.SetString(GetItemsKey(), items);
        

        foreach(var item in Items)
        {
            PlayerPrefs.SetInt(item.Key, item.Value);
        }
    }

    string GetItemsKey()
    {
        return NetworkType.ToString() + UserAddress + "Items";
    }
	
	// Update is called once per frame
	public void UpdateData (string key, int value) {
        Items.Remove(key);
        CreateItem(key, value);
	}

    public void RemoveItem(string key)
    {
        Items.Remove(key);
    }

    public void CreateItem(string key, int value)
    {
        Items.Add(key, value);  
    }
}
