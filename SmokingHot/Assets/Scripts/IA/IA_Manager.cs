
using System.Collections.Generic;
using System;
using UnityEngine;
using static GameManager;
using System.Linq;
using static WorldEvent;

// Source: https://stackoverflow.com/a/5924053
public class IA_Manager : MonoBehaviour
{
    public enum ProcessState
    {
        MaxMoney,
        MaxConsumers,
        MinCosts
    }

    public enum Command
    {
        GoMaxMoney,
        GoMaxConsumers,
        GoMinCosts
    }

    public class StateMachine
    {
        class StateTransition
        {
            readonly ProcessState CurrentState;
            readonly Command Command;

            public StateTransition(ProcessState currentState, Command command)
            {
                CurrentState = currentState;
                Command = command;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
            }
        }

        Dictionary<StateTransition, ProcessState> transitions;
        public ProcessState CurrentState { get; private set; }

        public StateMachine()
        {
            CurrentState = ProcessState.MaxMoney;
            transitions = new Dictionary<StateTransition, ProcessState>
            {
                { new StateTransition(ProcessState.MaxMoney, Command.GoMaxConsumers), ProcessState.MaxConsumers },
                { new StateTransition(ProcessState.MaxMoney, Command.GoMinCosts), ProcessState.MinCosts },
                { new StateTransition(ProcessState.MaxConsumers, Command.GoMaxMoney), ProcessState.MaxMoney },
                { new StateTransition(ProcessState.MaxConsumers, Command.GoMinCosts), ProcessState.MinCosts },
                { new StateTransition(ProcessState.MinCosts, Command.GoMaxConsumers), ProcessState.MaxConsumers },
                { new StateTransition(ProcessState.MinCosts, Command.GoMaxMoney), ProcessState.MaxMoney }
            };
        }

        public ProcessState GetNext(Command command)
        {
            StateTransition transition = new StateTransition(CurrentState, command);
            ProcessState nextState;
            if (!transitions.TryGetValue(transition, out nextState))
                throw new Exception("Invalid transition: " + CurrentState + " -> " + command);
            return nextState;
        }

        public ProcessState MoveNext(Command command)
        {
            CurrentState = GetNext(command);
            return CurrentState;
        }
    }

    private StateMachine stateMachine;
    private List<GameState> iaStateReports;
    private CompanyEntity iaCompany;
    private CompanyEntity playerCompany;
    private GameManager gameManager;


    public void Init(CompanyEntity iaCompany, CompanyEntity playerCompany, GameManager gameManager)
    {
        stateMachine = new StateMachine();
        iaStateReports = new List<GameState>();
        this.iaCompany = iaCompany;
        this.playerCompany = playerCompany;
        this.gameManager = gameManager;
    }

    public void ProcessEndOfYear(GameState iaStatReport, CompanyEntity iaCompany, WorldEvent worldEvent)
    {
        iaStateReports.Add(iaStatReport);
        HandleChangeOfState(worldEvent);

        HandleWorldEvent(iaCompany, worldEvent);
    }

    public string GetIAStrategy()
    {
        string strat = "";

        switch (stateMachine.CurrentState)
        {
            case ProcessState.MaxConsumers:
                strat = Env.IA_Strategy_MaxCustommer;
                break;
            case ProcessState.MinCosts:
                strat = Env.IA_Strategy_MinCosts;
                break;
            case ProcessState.MaxMoney:
            default:
                strat = Env.IA_Strategy_MaxMoney;
                break;
        }
        return strat;
    }

    private void HandleChangeOfState(WorldEvent worldEvent)
    {
        switch (stateMachine.CurrentState)
        {
            case ProcessState.MaxConsumers:
                ProcessMaxConsumersState(worldEvent);
                break;
            case ProcessState.MinCosts:
                ProcessMinCostsState(worldEvent);
                break;
            case ProcessState.MaxMoney:
            default:
                ProcessMaxMoneyState(worldEvent);
                break;
        }
    }

    private void HandleWorldEvent(CompanyEntity iaCompany, WorldEvent worldEvent)
    {
        switch (stateMachine.CurrentState)
        {
            case ProcessState.MaxConsumers:
                MaximizeConsumersStrategy(iaCompany, worldEvent);
                break;
            case ProcessState.MinCosts:
                MinimizeCostsStrategy(iaCompany, worldEvent);
                break;
            case ProcessState.MaxMoney:
            default:
                MaximizeMoneyStrategy(iaCompany, worldEvent);
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

    private void ProcessMaxConsumersState(WorldEvent worldEvent)
    {
        float diffConsumers = GetDiffConsumers();
        float diffMoney = GetDiffMoney();
        float diffCosts = GetDiffCosts();

        if (diffConsumers >= 0)
        {
            // Maximizing consumers is working, we switch startegy
            if (diffMoney < 0)
            {
                stateMachine.MoveNext(Command.GoMaxMoney);
            }
            else if (diffCosts < 0)
            {
                stateMachine.MoveNext(Command.GoMinCosts);
            }
        }
    }

    private void ProcessMaxMoneyState(WorldEvent worldEvent)
    {
        float diffMoney = GetDiffMoney();
        float diffConsumers = GetDiffConsumers();
        float diffCosts = GetDiffCosts();

        if (diffMoney >= 0)
        {
            // Maximizing money is working, we switch startegy
            if (diffConsumers < 0)
            {
                stateMachine.MoveNext(Command.GoMaxConsumers);
            }
            else if (diffCosts < 0)
            {
                stateMachine.MoveNext(Command.GoMinCosts);
            }
        }
    }

    private void ProcessMinCostsState(WorldEvent worldEvent)
    {
        float diffCosts = GetDiffCosts();
        float diffMoney = GetDiffMoney();
        float diffConsumers = GetDiffConsumers();

        if (diffCosts >= 0)
        {
            // Minimizing costs is working, we switch startegy
            if (diffMoney < 0)
            {
                stateMachine.MoveNext(Command.GoMaxMoney);
            }
            else if (diffConsumers < 0)
            {
                stateMachine.MoveNext(Command.GoMaxConsumers);
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