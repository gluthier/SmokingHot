using UnityEngine;

[CreateAssetMenu(fileName = "WorldEvent", menuName = "WorldEventSO", order = 1)]
public class WorldEventSO : ScriptableObject
{
    public enum ImpactFunction
    {
        GainMoney,
        LooseMoney,

        GainReputationKids,
        GainReputationTeenagers,
        GainReputationYoungAdults,
        GainReputationAdults,
        GainReputationSeniors,

        LooseReputationKids,
        LooseReputationTeenagers,
        LooseReputationYoungAdults,
        LooseReputationAdults,
        LooseReputationSeniors,

        BlockAdvertisment
    }

    public string title;
    public string description;

    public ImpactFunction acceptFunction;
    public float acceptCost;
    public ImpactFunction refuseFunction;
    public float refuseCost;

    public void AcceptEvent(GameManager gameManager)
    {
        HandleEvent(gameManager, acceptCost);
    }

    public void RefuseEvent(GameManager gameManager)
    {
        HandleEvent(gameManager, refuseCost);
    }

    private void HandleEvent(GameManager gameManager, float cost)
    {
        switch (acceptFunction)
        {
            case ImpactFunction.GainMoney:
                gameManager.SpendMoney(-cost);
                break;

            case ImpactFunction.LooseMoney:
                gameManager.SpendMoney(cost);
                break;

            case ImpactFunction.GainReputationKids:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.Kid, (int)cost);
                break;

            case ImpactFunction.GainReputationTeenagers:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.Teenager, (int)cost);
                break;

            case ImpactFunction.GainReputationYoungAdults:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.YoungAdult, (int)cost);
                break;

            case ImpactFunction.GainReputationAdults:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.Adult, (int)cost);
                break;

            case ImpactFunction.GainReputationSeniors:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.Senior, (int)cost);
                break;

            case ImpactFunction.LooseReputationKids:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.Kid, (int)-cost);
                break;

            case ImpactFunction.LooseReputationTeenagers:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.Teenager, (int)-cost);
                break;

            case ImpactFunction.LooseReputationYoungAdults:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.YoungAdult, (int)-cost);
                break;

            case ImpactFunction.LooseReputationAdults:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.Adult, (int)-cost);
                break;

            case ImpactFunction.LooseReputationSeniors:
                gameManager.ImpactReputation(SimulationManager.AgeBracket.Senior, (int)-cost);
                break;

            case ImpactFunction.BlockAdvertisment:
                gameManager.BlockAdvertisment(cost);
                break;

            default:
                break;
        }
    }
}