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

    public CoinSpawner coinSpawner;

    #region DEBUG ATTRIBUTES
    private bool DEBUG_isHeadlessModeOnAcceptEvents;
    private bool DEBUG_isHeadlessModeOnRefuseEvents;
    private List<GameState> gameStateReports;
    #endregion

    public struct GameState
    {
        public int year;
        public string companyName;
        public float money;
        public PopularityLevel popularity;
        public float numConsumers;
        public float manufacturingCosts;
        public float lobbyingCosts;
        public float adCampaignsCosts;
        public CigarettePackEntity cigarettePackProduced;
        public float cigarettePackPrice;
        public float deadConsumers;
        public float newConsumers;
        public float lostConsumers;
        public float yearlyMoneyBonus;
    }

    public void enterGame(string companyName)
    {
        // Carefull: the order of execution is important to avoid null references
        displayMainUI();
        cameraManager.SwitchPlayingCamera();

        SetupSimulationManager(companyName);
        simulationManager.StartSimulation();

        SetupWorldEventUI();
    }

    private void SetupWorldEventUI()
    {
        worldEventUI.Init(simulationManager.GetPlayerCompany());
        HideWorldEventUI();
    }


    private void displayMainUI()
    {
        gameUI.gameObject.SetActive(true);
    }

    private void SetupSimulationManager(string companyName)
    {
        simulationManager = gameObject.GetComponent<SimulationManager>();

        if (simulationManager == null)
        {
            simulationManager = gameObject.AddComponent<SimulationManager>();
        }

        simulationManager.Init(this, companyName);
    }

    public void PopulateMainUI(GameState gameState, bool showUpdate)
    {
        if (DEBUG_isHeadlessModeOnAcceptEvents || DEBUG_isHeadlessModeOnRefuseEvents)
        {
            gameStateReports.Add(gameState);
            return;
        }

        gameUI.PopulateMainUI(gameState, showUpdate);
    }

    public void PopulateWorldEventUI(WorldEvent worldEvent)
    {
        if (DEBUG_isHeadlessModeOnAcceptEvents)
        {
            worldEvent.AcceptEvent(simulationManager.GetPlayerCompany());
            HandleEndEvent();
            return;
        }
        else if (DEBUG_isHeadlessModeOnRefuseEvents)
        {
            worldEvent.RefuseEvent(simulationManager.GetPlayerCompany());
            HandleEndEvent();
            return;
        }

        worldEventUI.DisplayEvent(worldEvent, HandleEndEvent);
    }

    private void HandleEndEvent()
    {
        HideWorldEventUI();
        simulationManager.ContinueSimulation();

        PopulateMainUI(
            simulationManager.RetrieveGameState(), true);
    }

    public void ShowWorldEvent()
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
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (simulationManager == null || !simulationManager.isSimulationOn)
                return;

            enterGame("Big Tobacco");

            gameStateReports = new List<GameState>(50);
            simulationManager.gameMinutesLength = 1 / 200f;
            DEBUG_isHeadlessModeOnAcceptEvents = true;

            StartCoroutine(
                DEBUG_simulateGameGetReport());
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (simulationManager == null || !simulationManager.isSimulationOn)
                return;

            enterGame("Big Tobacco");

            gameStateReports = new List<GameState>(50);
            simulationManager.gameMinutesLength = 1 / 200f;
            DEBUG_isHeadlessModeOnRefuseEvents = true;

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

        string csv = "Year,Money,Popularity,Consumers,Manufacturing,Lobbying,Ad campaings,Toxicity,Addiction,cigarettePackPrice,deadConsumers,newConsumers,lostConsumers,yearlyMoneyBonus";

        foreach (GameState d in gameStateReports)
        {
            csv += $"\n{d.year},{d.money},{d.popularity},{d.numConsumers},{d.manufacturingCosts},{d.lobbyingCosts},{d.adCampaignsCosts},{d.cigarettePackProduced.GetToxicityDescription()},{d.cigarettePackProduced.GetAddictionDescription()},{d.cigarettePackPrice},{d.deadConsumers},{d.newConsumers},{d.lostConsumers},{d.yearlyMoneyBonus}";
        }

        string eventMode = DEBUG_isHeadlessModeOnAcceptEvents ? "accept" : "refuse";

        string folder = Application.persistentDataPath;
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_hhmmss");
        string filePath = Path.Combine(folder, $"{timestamp}_{eventMode}_report.csv");

        using (var writer = new StreamWriter(filePath, false))
        {
            writer.Write(csv);
        }


        Debug.Log($"Event mode: {eventMode} -- CSV file report written to \"{filePath}\"");
    }
    #endregion
}
