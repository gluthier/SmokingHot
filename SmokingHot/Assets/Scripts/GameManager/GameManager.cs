using UnityEngine;
using System.Collections.Generic;
using static SimulationManager;

public class GameManager : MonoBehaviour
{
    public GameUI gameUI;
    public CameraManager cameraManager;
    public SkillTreeManager skillTreeManager;
    public WorldEventUI worldEventUI;

    private SimulationManager simulationManager;

    public struct GameUIData
    {
        public string conglomerateName;
        public int year;
        public string continent;
        public float population;
        public float smokerPercentage;
        public float deathSmokerPercentage;
        public float money;
        public float newSmokerAcquisition;
        public float smokerRetention;
        public CigarettePackEntity cigarettePackProduced;
        public Dictionary<AgeBracket, PopularityLevel> popularityByAgeBracket;
        public List<AdCampaignEntity> adCampaigns;
    }

    public void enterGame(string conglomerateName)
    {
        SetupWorldEventUI();

        displayMainUI();
        cameraManager.SwitchPlayingCamera();

        SetupSimulationManager(conglomerateName);
        simulationManager.StartSimulation();
    }

    private void SetupWorldEventUI()
    {
        worldEventUI.Init(this);
        HideWorldEventUI();
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

    public void PopulateMainUI(GameUIData gameUIData, bool showUpdate)
    {
        gameUI.PopulateMainUI(gameUIData, showUpdate);
    }

    public void PopulateWorldEventUI(WorldEventSO worldEvent)
    {
        worldEventUI.DisplayEvent(worldEvent, HandleEndEvent);
    }

    private void HandleEndEvent()
    {
        HideWorldEventUI();
        simulationManager.ContinueSimulation();
    }

    public void SpendMoney(float amount)
    {
        simulationManager.SpendMoney(amount);
    }

    public void ImpactReputation(AgeBracket ageBracket, int amount)
    {
        simulationManager.ImpactReputation(ageBracket, amount);
    }

    // successPercentage must be between 0.0 and 1.0
    public void BlockAdvertisment(float successPercentage)
    {
        if (Random.value < successPercentage)
        {
            simulationManager.BlockAdvertisment();
        }
    }


    public void DisplayWorldEventUI()
    {
        worldEventUI.gameObject.SetActive(true);
    }

    public void HideWorldEventUI()
    {
        worldEventUI.gameObject.SetActive(false);
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
