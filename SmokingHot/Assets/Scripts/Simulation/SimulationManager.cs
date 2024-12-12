using System;
using System.Collections;
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
    float secondsForAYearSimulated;

    private CompanyEntity playerCompany;
    private string playerCompanyName = "";

    private CompanyEntity iaCompany;
    private IA_Manager iaManager;

    private GameManager gameManager;
    private WorldEventManager worldEventManager;

    private List<Building.TYPE> firstPlayerInvestment;

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

        firstPlayerInvestment = new List<Building.TYPE>();
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

    public void PauseSimulation()
    {
        isSimulationOn = false;
    }

    public void PlaySimulation()
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

    public List<CompanyEntity> GetSimulatedCompanies()
    {
        return new List<CompanyEntity>
        {
            playerCompany,
            iaCompany
        };
    }

    private void Awake()
    {
        PauseSimulation();
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
        iaManager.Init();
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
        
        float yearSimulatedPerRealMinute = totalYearSimulated / gameMinutesLength;
        secondsForAYearSimulated = 60f / yearSimulatedPerRealMinute;

        playerCompany = new CompanyEntity(gameData.playerCompany, true);
        iaCompany = new CompanyEntity(gameData.iaCompany, false);
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
        return company.RetrieveCompanyGameState(yearPassed, iaManager.GetIAStrategy());
    }

    private void HandleSimulatedTime()
    {
        if (!isSimulationOn)
            return;

        timePassed += Time.deltaTime;

        gameManager.UpdateYearLoadingUI(timePassed / secondsForAYearSimulated);

        if (timePassed >= secondsForAYearSimulated)
        {
            yearPassed += 1;
            timePassed = 0;

            WorldEvent worldEvent = new NoEvent();
            if (yearPassed < totalYearSimulated)
            {
                float moneyGained = HandleEndOfSimulatedYear();
                gameManager.coinSpawner.spawnCoins(moneyGained);
                StartCoroutine(HandleMarketShareWithDelay(2.0f));

                worldEvent = HandleWorldEvent();

                gameManager.PopulateMainUI(true);
            }
            else
            {
                HandleEndOfGame();
                return;
            }

            iaManager.ProcessEndOfYear(
                RetrieveIAGameState(), iaCompany, worldEvent, firstPlayerInvestment);
        }
    }

    private IEnumerator HandleMarketShareWithDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        gameManager.customerManager.HandleMarketShare(playerMarketShare());
    }

    private float playerMarketShare()
    {
        return playerCompany.GetConsumers() / (playerCompany.GetConsumers() + iaCompany.GetConsumers());
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

        PauseSimulation();

        WorldEvent worldEvent =
            worldEventManager.CreateWorldEvent(RetrievePlayerGameState());

        gameManager.PopulateWorldEventUI(worldEvent);
        gameManager.ShowWorldEvent();

        return worldEvent;
    }

    private void HandleEndOfGame()
    {
        PauseSimulation();
        gameManager.DisplayEndScreen();
    }

    private void ResetSimulation()
    {
        yearPassed = 0;
        timePassed = 0f;

        iaManager.ResetIAManager();

        LoadData(
            GameDataLoader.Load());

        SetPlayerCompanyName(playerCompanyName);

        firstPlayerInvestment = new List<Building.TYPE>();
    }

    public void ApplyEffect(List<string> effects, Building.TYPE buildingType,
        CompanyEntity company)
    {
        if (company.IsPlayer() &&
            firstPlayerInvestment.Count == 0)
        {
            firstPlayerInvestment.Add(buildingType);
        }

        // Debug only
        if (!company.IsPlayer())
        {
            string msg = $"IA apply {effects.Count} effects:";
            foreach (string effect in effects)
            {
                msg += effect + ", ";
            }
            Debug.Log(msg);
        }

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
                            company.IncreaseParam(CompanyEntity.Param.Money, amount);
                            break;
                        case "popularity":
                            company.ImpactPopularity(amount);
                            break;
                        case "packetPrice":
                            company.IncreaseParam(CompanyEntity.Param.cigarettePackPrice, amount);
                            break;
                        case "yearlyBonus":
                            company.IncreaseParam(CompanyEntity.Param.BonusMoney, amount);
                            break;
                        case "nbConsumers":
                            company.IncreaseParam(CompanyEntity.Param.Consumers, amount);
                            break;
                        case "deadConsumers":
                            company.IncreaseParam(CompanyEntity.Param.DeadConsumers, amount);
                            break;
                        default:
                            Debug.Log(effect);
                            break;

                    }
                    break;
                case "Down":
                    switch (skillParts[1])
                    {
                        case "money":
                            company.DecreaseParam(CompanyEntity.Param.Money, amount);
                            break;
                        case "packetPrice":
                            company.DecreaseParam(CompanyEntity.Param.cigarettePackPrice, amount);
                            break;
                        case "nbConsumers":
                            company.DecreaseParam(CompanyEntity.Param.Consumers, amount);
                            break;
                        case "popularity":
                            company.ImpactPopularity(-amount);
                            break;
                        case "adCost":
                            company.DecreaseParam(CompanyEntity.Param.AdCampaignsCosts, amount);
                            break;
                        case "manuCost":
                            company.DecreaseParam(CompanyEntity.Param.ManufacturingCosts, amount);
                            break;
                        case "lobbyCost":
                            company.DecreaseParam(CompanyEntity.Param.LobbyingCosts, amount);
                            break;
                        case "deadConsumers":
                            company.DecreaseParam(CompanyEntity.Param.DeadConsumers, amount);
                            break;
                        case "lostConsumers":
                            company.DecreaseParam(CompanyEntity.Param.LostConsumers, amount);
                            break;
                        default:
                            Debug.Log(effect);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        if (company.IsPlayer())
            gameManager.PopulateMainUI(true);
    }
}
