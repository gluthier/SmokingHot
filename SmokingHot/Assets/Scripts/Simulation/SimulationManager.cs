using System.Collections.Generic;
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

    private bool isSimulationOn;
    private int yearPassed;
    private float timePassed;

    private float totalYearSimulated;
    public float gameMinutesLength;

    private CompanyEntity playerCompany;
    private CompanyEntity iaCompany;
    private string playerCompanyName = "";

    private GameManager gameManager;
    private WorldEventManager worldEventManager;

    public void Init(GameManager gameManager, string companyName)
    {
        this.gameManager = gameManager;
        yearPassed = 0;

        SetupWorldEventManager(gameManager);

        LoadData(
            GameDataLoader.Load());

        SetPlayerCompanyName(companyName);

        gameManager.PopulateMainUI(
            RetrieveUIValues(), false);
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
                return "Hated";
            case PopularityLevel.Disliked:
                return "Disliked";
            case PopularityLevel.Neutral:
                return "Neutral";
            case PopularityLevel.Appreciated:
                return "Appreciated";
            case PopularityLevel.Loved:
                return "Loved";
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

    private void SetupWorldEventManager(GameManager gameManager)
    {
        worldEventManager = gameObject.AddComponent<WorldEventManager>();
        worldEventManager.Init(gameManager);
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

    private GameManager.GameUIData RetrieveUIValues()
    {
        return new GameManager.GameUIData
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

            if (yearPassed < totalYearSimulated)
            {
                float playerMoneyGained = HandleEndOfSimulatedYear();
                gameManager.coinSpawner.spawnCoins(playerMoneyGained);

                HandleWorldEvent();

                gameManager.PopulateMainUI(
                    RetrieveUIValues(), true);
            }
            else
            {
                HandleEndOfGame();

                gameManager.PopulateMainUI(
                    RetrieveUIValues(), false);
            }
        }
    }

    private float HandleEndOfSimulatedYear()
    {
        float playerMoneyGained = playerCompany.EndFiscalYear();
        iaCompany.EndFiscalYear();

        return playerMoneyGained;
    }

    private void HandleWorldEvent()
    {
        if (yearPassed % Env.WorldEventFrequencyYear != 0)
            return;

        isSimulationOn = false;

        WorldEvent worldEvent =
            worldEventManager.CreateWorldEvent(yearPassed);

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
