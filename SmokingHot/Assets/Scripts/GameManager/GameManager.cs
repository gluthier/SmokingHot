using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using static SimulationManager;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameDifficulty
    {
        Easy,
        Normal,
        Hard
    }
    private GameDifficulty gameDifficulty;

    public GameUI gameUI;
    public CameraManager cameraManager;
    public SkillTreeManager skillTreeManager;
    public MainMenuForm mainMenuForm;
    public WorldEventUI worldEventUI;
    public AudioSource audioSource;
    public AudioClip newEventSound;
    public EndgameScreen endgameScreen;
    public TooltipManager tooltipManager;
    public Music musicManager;

    private SimulationManager simulationManager;

    public CoinManager coinManager;
    public CustomerManager customerManager;

#if DEBUG
    private bool DEBUG_isHeadlessModeOnAcceptEvents;
    private bool DEBUG_isHeadlessModeOnRefuseEvents;
    private List<GameState> gameStateReports;
#endif

    public struct GameState
    {
        public int year;
        public string companyName;
        public float money;
        public float numConsumers;
        public float deads;
        public float totalDeads;
        public PopularityLevel popularity;
        public float manufacturingCosts; // cout prod cigarette
        public float lobbyingCosts; // dépense en lobbying
        public float adCampaignsCosts;  // dépense en campagne
        public CigarettePackEntity cigarettePackProduced;
        public float cigarettePackPrice; // +
        public float deadConsumers; // -
        public float newConsumers; // +
        public float lostConsumers; // -
        public float yearlyMoneyBonus; // +
        public string iaStrategy;
    }

    private void Awake()
    {
        ResetGameTitleMaterial();
        gameDifficulty = GameDifficulty.Normal;
    }

    private void Start()
    {
        // Ensure there is an AudioSource component attached
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (endgameScreen != null)
        {
            endgameScreen.gameObject.SetActive(false);
        }
    }

    private void SetupGame()
    {
        Screen.fullScreen = true;
    }

    public SimulationManager GetSimulationManager() { return simulationManager; }
    public void enterGame(string companyName)
    {
        // Carefull: the order of execution is important to avoid null references
        displayMainUI();
        StartCoroutine(FadeOutTitle());
        cameraManager.SwitchPlayingCamera();

        SetupSimulationManager(companyName);
        simulationManager.StartSimulation(gameDifficulty);

        SetupWorldEventUI();
    }

    public void SetGameDifficulty(GameDifficulty difficulty)
    {
        gameDifficulty = difficulty;
    }

    private IEnumerator FadeOutTitle()
    {
        TextMeshProUGUI gameTitle = transform.Find("GameTitle").GetComponent<TextMeshProUGUI>();
        float dilate = 0.15f;

        while (true)
        {
            dilate -= 1.0f * Time.deltaTime;

            if (dilate <= -0.6f)
            {
                gameTitle.gameObject.SetActive(false);
                yield break;
            }

            gameTitle.materialForRendering.SetFloat("_FaceDilate", dilate);
            gameTitle.SetMaterialDirty();
            gameTitle.ForceMeshUpdate(true);

            yield return null;
        }
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

        simulationManager.Init(this, companyName, gameDifficulty);
    }

    public void PopulateMainUI(bool showUpdate)
    {
        GameState playerGameState = simulationManager.RetrievePlayerGameState();
        GameState iaGameState = simulationManager.RetrieveIAGameState();

#if DEBUG
        if (DEBUG_isHeadlessModeOnAcceptEvents || DEBUG_isHeadlessModeOnRefuseEvents)
        {
            gameStateReports.Add(playerGameState);
            return;
        }
#endif

        gameUI.PopulateMainUI(playerGameState, iaGameState, showUpdate);
    }

    public void DisplayEndScreen()
    {
        if (endgameScreen != null)
        {
            ResetGameTitleMaterial(); // to fix material animation at start

            endgameScreen.gameObject.SetActive(true);

            endgameScreen.PopulateUI(
                simulationManager.GetSimulatedCompanies());
        }
    }

    public float GetPlayerMoney()
    {
        return simulationManager.RetrievePlayerGameState().money;
    }

    public float GetIAMoney()
    {
        return simulationManager.RetrieveIAGameState().money;
    }

    public void PopulateWorldEventUI(WorldEvent worldEvent)
    {
#if DEBUG
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
#endif

        worldEventUI.DisplayEvent(worldEvent, HandleEndEvent);
    }

    private void HandleEndEvent()
    {
        HideWorldEventUI();
        simulationManager.PlaySimulation();

        PopulateMainUI(true);
    }

    public void ShowWorldEvent()
    {
        tooltipManager.HideAllTooltips();
        audioSource.PlayOneShot(newEventSound);
        worldEventUI.gameObject.SetActive(true);
    }

    public void HideWorldEventUI()
    {
        worldEventUI.gameObject.SetActive(false);
    }

    void Update()
    {
#if DEBUG
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (simulationManager != null && simulationManager.isSimulationOn)
                return;

            FindFirstObjectByType<MainMenuForm>()?.gameObject.SetActive(false);
            enterGame("Hot Smokes");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (simulationManager == null)
                return;

            RestartSimulation();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            if (simulationManager == null || !simulationManager.isSimulationOn)
                return;

            enterGame("Hot Smokes");

            gameStateReports = new List<GameState>(50);
            simulationManager.gameMinutesLength = 1 / 200f;
            DEBUG_isHeadlessModeOnAcceptEvents = true;

            StartCoroutine(
                DEBUG_simulateGameGetReport());
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (simulationManager == null || !simulationManager.isSimulationOn)
                return;

            enterGame("Hot Smokes");

            gameStateReports = new List<GameState>(50);
            simulationManager.gameMinutesLength = 1 / 200f;
            DEBUG_isHeadlessModeOnRefuseEvents = true;

            StartCoroutine(
                DEBUG_simulateGameGetReport());
        }
