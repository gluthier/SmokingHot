using System.Collections.Generic;
using System;

public enum EventProcessState
{
    MaxMoney,
    MaxConsumers,
    MinCosts
}

public enum EventCommand
{
    GoMaxMoney,
    GoMaxConsumers,
    GoMinCosts
}

// Source: https://stackoverflow.com/a/5924053
public class EventStateMachine
{
    class EventStateTransition
    {
        readonly EventProcessState CurrentState;
        readonly EventCommand Command;

        public EventStateTransition(EventProcessState currentState, EventCommand command)
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
            EventStateTransition other = obj as EventStateTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
        }
    }

    Dictionary<EventStateTransition, EventProcessState> transitions;
    public EventProcessState CurrentState { get; private set; }

    public EventStateMachine()
    {
        CurrentState = EventProcessState.MinCosts;
        transitions = new Dictionary<EventStateTransition, EventProcessState>
            {
                { new EventStateTransition(EventProcessState.MaxMoney, EventCommand.GoMaxConsumers),
                                      EventProcessState.MaxConsumers },

                { new EventStateTransition(EventProcessState.MaxMoney, EventCommand.GoMinCosts),
                                      EventProcessState.MinCosts },

                { new EventStateTransition(EventProcessState.MaxConsumers, EventCommand.GoMaxMoney),
                                      EventProcessState.MaxMoney },

                { new EventStateTransition(EventProcessState.MaxConsumers, EventCommand.GoMinCosts),
                                      EventProcessState.MinCosts },

                { new EventStateTransition(EventProcessState.MinCosts, EventCommand.GoMaxConsumers),
                                      EventProcessState.MaxConsumers },

                { new EventStateTransition(EventProcessState.MinCosts, EventCommand.GoMaxMoney),
                                      EventProcessState.MaxMoney }
            };
    }

    public EventProcessState GetNext(EventCommand command)
    {
        EventStateTransition transition = new EventStateTransition(CurrentState, command);
        EventProcessState nextState;
        if (!transitions.TryGetValue(transition, out nextState))
            throw new Exception("Invalid transition: " + CurrentState + " -> " + command);
        return nextState;
    }

    public EventProcessState MoveNext(EventCommand command)
    {
        CurrentState = GetNext(command);
        return CurrentState;
    }
}