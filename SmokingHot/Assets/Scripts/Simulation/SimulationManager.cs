using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public enum AgeBracket
    {
        Kid,
        Teenager,
        YoungAdult,
        Adult,
        Senior
    }

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
    private float gameMinutesLength;
    private List<ConglomerateEntity> conglomerates;
    private string playerConglomerateName = "";

    private GameManager gameManager;
    private WorldEventManager worldEventManager;

    public void Init(GameManager gameManager, string conglomerateName)
    {
        this.gameManager = gameManager;
        yearPassed = 0;

        SetupWorldEventManager(gameManager);

        LoadData(
            GameDataLoader.Load());

        SetPlayerConglomerateName(conglomerateName);

        gameManager.PopulateMainUI(
            RetrieveUIValues(), false);
    }

    public void StartSimulation()
    {
        isSimulationOn = true;
        ResetSimulation();
    }

    public void SpendMoney(int amount)
    {
        conglomerates[Env.PlayerConglomerateID].SpendMoney(amount);
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

    private void SetPlayerConglomerateName(string conglomerateName)
    {
        playerConglomerateName = conglomerateName;
        conglomerates[Env.PlayerConglomerateID].SetConglomerateName(playerConglomerateName);
    }

    private void LoadData(GameData gameData)
    {
        totalYearSimulated = gameData.totalYearSimulated;
        gameMinutesLength = gameData.gameMinutesLength;

        conglomerates = new List<ConglomerateEntity>();

        foreach (ConglomerateData conglomerateData in gameData.conglomerates)
        {
            conglomerates.Add(new ConglomerateEntity(conglomerateData));
        }
    }

    private GameManager.GameUIData RetrieveUIValues()
    {
        ConglomerateEntity playerConglomerate = conglomerates[Env.PlayerConglomerateID];

        return new GameManager.GameUIData
        {
            conglomerateName = playerConglomerate.conglomerateName,
            year = yearPassed,
            continent = playerConglomerate.continentName,
            money = playerConglomerate.totalMoney,
            population = playerConglomerate.population,
            smokerPercentage = playerConglomerate.smokerPercentage,
            newSmokerAcquisition = playerConglomerate.newSmokerAcquisition,
            smokerRetention = playerConglomerate.smokerRetention,
            deathSmokerPercentage = playerConglomerate.deathSmokerPercentage,
            cigarettePackProduced = playerConglomerate.cigarettePackProduced,
            popularityByAgeBracket = playerConglomerate.popularityByAgeBracket,
            adCampaigns = playerConglomerate.adCampaigns
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
                HandleEndOfSimulatedYear();

                gameManager.PopulateMainUI(
                    RetrieveUIValues(), true);

                HandleWorldEvent();
            }
            else
            {
                HandleEndOfGame();

                gameManager.PopulateMainUI(
                    RetrieveUIValues(), false);
            }
        }
    }

    private void HandleEndOfSimulatedYear()
    {
        foreach (ConglomerateEntity conglomerate in conglomerates)
        {
            conglomerate.EndFiscalYear();
        }
    }

    private void HandleWorldEvent()
    {
        if (yearPassed % Env.WorldEventFrequencyYear != 0)
            return;

        isSimulationOn = false;
        worldEventManager.CreateWorldEvent();
        gameManager.DisplayWorldEventUI();
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

        SetPlayerConglomerateName(playerConglomerateName);
    }
}
