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

    private Coroutine timer;

    private ElementItem opponentElement;
    private ElementItem playerElement;
    private int reward;

    // Use this for initialization
    void Start () {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<Data>();

        SelectedItemText.SetText("");
        ErrorText.SetText("");

        CancelButton.interactable = false;
        reward = -1;

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
            StartBattle();
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

    public ElementItem GetElementFromMessage(string neededMessage)
    {
        foreach (var item in data.Items)
        {
            var message = item.Key + " +" + item.Value;
            if (message.Equals(neededMessage))
            {
                return new ElementItem(item.Key, item.Value);
            }
        }

        return null;
    }

    public void SelectItem(GameObject item, string message)
    {
        SelectedItemText.SetText(message);
        ErrorText.SetText("");
    }

    public void StartBattle()
    {
        if (SelectedItemText.text.Length < 1)
        {
            ErrorText.SetText("Please, select an element word");
            return;
        }

        if (timer != null)
        {
            // Stop the timer
            StopCoroutine(timer);
        }

        // Create random element for the opponent
        opponentElement = CreateRandomItem();
        playerElement = GetElementFromMessage(SelectedItemText.text);

        // Show elements on the battle board
        int childs = BattleScene.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            DestroyImmediate(BattleScene.GetChild(i).gameObject);
        }
        InstantiniateItem(opponentElement);
        InstantiniateItem(playerElement);

        ItemListPanel.SetActive(false);
        BattlePanel.SetActive(true);

        StartCoroutine("Calculation");
    }

    private ElementItem CreateRandomItem()
    {
        string name = "word";
        int newValue = Random.Range(0, 4);

        string[] elements = new string[4];
        elements[0] = "Fire";
        elements[1] = "Water";
        elements[2] = "Wind";
        elements[3] = "Ground";

        string key = elements[newValue] + "-" + name;

        var value = Random.Range(1, 100);

        return new ElementItem(key, value);
    }

    private void GetBattleResult(out string result)
    {
        result = "";
        if (opponentElement.Key.StartsWith("Water")) 
        {
            if (playerElement.Key.StartsWith("Water"))
            {
                result = "Water Crashes Water!";
                reward = 1;
            }
            else if (playerElement.Key.StartsWith("Fire"))
            {
                result = "Water Extinguishes Fire!";
                reward = -1;
            }
            else if (playerElement.Key.StartsWith("Wind"))
            {
                result = "Wind freezes Water!";
                reward = opponentElement.Value;
            }
            else
            {
                result = "Draw";
                reward = 0;
            }
        }
        else if (opponentElement.Key.StartsWith("Fire"))
        {
            if (playerElement.Key.StartsWith("Fire"))
            {
                result = "Fire Crashes Fire!";
                reward = 1;
            }
            else if (playerElement.Key.StartsWith("Ground"))
            {
                result = "Fire Melts Ground!";
                reward = -1;
            }
            else if (playerElement.Key.StartsWith("Water"))
            {
                result = "Water Extinguishes Fire!";
                reward = opponentElement.Value;
            }
            else
            {
                result = "Draw";
                reward = 0;
            }
        }
        else if (opponentElement.Key.StartsWith("Ground"))
        {
            if (playerElement.Key.StartsWith("Ground"))
            {
                result = "Ground Crashes Ground!";
                reward = 1;
            }
            else if (playerElement.Key.StartsWith("Wind"))
            {
                result = "Ground Stops Wind!";
                reward = -1;
            }
            else if (playerElement.Key.StartsWith("Fire"))
            {
                result = "Fire Melts Ground!";
                reward = opponentElement.Value;
            }
            else
            {
                result = "Draw";
                reward = 0;
            }
        }
        else if (opponentElement.Key.StartsWith("Wind"))
        {
            if (playerElement.Key.StartsWith("Wind"))
            {
                result = "Wind Crashes Wind!";
                reward = 1;
            }
            else if (playerElement.Key.StartsWith("Water"))
            {
                result = "Wind Freezes Water!";
                reward = -1;
            }
            else if (playerElement.Key.StartsWith("Ground"))
            {
                result =  "Ground Stops Wind!";
                reward = opponentElement.Value;
            }
            else
            {
                result = "Draw";
                reward = 0;
            }
        }
    }

    private void RewardPlayer()
    {
        if (reward == -1)
        {
            data.RemoveItem(playerElement.Key);
        }
        else if (reward > 0)
        {
            data.UpdateData(playerElement.Key, playerElement.Value + reward);
        }
    }

    IEnumerator Calculation()
    {
        yield return new WaitForSeconds(0.5f);

        // Calculate the Battle outcome
        string resultMessage = "";
        GetBattleResult(out resultMessage);

        ElementReactionText.gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText(resultMessage);

        yield return new WaitForSeconds(1.5f);

        ElementReactionText.SetTrigger("Show");

        RewardPlayer();

        yield return new WaitForSeconds(1.5f);

        CancelButton.interactable = true;
    }

    public void InstantiniateItem(ElementItem opponentData)
    {
        GameObject item = new GameObject();
        if (opponentData.Key.StartsWith("Ground"))
        {
            item = Instantiate(GroundPrefab, BattleScene);
        }
        else if (opponentData.Key.StartsWith("Wind"))
        {
            item = Instantiate(WindPrefab, BattleScene);
        }
        else if (opponentData.Key.StartsWith("Fire"))
        {
            item = Instantiate(FirePrefab, BattleScene);
        }
        else
        {
            item = Instantiate(WaterPrefab, BattleScene);
        }

        item.GetComponentInChildren<TextMeshProUGUI>().SetText(opponentData.Key);
    }
}
