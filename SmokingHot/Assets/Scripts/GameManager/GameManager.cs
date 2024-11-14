using UnityEngine;
using System.Collections.Generic;
using static SimulationManager;

public class GameManager : MonoBehaviour
{
    public GameUI gameUI;
    public CameraManager cameraManager;
    public SkillTreeManager skillTreeManager;
    private SimulationManager simulationManager;

    public struct GameUIData
    {
        public string conglomerateName;
        public string continent;
        public long population;
        public float smokerPercentage;
        public float deathSmokerPercentage;
        public double money;
        public float newSmokerAcquisition;
        public float smokerRetention;
        public CigarettePackEntity cigarettePackProduced;
        public Dictionary<AgeBracket, PopularityLevel> popularityByAgeBracket;
        public List<AdCampaignEntity> adCampaigns;
    }

    public void enterGame(string conglomerateName)
    {
        displayMainUI();
        cameraManager.SwitchPlayingCamera();

        SetupSimulationManager(conglomerateName);
        simulationManager.StartSimulation();
    }

    private void displayMainUI()
    {
        gameUI.gameObject.SetActive(true);
    }

    private void SetupSimulationManager(string conglomerateName)
    {
        simulationManager = gameObject.GetComponent<SimulationManager>();

        if (simulationManager == null)
        {
            simulationManager = gameObject.AddComponent<SimulationManager>();
        }

        simulationManager.Init(this, conglomerateName);
    }

    public void PopulateMainUI(GameUIData gameUIData)
    {
        gameUI.PopulateMainUI(gameUIData);
    }

    void Update()
    {
        #region DEBUG
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FindFirstObjectByType<StartButtonClick>().gameObject.SetActive(false);
            enterGame("Big Tobacco");
        }
        #endregion

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
