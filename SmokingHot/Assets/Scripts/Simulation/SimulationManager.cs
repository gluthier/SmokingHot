using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public enum PopularityLevel
    {
        Hated = -2,
        Disliked = -1,
        Neutral = 0,
        Appreciated = 1,
        Loved = 2
    }

    public bool isSimulationOn;
    private int yearPassed;
    private float timePassed;

    private float totalYearSimulated;
    public float gameMinutesLength;

    private CompanyEntity playerCompany;
    private string playerCompanyName = "";

    private CompanyEntity iaCompany;
    private IA_Manager iaManager;

    private GameManager gameManager;
    private WorldEventManager worldEventManager;

    public void Init(GameManager gameManager, string companyName)
    {
        this.gameManager = gameManager;
        yearPassed = 0;

        LoadData(
            GameDataLoader.Load());

        SetPlayerCompanyName(companyName);

        SetupWorldEventManager();
        SetupIAManager();

        gameManager.PopulateMainUI(
            RetrieveGameState(), false);

        float initialMarketShare = playerMarketShare();
        gameManager.customerManager.InitialColors(initialMarketShare);
    }

    public CompanyEntity GetPlayerCompany()
    {
        return playerCompany;
    }

    public CompanyEntity GetIACompany()
    {
        return iaCompany;
    }

    public void StartSimulation()
    {
        isSimulationOn = true;

        ResetSimulation();
    }

    public void ContinueSimulation()
    {
        isSimulationOn = true;
    }

    public static string GetPopularityDescription(PopularityLevel popularity)
    {
        switch (popularity)
        {
            case PopularityLevel.Hated:
                return "D�test�";
            case PopularityLevel.Disliked:
                return "Pas appr�ci�";
            case PopularityLevel.Neutral:
                return "Neutre";
            case PopularityLevel.Appreciated:
                return "Appr�ci�";
            case PopularityLevel.Loved:
                return "Aim�";
            default:
                return "";
        }
    }

    private void Awake()
    {
        isSimulationOn = false;
    }

    private void Update()
    {
        HandleSimulatedTime();
    }

    private void SetupWorldEventManager()
    {
        worldEventManager = gameObject.AddComponent<WorldEventManager>();
        worldEventManager.Init(gameManager);
    }

    private void SetupIAManager()
    {
        iaManager = gameManager.AddComponent<IA_Manager>();
        iaManager.Init(iaCompany, playerCompany, gameManager);
    }

    private void SetPlayerCompanyName(string companyName)
    {
        playerCompanyName = companyName;
        playerCompany.SetCompanyName(playerCompanyName);
    }

    private void LoadData(GameData gameData)
    {
        totalYearSimulated = gameData.totalYearSimulated;
        gameMinutesLength = gameData.gameMinutesLength;

        playerCompany = new CompanyEntity(gameData.companyTemplate, true);
        iaCompany = new CompanyEntity(gameData.companyTemplate, false);
    }

    public GameManager.GameState RetrieveGameState()
    {
        return new GameManager.GameState
        {
            year = yearPassed,
            companyName = playerCompany.companyName,
            money = playerCompany.money,
            popularity = playerCompany.popularity,
            numConsumers = playerCompany.numConsumers,
            manufacturingCosts = playerCompany.manufacturingCosts,
            lobbyingCosts = playerCompany.lobbyingCosts,
            adCampaignsCosts = playerCompany.adCampaignsCosts,
            cigarettePackProduced = playerCompany.cigarettePackProduced,
            cigarettePackPrice = playerCompany.cigarettePackPrice,
            newConsumers = playerCompany.newConsumers,
            lostConsumers = playerCompany.lostConsumers,
            deadConsumers = playerCompany.deadConsumers,
            yearlyMoneyBonus = playerCompany.bonusMoney,
        };
    }

    private void HandleSimulatedTime()
    {
        if (!isSimulationOn)
            return;

        timePassed += Time.deltaTime;

        float yearSimulatedPerRealMinute = totalYearSimulated / gameMinutesLength;
        float secondsForAYearSimulated = 60f / yearSimulatedPerRealMinute;

        if (timePassed >= secondsForAYearSimulated)
        {
            yearPassed += 1;
            timePassed = 0;

            iaManager.ProcessEndOfYear();

            if (yearPassed < totalYearSimulated)
            {
                float moneyGained = HandleEndOfSimulatedYear();
                gameManager.coinSpawner.spawnCoins(moneyGained);
                gameManager.customerManager.HandleColors(playerMarketShare());

                HandleWorldEvent();

                gameManager.PopulateMainUI(
                    RetrieveGameState(), true);
            }
            else
            {
                HandleEndOfGame();

                gameManager.PopulateMainUI(
                    RetrieveGameState(), false);
            }
        }
    }

    private float playerMarketShare()
    {
        return playerCompany.numConsumers / (playerCompany.numConsumers + iaCompany.numConsumers);
    }

    private float HandleEndOfSimulatedYear()
    {
        float moneyGained = playerCompany.EndFiscalYear();
        iaCompany.EndFiscalYear();
        
        return moneyGained;
    }

    private void HandleWorldEvent()
    {
        if (yearPassed % Env.WorldEventFrequencyYear != 0)
            return;

        isSimulationOn = false;

        WorldEvent worldEvent =
            worldEventManager.CreateWorldEvent(RetrieveGameState());

        gameManager.PopulateWorldEventUI(worldEvent);
        gameManager.ShowWorldEvent();
    }

    private void HandleEndOfGame()
    {
        isSimulationOn = false;
    }

    private void ResetSimulation()
    {
        yearPassed = 0;
        timePassed = 0f;

        LoadData(
            GameDataLoader.Load());

        SetPlayerCompanyName(playerCompanyName);
    }
}
