using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using static SimulationManager;
using static GameManager;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameUI gameUI;
    public CameraManager cameraManager;
    public SkillTreeManager skillTreeManager;
    public WorldEventUI worldEventUI;

    private SimulationManager simulationManager;
    private WorldEventManager worldEventManager;

    public CoinSpawner coinSpawner;

    #region DEBUG ATTRIBUTES
    private bool DEBUG_isHeadlessModeOn;
    private List<GameManager.GameUIData> gameDataReport;
    #endregion

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
        SetupWorldEventManager();

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

    private void SetupWorldEventManager()
    {
        worldEventManager = gameObject.AddComponent<WorldEventManager>();
        worldEventManager.Init(this);
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
        if (DEBUG_isHeadlessModeOn)
        {
            gameDataReport.Add(gameUIData);
            return;
        }

        gameUI.PopulateMainUI(gameUIData, showUpdate);
    }

    public void PopulateWorldEventUI(WorldEventSO worldEvent)
    {
        if (DEBUG_isHeadlessModeOn)
        {
            worldEvent.AcceptEvent(this);
            HandleEndEvent();
            return;
        }

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
        if (UnityEngine.Random.value < successPercentage)
        {
            simulationManager.BlockAdvertisment();
        }
    }

    public void CreateWorldEvent()
    {
        worldEventUI.gameObject.SetActive(true);
        worldEventManager.CreateWorldEvent();
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
        else if (Input.GetKeyDown(KeyCode.R))
        {
            enterGame("Big Tobacco");

            gameDataReport = new List<GameUIData>(50);
            simulationManager.gameMinutesLength = 1/200f;
            DEBUG_isHeadlessModeOn = true;

            StartCoroutine(
                DEBUG_simulateGameGetReport());
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

    #region DEBUG METHODS
    private IEnumerator DEBUG_simulateGameGetReport()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("Simulated simulation done!");

        string csv = "Year,Population,Money,Smokers,Death,Acquisition,Retention,Packs produced,Ad campaigns";

        foreach (GameUIData d in gameDataReport)
        {
            csv += $"\n{d.year},{d.population},{d.money},{d.smokerPercentage},{d.deathSmokerPercentage},{d.newSmokerAcquisition},{d.smokerRetention},{d.cigarettePackProduced},{d.adCampaigns.Count}";
        }

        string folder = Application.persistentDataPath;
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_hhmmss");
        string filePath = Path.Combine(folder, $"{timestamp}_report.csv");

        using (var writer = new StreamWriter(filePath, false))
        {
            writer.Write(csv);
        }

        Debug.Log($"CSV file report written to \"{filePath}\"");
    }
    #endregion
}
