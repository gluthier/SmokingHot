using System.Collections.Generic;
using static SimulationManager;

public class CompanyEntity
{
    public enum PopularityLevel
    {
        Hated,
        Disliked,
        Neutral,
        Appreciated,
        Loved
    }

    private double totalMoney;
    public float marketSharePercentage;
    public float newSmokerAcquisition;
    public float smokerRetention;
    private List<AdCampaignEntity> adCampaigns;
    private CigarettePackEntity cigarettePackProduced;
    private Dictionary<AgeBracket, PopularityLevel> popularityByAgeBracket;

    public CompanyEntity(float marketShare, ContinentData continentData)
    {
        float moneyGainedPerCigarettePack = continentData.cigarettePackPrice * 
            (1 - Env.ProductionCostPercentage - Env.DistributionCostPercentage);

        double totalNumSmokers = continentData.population * continentData.smokerPercentage;
        double totalNumPacksSmoked = totalNumSmokers * continentData.cigarettePerDay / Env.CigarettesPerPack;

        double companySharePacksSmoked = totalNumPacksSmoked * marketShare;
        double startingMoney = Env.StartingMoneyAdjustement * companySharePacksSmoked * moneyGainedPerCigarettePack;

        totalMoney = startingMoney;
        marketSharePercentage = marketShare;
        newSmokerAcquisition = Env.NewSmokerAcquisition;
        smokerRetention = Env.SmokerRetention;
        adCampaigns = new List<AdCampaignEntity>();
        cigarettePackProduced = new CigarettePackEntity(continentData.cigarettePackPrice);
        popularityByAgeBracket = new Dictionary<AgeBracket, PopularityLevel>
        {
            { AgeBracket.Kid, PopularityLevel.Neutral },
            { AgeBracket.Teenager, PopularityLevel.Appreciated },
            { AgeBracket.YoungAdult, PopularityLevel.Neutral },
            { AgeBracket.Adult, PopularityLevel.Disliked },
            { AgeBracket.Senior, PopularityLevel.Neutral }
        };
    }
}
