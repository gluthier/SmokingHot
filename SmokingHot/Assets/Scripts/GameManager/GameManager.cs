using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameUI gameUI;
    public CameraManager cameraManager;
    public string companyName;

    void Start()
    {
        gameUI.gameObject.SetActive(false);
    }

    public void enterGame()
    {
        displayMainUI();
        cameraManager.SwitchPlayingCamera();
    }

    private void displayMainUI()
    {
        gameUI.gameObject.SetActive(true);
        gameUI.companyName.text = companyName;
    }
}
