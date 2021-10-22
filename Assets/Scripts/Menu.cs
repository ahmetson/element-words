using UnityEngine;

public class Menu : MonoBehaviour {

    public GameObject BlockchainPanel;
    public GameObject MainPanel;
    public GameObject ImportPanel;
    public GameObject CreatePanel;

    private Data data;
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<Data>();
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowImport()
    {
        MainPanel.SetActive(false);
        CreatePanel.SetActive(false);
        BlockchainPanel.SetActive(false);

        ImportPanel.SetActive(true);
    }

    public void ShowMain()
    {
        ImportPanel.SetActive(false);
        CreatePanel.SetActive(false);
        BlockchainPanel.SetActive(false);

        MainPanel.SetActive(true);
    }

    public void ShowBlockchain()
    {
        ImportPanel.SetActive(false);
        CreatePanel.SetActive(false);
        MainPanel.SetActive(false);

        BlockchainPanel.SetActive(true);
    }

    public void ShowCreate()
    {
        MainPanel.SetActive(false);
        ImportPanel.SetActive(false);
        BlockchainPanel.SetActive(false);

        CreatePanel.SetActive(true);
    }

    public void ShowProfile()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Profile");
    }

    public void ShowCentralizedNetwork()
    {
        data.NetworkType = Data.NETWORK_TYPE.CENTRALIZED;
        ShowMain();
    }
}
