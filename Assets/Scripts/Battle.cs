using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class ElementItem
{
    public string Key;
    public int Value;

    public ElementItem(string k, int v)
    {
        Key = k;
        Value = v;
    }
}

public class Battle : MonoBehaviour {

    // Panels
    public GameObject ItemListPanel;
    public GameObject BattlePanel;

    // Title
    public TextMeshProUGUI TimerText;

    // Item List Panel -> Selected Item
    public TextMeshProUGUI SelectedItemText;

    // Item List Panel -> Container
    public Transform ItemList;

    // Battle Panel -> Container
    public Transform BattleScene;

    public Animator StartBattleText;
    public Animator ElementReactionText;
    public Button CancelButton;
    public TextMeshProUGUI ErrorText;

    public GameObject GroundPrefab;
    public GameObject WindPrefab;
    public GameObject FirePrefab;
    public GameObject WaterPrefab;

    private Data data;
    private GameObject opponent;
    private GameObject player;

    private Coroutine timer;

	// Use this for initialization
	void Start () {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<Data>();

        SelectedItemText.SetText("");
        ErrorText.SetText("");

        CancelButton.interactable = false;
        opponent = null;
        player = null;

        // Clear and show everything from the Item's List
        ShowSelectableItemList();

        // Start the Timer, Enable the Selection
        timer = StartCoroutine("SelectionTime");
	}

    public void FinishBattle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    IEnumerator SelectionTime()
    {
        for(int i=20; i>=1; i--)
        {
            TimerText.SetText(i.ToString() + " Seconds to Select Item!");
            yield return new WaitForSeconds(1);
        }

        if (SelectedItemText.text.Length < 1)
        {
            FinishBattle();
        }
        else
        {
            timer = null;
        }
    }

    public void ShowSelectableItemList()
    {
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

            element.AddComponent<SelectableObject>();

            element.transform.GetComponentInChildren<TextMeshProUGUI>().SetText(message);
        }

        ItemListPanel.SetActive(true);
        BattlePanel.SetActive(false);
    }

    public void SelectItem(GameObject item, string message)
    {
        SelectedItemText.SetText(message);
        ErrorText.SetText("");
    }
}
