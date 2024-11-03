using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameUI gameUI;
    public CameraManager cameraManager;
    public string companyName;

    private SimulationManager simulationManager;

    void Start()
    {
        gameUI.gameObject.SetActive(false);
    }

    public void enterGame()
    {
        displayMainUI();
        cameraManager.SwitchPlayingCamera();
        simulationManager.StartSimulation();
    }

    private void displayMainUI()
    {
        gameUI.gameObject.SetActive(true);
        gameUI.companyName.text = companyName;
    }
}
