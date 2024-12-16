using System.Collections.Generic;
using System;
using UnityEngine;
using static GameManager;
using static WorldEvent;
using System.Linq;

public class IA_Manager : MonoBehaviour
{
    private EventStateMachine eventStateMachine;
    private SkillStateMachine skillStateMachine;
    private VirtualSkillTreeManager virtualSkillTreeManager;

    private List<GameState> iaStateReports;

    private int yearPassed;

    private float startMoneyChangeEventState = 0;
    private float startConsummersChangeEventState = 0;
    private float startCostsChangeEventState = 0;

    public void Init()
    {
        eventStateMachine = new EventStateMachine();
        skillStateMachine = new SkillStateMachine();

        iaStateReports = new List<GameState>();

        InitVirtualSkillTreeManager();
    }

    private void InitVirtualSkillTreeManager()
    {
        virtualSkillTreeManager = gameObject.AddComponent<VirtualSkillTreeManager>();
        virtualSkillTreeManager.Init();
    }

    public void ProcessEndOfYear(GameState iaStatReport, CompanyEntity iaCompany,
        WorldEvent worldEvent, int yearPassed)
    {
        this.yearPassed = yearPassed;
        iaStateReports.Add(iaStatReport);

        HandleChangeOfEventState();
        HandleChangeOfSkillState(iaCompany);

        HandleWorldEvent(iaCompany, worldEvent);
    }

    public string GetIAStrategy()
    {
        string eventStrat = "";
        string skillStrat = "";

        switch (eventStateMachine.CurrentState)
        {
            case EventProcessState.MaxMoney:
                eventStrat = Env.IA_Strategy_MaxMoney;
                break;
            case EventProcessState.MaxConsumers:
                eventStrat = Env.IA_Strategy_MaxCustommer;
                break;
            case EventProcessState.MinCosts:
            default:
                eventStrat = Env.IA_Strategy_MinCosts;
                break;
        }

        switch (skillStateMachine.CurrentState)
        {
            case SkillProcessState.InvestInManufacturing:
                skillStrat = Env.IA_Strategy_InvestInManufacturing;
                break;
            case SkillProcessState.InvestInAds:
                skillStrat = Env.IA_Strategy_InvestInAds;
                break;
            case SkillProcessState.InvestInPopularity:
                skillStrat = Env.IA_Strategy_InvestInPopularity;
                break;
            case SkillProcessState.NoSpecialisation:
            default:
                break;
        }

        return $"Le concurrent est entrain de {eventStrat}{skillStrat}.";
    }

    private void HandleChangeOfEventState()
    {
        // only offer a chance to switch state each 5 years
        if (yearPassed % 5 != 0)
            return;

        switch (eventStateMachine.CurrentState)
        {
            case EventProcessState.MaxMoney:
                ProcessMaxMoneyState();
                break;
            case EventProcessState.MaxConsumers:
                ProcessMaxConsumersState();
                break;
            case EventProcessState.MinCosts:
            default:
                ProcessMinCostsState();
                break;
        }
    }

    private void HandleChangeOfSkillState(CompanyEntity iaCompany)
    {
        switch (skillStateMachine.CurrentState)
        {
            case SkillProcessState.NoSpecialisation:
                System.Random random = new System.Random();
                if (random.Next(10) < 2) // 20% chance
                    ChooseSkillStrategy();
                break;
            // IA stays in investement strategy once it has specialised
            case SkillProcessState.InvestInManufacturing:
            case SkillProcessState.InvestInAds:
            case SkillProcessState.InvestInPopularity:
            default:
                break;
        }
        HandleSkillTreeDeveloppment(iaCompany);
    }

    private void ChooseSkillStrategy()
    {
        System.Random random = new System.Random();
        int randChoice = random.Next(3);

        // it will choose at random one stategy that is not the first one choosen by the player
        switch (randChoice)
        {
            case 0:
                skillStateMachine.MoveNext(SkillCommand.SpecialiseInAds);
                break;
            case 1:
                skillStateMachine.MoveNext(SkillCommand.SpecialiseInPopularity);
                break;
            case 2:
                skillStateMachine.MoveNext(SkillCommand.SpecialiseInManufacturing);
                break;
            default:
                break;
        }
    }

    private void HandleWorldEvent(CompanyEntity iaCompany, WorldEvent worldEvent)
    {
        switch (eventStateMachine.CurrentState)
        {
            case EventProcessState.MaxConsumers:
                MaximizeConsumersStrategy(iaCompany, worldEvent);
                break;
            case EventProcessState.MaxMoney:
                MaximizeMoneyStrategy(iaCompany, worldEvent);
                break;
            case EventProcessState.MinCosts:
            default:
                MinimizeCostsStrategy(iaCompany, worldEvent);
                break;
        }
    }

