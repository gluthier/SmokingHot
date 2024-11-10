using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameUI gameUI;
    public CameraManager cameraManager;
    public string companyName;
    public SkillTreeManager skillTreeManager;
    private SimulationManager simulationManager;

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

    void Update()
    {
        //Check for mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    //Our custom method. 
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }
    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.tag == "Building")
        {
           skillTreeManager.ShowPanel(gameObject);            
        }
    }
}