#endif

        if (Input.GetKeyDown(KeyCode.Return))
        {
            MainMenuForm mainMenuForm = FindFirstObjectByType<MainMenuForm>();

            if (mainMenuForm.isActiveAndEnabled)
            {
                mainMenuForm.TaskOnClick();
            }
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            // Toggle fullscreen
            Screen.fullScreen = !Screen.fullScreen;
        }

        if (GameHasStarted())
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    GameObject collider = raycastHit.transform.gameObject;

                    //Check for mouse click 
                    if (Input.GetMouseButtonDown(0))
                    {
                        //Our custom method. 
                        CurrentClickedGameObject(collider);
                    }
                    else
                    {
                        tooltipManager.ShowToolTipIfMatchTag(collider);
                    }
                }
                else
                {
                    tooltipManager.HideAllTooltips();
                }
            }
        }
    }

    public void UpdateYearLoadingUI(float percent)
    {
        percent = Mathf.Clamp01(percent);
        gameUI.UpdateYearLoaderWidth(percent);
    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (IsTagAnyBuilding(gameObject) && skillTreeManager.GetCurrentActivePanel() == -1)
        {
            tooltipManager.HideAllTooltips();
            skillTreeManager.ShowPanel(gameObject);
            simulationManager.PauseSimulation();
        }
    }

    private bool IsTagAnyBuilding(GameObject collider)
    {
        return collider.CompareTag(Env.PublicityBuildingsTag) ||
            collider.CompareTag(Env.PopularityBuildingsTag) ||
            collider.CompareTag(Env.ManufacturingBuildingsTag);
    }

    private void ShowChestToolTipIfTagMatch(GameObject collider)
    {
        if (collider.CompareTag(Env.CustomerSharesTag) ||
            collider.CompareTag(Env.ChestTag) ||
            IsTagAnyBuilding(collider))
        {
            tooltipManager.ShowToolTipIfMatchTag(collider);
        }
    }

#if DEBUG
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
#endif

    public void HandleSkillEffect(List<string> effects, Building.TYPE buildingType,
        bool isPlayer)
    {
        CompanyEntity company = isPlayer ?
            simulationManager.GetPlayerCompany() :
            simulationManager.GetIACompany();

        simulationManager.ApplyEffect(effects, buildingType, company);
    }

    public void PlaySimulation()
    {
        simulationManager.PlaySimulation();
    }

    public void RestartSimulation()
    {
        coinManager.ClearAllCoins();
        simulationManager.StartSimulation(gameDifficulty);
        skillTreeManager.ResetSkillTreeManager();
        musicManager.PlayRandomClip();
        endgameScreen.gameObject.SetActive(false);
        HideWorldEventUI();
    }

    private bool GameHasStarted()
    {
        return simulationManager != null &&
            simulationManager.isSimulationOn &&
            mainMenuForm != null &&
            !mainMenuForm.isActiveAndEnabled;
    }

    private void OnApplicationQuit()
    {
        ResetGameTitleMaterial();
    }

    private void ResetGameTitleMaterial()
    {
        TextMeshProUGUI gameTitle = transform.Find("GameTitle").GetComponent<TextMeshProUGUI>();
        gameTitle.materialForRendering.SetFloat("_FaceDilate", 0.15f);
        gameTitle.SetMaterialDirty();
        gameTitle.ForceMeshUpdate(true);
    }
}