    private void HandleSkillTreeDeveloppment(CompanyEntity iaCompany)
    {
        switch (skillStateMachine.CurrentState)
        {
            case SkillProcessState.InvestInManufacturing:
                virtualSkillTreeManager.HandleIASkillTree(Building.TYPE.MANUFACTURING, iaCompany, yearPassed);
                break;
            case SkillProcessState.InvestInAds:
                virtualSkillTreeManager.HandleIASkillTree(Building.TYPE.PUBLICITY, iaCompany, yearPassed);
                break;
            case SkillProcessState.InvestInPopularity:
                virtualSkillTreeManager.HandleIASkillTree(Building.TYPE.POPULARITY, iaCompany, yearPassed);
                break;
            case SkillProcessState.NoSpecialisation:
            default:
                break;
        }
    }

    private void MaximizeConsumersStrategy(CompanyEntity iaCompany, WorldEvent worldEvent)
    {
        WorldEventImpact[] interestedEvents = new WorldEventImpact[] {
            WorldEventImpact.Consumers,
            WorldEventImpact.NewConsumers
        };

        worldEvent.HandleEventBasedOnInterests(iaCompany, interestedEvents);
    }

    private void MinimizeCostsStrategy(CompanyEntity iaCompany, WorldEvent worldEvent)
    {
        WorldEventImpact[] interestedEvents = new WorldEventImpact[] {
            WorldEventImpact.ManufacturingCosts,
            WorldEventImpact.LobbyingCosts,
            WorldEventImpact.AdCampaignsCosts,
            WorldEventImpact.CigarettePackPrice
        };

        worldEvent.HandleEventBasedOnInterests(iaCompany, interestedEvents);
    }

    private void MaximizeMoneyStrategy(CompanyEntity iaCompany, WorldEvent worldEvent)
    {
        WorldEventImpact[] interestedEvents = new WorldEventImpact[] {
            WorldEventImpact.Money,
            WorldEventImpact.BonusMoney
        };

        worldEvent.HandleEventBasedOnInterests(iaCompany, interestedEvents);
    }

    private void ProcessMaxConsumersState()
    {
        float lastConsumers = GetLastConsumers();

        // Maximizing consumers is working, we switch startegy
        if (lastConsumers < startConsummersChangeEventState)
        {
            eventStateMachine.MoveNext(Get50PercentChance() ?
                EventCommand.GoMaxMoney : EventCommand.GoMinCosts);
        }

        UpdateStartingValuesChangeEventState();
    }

    private void ProcessMaxMoneyState()
    {
        float lastMoney = GetLastMoney();

        // Maximizing money is working, we switch startegy
        if (lastMoney < startMoneyChangeEventState)
        {
            eventStateMachine.MoveNext(Get50PercentChance() ?
                EventCommand.GoMaxConsumers : EventCommand.GoMinCosts);
        }

        UpdateStartingValuesChangeEventState();
    }

    private void ProcessMinCostsState()
    {
        float lastCosts = GetLastCosts();

        // Minimizing costs is working, we switch startegy
        if (lastCosts < startCostsChangeEventState)
        {
            eventStateMachine.MoveNext(Get50PercentChance() ?
                EventCommand.GoMaxConsumers : EventCommand.GoMaxMoney);
        }

        UpdateStartingValuesChangeEventState();
    }

    private bool Get50PercentChance()
    {
        System.Random random = new System.Random();
        int randChoice = random.Next(2);

        return randChoice < 1;
    }

    private void UpdateStartingValuesChangeEventState()
    {
        startConsummersChangeEventState = GetLastConsumers();
        startMoneyChangeEventState = GetLastMoney();
        startCostsChangeEventState = GetLastCosts();
    }

    private float GetLastMoney()
    {
        // No need to be precise, the diff of first year can be ignored
        float last =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].money;

        return last;
    }

    private float GetLastConsumers()
    {
        // No need to be precise, the diff of first year can be ignored
        float last =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].numConsumers;

        return last;
    }

    private float GetLastCosts()
    {
        // No need to be precise, the diff of first year can be ignored
        float lastManufacturing =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].manufacturingCosts;

        float lastLobbying =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].lobbyingCosts;

        float lastAdCampaigns =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].adCampaignsCosts;

        float last = lastManufacturing + lastLobbying + lastAdCampaigns;

        return last;
    }

    public void ResetIAManager()
    {
        virtualSkillTreeManager.ResetAllVirtualTrees();
    }
}