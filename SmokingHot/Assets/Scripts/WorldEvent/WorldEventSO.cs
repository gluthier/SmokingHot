using UnityEngine;

[CreateAssetMenu(fileName = "WorldEvent", menuName = "WorldEventSO", order = 1)]
public class WorldEventSO : ScriptableObject
{
    public enum ImpactFunction
    {
        GainMoney,
        LooseMoney,

        GainReputation,
        LooseReputation,
    }

    public string title;
    public string description;

    public ImpactFunction acceptFunction;
    public int acceptCost;
    public ImpactFunction refuseFunction;
    public int refuseCost;

    public void AcceptEvent(GameManager gameManager)
    {
        HandleEvent(gameManager, acceptCost);
    }

    public void RefuseEvent(GameManager gameManager)
    {
        HandleEvent(gameManager, refuseCost);
    }

    private void HandleEvent(GameManager gameManager, int cost)
    {
        switch (acceptFunction)
        {
            case ImpactFunction.GainMoney:
                gameManager.SpendMoney(-cost);
                break;

            case ImpactFunction.LooseMoney:
                gameManager.SpendMoney(cost);
                break;

            case ImpactFunction.GainReputation:
                gameManager.ImpactReputation(cost);
                break;

            case ImpactFunction.LooseReputation:
                gameManager.ImpactReputation(-cost);
                break;

            default:
                break;
        }
    }
}