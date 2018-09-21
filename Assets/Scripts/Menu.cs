using UnityEngine;

public class Menu : MonoBehaviour {

    public GameObject MainPanel;
    public GameObject ImportPanel;
    public GameObject CreatePanel;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowImport()
    {
        MainPanel.SetActive(false);
        CreatePanel.SetActive(false);

        ImportPanel.SetActive(true);
    }

    public void ShowMain()
    {
        ImportPanel.SetActive(false);
        CreatePanel.SetActive(false);

        MainPanel.SetActive(true);
    }

    public void ShowCreate()
    {
        MainPanel.SetActive(false);
        ImportPanel.SetActive(false);

        CreatePanel.SetActive(true);
    }

    public void ShowProfile()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Profile");
    }
}
