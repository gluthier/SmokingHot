
public class CigarettePackEntity
{
    public enum ToxicityLevel
    {
        VeryBad,
        Bad,
        Average
    }

    public enum AddictionLevel
    {
        VeryAddictive,
        Addictive,
        Average
    }

    private ToxicityLevel cigaretteToxicity;
    private AddictionLevel cigaretteAddiction;
    private float productionCost;
    private float distributionCost;

    public CigarettePackEntity(float cigarettePackPrice)
    {
        cigaretteToxicity = ToxicityLevel.Average;
        cigaretteAddiction = AddictionLevel.Average;
        productionCost = Env.ProductionCostPercentage * cigarettePackPrice;
        distributionCost = Env.DistributionCostPercentage * cigarettePackPrice;
    }
}
