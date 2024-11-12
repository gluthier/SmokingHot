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

    private GameManager gameManager;
    private WorldEventManager worldEventManager;


    public void Start()
    {
        isSimulationOn = false;
        worldEventManager = gameObject.AddComponent<WorldEventManager>();
    }

    public void Init(GameManager gameManager, string conglomerateName)
    {
        this.gameManager = gameManager;

        LoadData(
            GameDataLoader.Load());

        SetPlayerConglomerateName(conglomerateName);

        gameManager.PopulateMainUI(
            RetrieveUIValues());
    }

    public void SetPlayerConglomerateName(string conglomerateName)
    {
        conglomerates[Env.PlayerConglomerateID].SetConglomerateName(conglomerateName);
    }

    public void StartSimulation()
    {
        Env.PrintDebug($"StartSimulation");

        isSimulationOn = true;
        ResetSimulation();
    }

    public void LoadData(GameData gameData)
    {
        totalYearSimulated = gameData.totalYearSimulated;
        gameMinutesLength = gameData.gameMinutesLength;

        conglomerates = new List<ConglomerateEntity>();

        foreach (ConglomerateData conglomerateData in gameData.conglomerates)
        {
            conglomerates.Add(new ConglomerateEntity(conglomerateData));
        }
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

    void Update()
    {
        HandleSimulatedTime();
    }

    private GameManager.GameUIData RetrieveUIValues()
    {
        ConglomerateEntity playerConglomerate = conglomerates[Env.PlayerConglomerateID];

        return new GameManager.GameUIData
        {
            conglomerateName = playerConglomerate.conglomerateName,
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
            }
            else
            {
                HandleEndOfGame();
            }
        }
    }

    private void HandleEndOfSimulatedYear()
    {
        Env.PrintDebug($"HandleSimulatedTime, yearPassed: {yearPassed}");

        foreach (ConglomerateEntity conglomerate in conglomerates)
        {
            conglomerate.EndFiscalYear();
        }
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
    }
}
