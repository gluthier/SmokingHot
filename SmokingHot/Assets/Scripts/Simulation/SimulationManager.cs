using System;
using System.Collections.Generic;
using System.Net.Http;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

        float initialMarketShare = playerMarketShare();
        gameManager.customerManager.InitialColors(initialMarketShare);

        gameManager.PopulateMainUI(false);
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
                return "Détesté";
            case PopularityLevel.Disliked:
                return "Pas apprécié";
            case PopularityLevel.Neutral:
                return "Neutre";
            case PopularityLevel.Appreciated:
                return "Apprécié";
            case PopularityLevel.Loved:
                return "Aimé";
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

    public GameManager.GameState RetrievePlayerGameState()
    {
        return RetrieveGameStateFromCompany(playerCompany);
    }

    public GameManager.GameState RetrieveIAGameState()
    {
        return RetrieveGameStateFromCompany(iaCompany);
    }

    private GameManager.GameState RetrieveGameStateFromCompany(CompanyEntity company)
    {
        return new GameManager.GameState
        {
            year = yearPassed,
            companyName = company.companyName,
            money = company.money,
            popularity = company.popularity,
            numConsumers = company.numConsumers,
            manufacturingCosts = company.manufacturingCosts,
            lobbyingCosts = company.lobbyingCosts,
            adCampaignsCosts = company.adCampaignsCosts,
            cigarettePackProduced = company.cigarettePackProduced,
            cigarettePackPrice = company.cigarettePackPrice,
            deadConsumers = company.deadConsumers,
            newConsumers = company.newConsumers,
            lostConsumers = company.lostConsumers,
            yearlyMoneyBonus = company.bonusMoney
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

            WorldEvent worldEvent = new NoEvent();
            if (yearPassed < totalYearSimulated)
            {
                float moneyGained = HandleEndOfSimulatedYear();
                gameManager.coinSpawner.spawnCoins(moneyGained);
                gameManager.customerManager.HandleColors(playerMarketShare());

                worldEvent = HandleWorldEvent();

                gameManager.PopulateMainUI(true);
            }
            else
            {
                HandleEndOfGame();

                gameManager.PopulateMainUI(false);
            }

            iaManager.ProcessEndOfYear(
                RetrieveIAGameState(), iaCompany, worldEvent);
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

    private WorldEvent HandleWorldEvent()
    {
        if (yearPassed % Env.WorldEventFrequencyYear != 0)
            return new NoEvent();

        isSimulationOn = false;

        WorldEvent worldEvent =
            worldEventManager.CreateWorldEvent(RetrievePlayerGameState());

        gameManager.PopulateWorldEventUI(worldEvent);
        gameManager.ShowWorldEvent();

        return worldEvent;
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

    public void ApplyEffect(List<string> effects, int index)
    {
        CompanyEntity company = GetPlayerCompany();

        foreach (string effect in effects)
        {
            string[] skillParts = effect.Split(" ");
            int amount = Int32.Parse(skillParts[2]);

            switch (skillParts[0])
            {
                case "Up":
                    switch (skillParts[1])
                    {
                        case "money":
                            company.money += amount;
                            break;
                        case "popularity":
                            company.popularity += amount;
                            break;
                        case "packetPrice":
                            company.cigarettePackPrice += amount;
                            break;
                        case "yearlyBonus":
                            company.bonusMoney += amount;
                            break;
                        case "nbConsumers":
                            company.numConsumers += amount;
                            break;
                        case "deadConsumers":
                            company.deadConsumers += amount;
                            break;
                        default:
                            Debug.Log(effect);
                            break;

                    }
                    break;
                case "Down":
                    switch (skillParts[1])
                    {
                        case "packetPrice":
                            company.cigarettePackPrice -= amount;
                            break;
                        case "nbConsumers":
                            company.numConsumers -= amount;
                            break;
                        case "popularity":
                            company.popularity -= amount;
                            break;
                        case "adCost":
                            company.adCampaignsCosts -= amount;
                            break;
                        case "manuCost":
                            company.manufacturingCosts -= amount;
                            break;
                        case "lobbyCost":
                            company.lobbyingCosts -= amount;
                            break;
                        case "deadConsumers":
                            company.deadConsumers -= amount;
                            break;
                        case "lostConsumers":
                            company.lostConsumers -= amount;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
