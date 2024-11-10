using System.Collections.Generic;
using static AdCampaignEntity;
using static SimulationManager;

public class ConglomerateEntity
{
    private string conglomerateName;
    private double totalMoney;
    private float newSmokerAcquisition;
    private float smokerRetention;
    private float productionCostPercentage;
    private float distributionCostPercentage;

    private string continentName;
    private int cigarettesPerPack;
    private float cigarettePackPrice;
    private long population;
    private float smokerPercentage;
    private float deathSmokerPercentage;
    private float cigarettePerDay;

    private Dictionary<AgeBracket, float> populationDistribution;
    private List<AdCampaignEntity> adCampaigns;
    private CigarettePackEntity cigarettePackProduced;
    private Dictionary<AgeBracket, PopularityLevel> popularityByAgeBracket;


    public ConglomerateEntity(ConglomerateData conglomerateData)
    {
        adCampaigns = new List<AdCampaignEntity>();
        popularityByAgeBracket = new Dictionary<AgeBracket, PopularityLevel>
        {
            { AgeBracket.Kid, PopularityLevel.Neutral },
            { AgeBracket.Teenager, PopularityLevel.Neutral },
            { AgeBracket.YoungAdult, PopularityLevel.Neutral },
            { AgeBracket.Adult, PopularityLevel.Neutral },
            { AgeBracket.Senior, PopularityLevel.Neutral }
        };

        LoadData(conglomerateData);
    }

    public void EndFiscalYear()
    {
        EndAdCampaignFiscalYear();

        ComputeMoney();
        UpdateDeathFromSmoking();
        UpdateSmokerStatistics();
    }

    private void LoadData(ConglomerateData conglomerateData)
    {
        conglomerateName = conglomerateData.conglomerateName;
        totalMoney = conglomerateData.startingMoney;
        newSmokerAcquisition = conglomerateData.newSmokerAcquisition;
        smokerRetention = conglomerateData.smokerRetention;
        productionCostPercentage = conglomerateData.productionCostPercentage;
        distributionCostPercentage = conglomerateData.distributionCostPercentage;

        continentName = conglomerateData.continentName;
        cigarettesPerPack = conglomerateData.cigarettesPerPack;
        cigarettePackPrice = conglomerateData.cigarettePackPrice;
        population = conglomerateData.population;
        smokerPercentage = conglomerateData.smokerPercentage;
        deathSmokerPercentage = conglomerateData.deathSmokerPercentage;
        cigarettePerDay = conglomerateData.cigarettePerDay;

        populationDistribution = new Dictionary<AgeBracket, float>
        {
            { AgeBracket.Kid, conglomerateData.populationDistribution.kid },
            { AgeBracket.Teenager, conglomerateData.populationDistribution.teenager },
            { AgeBracket.YoungAdult, conglomerateData.populationDistribution.youngadult },
            { AgeBracket.Adult, conglomerateData.populationDistribution.adult },
            { AgeBracket.Senior, conglomerateData.populationDistribution.senior }
        };

        cigarettePackProduced = new CigarettePackEntity(conglomerateData);
    }

    private void ComputeMoney()
    {
        float cigarettePackSold = Env.DaysInAYear * cigarettePerDay / cigarettesPerPack;
        float totalCigarettePackMoney = cigarettePackSold * cigarettePackPrice;

        float expensesPercentage = productionCostPercentage + distributionCostPercentage;
        float benefitPercentage = 1 - expensesPercentage;

        float expensesMoney = totalCigarettePackMoney * expensesPercentage;
        float benefitMoney = totalCigarettePackMoney * benefitPercentage;

        totalMoney += benefitMoney - expensesMoney;
    }

    private void UpdateSmokerStatistics()
    {
        float nonSmokerPercentage = 1 - smokerPercentage;

        long nonSmokersPopulation = (long)(population * nonSmokerPercentage);
        long smokersPopulation = (long)(population * smokerPercentage);

        long newSmokers = (long)(nonSmokersPopulation * newSmokerAcquisition);
        long smokersKept = (long)(smokersPopulation * smokerRetention);

        long newTotalSmokers = newSmokers + smokersKept;
        smokerPercentage = (float)newTotalSmokers / population;
    }

    private void UpdateDeathFromSmoking()
    {
        long smokersPopulation = (long)(population * smokerPercentage);
        long numDeathFromSmoking = (long)(deathSmokerPercentage * smokersPopulation);

        population -= numDeathFromSmoking;
    }

    private void EndAdCampaignFiscalYear()
    {
        List<AdCampaignEntity> toRemove = new List<AdCampaignEntity>();

        // Get ad campaigns results
        foreach (AdCampaignEntity adCampaign in adCampaigns)
        {
            totalMoney -= adCampaign.EndFiscalYear();
            HandleAdCampaignResult(adCampaign);

            if (adCampaign.GetDurationRemaining() <= 0)
            {
                toRemove.Add(adCampaign);
            }
        }

        // Remove finished ad campaigns
        foreach (AdCampaignEntity adCampaign in toRemove)
        {
            adCampaigns.Remove(adCampaign);
        }
    }

    private void HandleAdCampaignResult(AdCampaignEntity adCampaign)
    {
        (AdType, AdQualityReception) result = adCampaign.GetResult();

        newSmokerAcquisition += result.Item1 == AdType.NewSmokerAcquisition ?
            (int)result.Item2 * Env.NewSmokerAcquisitionIncrement : 0;

        smokerRetention += result.Item1 == AdType.SmokerRetention ?
            (int)result.Item2 * Env.SmokerRetentioIncrement : 0;
    }
    }
}
