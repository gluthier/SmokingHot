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

    private List<GameState> iaStateReports;

    public void Init()
    {
        eventStateMachine = new EventStateMachine();
        skillStateMachine = new SkillStateMachine();

        iaStateReports = new List<GameState>();
    }

    public void ProcessEndOfYear(GameState iaStatReport, CompanyEntity iaCompany, WorldEvent worldEvent, List<Building.TYPE> firstPlayerInvestment)
    {
        iaStateReports.Add(iaStatReport);

        HandleChangeOfEventState();
        HandleChangeOfSkillState(firstPlayerInvestment);

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

    private void HandleChangeOfSkillState(List<Building.TYPE> firstPlayerInvestment)
    {
        if (firstPlayerInvestment.Count == 0)
            return;

        switch (skillStateMachine.CurrentState)
        {
            case SkillProcessState.NoSpecialisation:
                ChooseSkillStrategy(firstPlayerInvestment.First());
                HandleSkillTreeDeveloppment();
                break;
            // IA stays in investement strategy once it has specialised
            case SkillProcessState.InvestInManufacturing:
            case SkillProcessState.InvestInAds:
            case SkillProcessState.InvestInPopularity:
            default:
                break;
        }
    }

    private void ChooseSkillStrategy(Building.TYPE buildingTypeFirstInvestmentByPlayer)
    {
        // it will choose at random one stategy that is not the first one choosen by the player
        switch (buildingTypeFirstInvestmentByPlayer)
        {
            case Building.TYPE.CIGARETTE:
                ExecuteAtRandomSkillStrategyInvestment(
                    SkillCommand.SpecialiseInAds, SkillCommand.SpecialiseInPopularity);
                break;
            case Building.TYPE.PUBLICITY:
                ExecuteAtRandomSkillStrategyInvestment(
                    SkillCommand.SpecialiseInManufacturing, SkillCommand.SpecialiseInPopularity);
                break;
            case Building.TYPE.REPUTATION:
                ExecuteAtRandomSkillStrategyInvestment(
                    SkillCommand.SpecialiseInAds, SkillCommand.SpecialiseInManufacturing);
                break;
            case Building.TYPE.LOBBYING:
            default:
                break;
        }
    }

    private void ExecuteAtRandomSkillStrategyInvestment(SkillCommand optionOne, SkillCommand optionTwo)
    {
        System.Random random = new System.Random();

        if (random.Next(2) < 1)
        {
            skillStateMachine.MoveNext(optionOne);
        }
        else
        {
            skillStateMachine.MoveNext(optionTwo);
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

    private void HandleSkillTreeDeveloppment()
    {
        SkillTreeManager skillTreeManager = FindFirstObjectByType<SkillTreeManager>();

        switch (skillStateMachine.CurrentState)
        {
            case SkillProcessState.InvestInManufacturing:
                skillTreeManager.HandleIASkillTree(Building.TYPE.CIGARETTE);
                break;
            case SkillProcessState.InvestInAds:
                skillTreeManager.HandleIASkillTree(Building.TYPE.PUBLICITY);
                break;
            case SkillProcessState.InvestInPopularity:
                skillTreeManager.HandleIASkillTree(Building.TYPE.REPUTATION);
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
        float diffConsumers = GetDiffConsumers();
        float diffMoney = GetDiffMoney();
        float diffCosts = GetDiffCosts();

        if (diffConsumers >= 0)
        {
            // Maximizing consumers is working, we switch startegy
            if (diffMoney < 0)
            {
                eventStateMachine.MoveNext(EventCommand.GoMaxMoney);
            }
            else if (diffCosts < 0)
            {
                eventStateMachine.MoveNext(EventCommand.GoMinCosts);
            }
        }
    }

    private void ProcessMaxMoneyState()
    {
        float diffMoney = GetDiffMoney();
        float diffConsumers = GetDiffConsumers();
        float diffCosts = GetDiffCosts();

        if (diffMoney >= 0)
        {
            // Maximizing money is working, we switch startegy
            if (diffConsumers < 0)
            {
                eventStateMachine.MoveNext(EventCommand.GoMaxConsumers);
            }
            else if (diffCosts < 0)
            {
                eventStateMachine.MoveNext(EventCommand.GoMinCosts);
            }
        }
    }

    private void ProcessMinCostsState()
    {
        float diffCosts = GetDiffCosts();
        float diffMoney = GetDiffMoney();
        float diffConsumers = GetDiffConsumers();

        if (diffCosts >= 0)
        {
            // Minimizing costs is working, we switch startegy
            if (diffMoney < 0)
            {
                eventStateMachine.MoveNext(EventCommand.GoMaxMoney);
            }
            else if (diffConsumers < 0)
            {
                eventStateMachine.MoveNext(EventCommand.GoMaxConsumers);
            }
        }
    }

    private float GetDiffMoney()
    {
        // No need to be precise, the diff of first two years can be ignored
        float last =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].money;

        float lastlast =
            iaStateReports.Count < 2 ? 0 : iaStateReports[^2].money;

        return lastlast - last;
    }

    private float GetDiffConsumers()
    {
        // No need to be precise, the diff of first two years can be ignored
        float last =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].numConsumers;

        float lastlast =
            iaStateReports.Count < 2 ? 0 : iaStateReports[^2].numConsumers;

        return lastlast - last;
    }

    private float GetDiffCosts()
    {
        // No need to be precise, the diff of first two years can be ignored
        float lastManufacturing =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].manufacturingCosts;
        float lastlastManufacturing =
            iaStateReports.Count < 2 ? 0 : iaStateReports[^2].manufacturingCosts;

        float lastLobbying =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].lobbyingCosts;
        float lastlastLobbying =
            iaStateReports.Count < 2 ? 0 : iaStateReports[^2].lobbyingCosts;

        float lastAdCampaigns =
            iaStateReports.Count < 1 ? 0 : iaStateReports[^1].adCampaignsCosts;
        float lastlastAdCampaigns =
            iaStateReports.Count < 2 ? 0 : iaStateReports[^2].adCampaignsCosts;

        float last = lastManufacturing + lastLobbying + lastAdCampaigns;
        float lastlast = lastlastManufacturing + lastlastLobbying + lastlastAdCampaigns;

        return lastlast - last;
    }
}