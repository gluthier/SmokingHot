using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using static SimulationManager;

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
        public int year;
        public string companyName;
        public float money;
        public PopularityLevel popularity;
        public float consumers;
        public float manufacturing;
        public float lobbying;
        public float adCampaigns;
        public CigarettePackEntity cigarettePackProduced;
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

    public void SpendMoney(int amount)
    {
        simulationManager.SpendMoney(amount);
    }

    public void ImpactReputation(int amount)
    {
        simulationManager.ImpactReputation(amount);
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

        string csv = "Year,Money,Popularity,Consumers,Manufacturing,Lobbying,Ad campaings,Toxicity,Addiction";

        foreach (GameUIData d in gameDataReport)
        {
            csv += $"\n{d.year},{d.money},{d.popularity},{d.consumers},{d.manufacturing},{d.lobbying},{d.adCampaigns},{d.cigarettePackProduced.GetToxicityDescription()},{d.cigarettePackProduced.GetAddictionDescription()}";
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
