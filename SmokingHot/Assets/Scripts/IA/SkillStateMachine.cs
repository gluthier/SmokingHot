using System.Collections.Generic;
using System;

public enum SkillProcessState
{
    NoSpecialisation,
    InvestInManufacturing,
    InvestInAds,
    InvestInPopularity
}

public enum SkillCommand
{
    SpecialiseInManufacturing,
    SpecialiseInAds,
    SpecialiseInPopularity
}

// Source: https://stackoverflow.com/a/5924053
public class SkillStateMachine
{
    class SkillStateTransition
    {
        readonly SkillProcessState CurrentState;
        readonly SkillCommand Command;

        public SkillStateTransition(SkillProcessState currentState, SkillCommand command)
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
            SkillStateTransition other = obj as SkillStateTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
        }
    }

    Dictionary<SkillStateTransition, SkillProcessState> transitions;
    public SkillProcessState CurrentState { get; private set; }

    public SkillStateMachine()
    {
        CurrentState = SkillProcessState.NoSpecialisation;
        transitions = new Dictionary<SkillStateTransition, SkillProcessState>
            {
                { new SkillStateTransition(SkillProcessState.NoSpecialisation, SkillCommand.SpecialiseInManufacturing),
                                      SkillProcessState.InvestInManufacturing },

                { new SkillStateTransition(SkillProcessState.NoSpecialisation, SkillCommand.SpecialiseInAds),
                                      SkillProcessState.InvestInAds },

                { new SkillStateTransition(SkillProcessState.NoSpecialisation, SkillCommand.SpecialiseInPopularity),
                                      SkillProcessState.InvestInPopularity }
            };
    }

    public SkillProcessState GetNext(SkillCommand command)
    {
        SkillStateTransition transition = new SkillStateTransition(CurrentState, command);
        SkillProcessState nextState;
        if (!transitions.TryGetValue(transition, out nextState))
            throw new Exception("Invalid transition: " + CurrentState + " -> " + command);
        return nextState;
    }

    public SkillProcessState MoveNext(SkillCommand command)
    {
        CurrentState = GetNext(command);
        return CurrentState;
    }
}