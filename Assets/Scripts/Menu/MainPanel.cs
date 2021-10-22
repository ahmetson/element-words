using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {

    public GameObject PlayButton;
    public GameObject ImportButton;
    public GameObject CreateButton;

    private Data data;
    void OnEnable ()
    {
        Button playButton = PlayButton.GetComponent<Button>();

        data = GameObject.FindGameObjectWithTag("Data").GetComponent<Data>();
        if (data.NetworkType == Data.NETWORK_TYPE.CENTRALIZED)
        {
            ImportButton.SetActive(false);
            CreateButton.SetActive(false);

            playButton.interactable = true;
        } else
        {
            ImportButton.SetActive(true);
            CreateButton.SetActive(true);

            playButton.interactable = false;
        }
    }
}
