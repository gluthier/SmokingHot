using System.Collections.Generic;

using static AdCampaignEntity;
using static CigarettePackEntity;
using static SimulationManager;

public class ConglomerateEntity
{
    public string conglomerateName;
    public float totalMoney;
    public float newSmokerAcquisition;
    public float smokerRetention;
    private float productionCostPercentage;
    private float distributionCostPercentage;

    public string continentName;
    private int cigarettesPerPack;
    private float cigarettePackPrice;
    public float population;
    private float populationGrowth;
    public float smokerPercentage;
    public float deathSmokerPercentage;
    private float cigarettePerDay;

    private Dictionary<AgeBracket, float> populationDistribution;
    public List<AdCampaignEntity> adCampaigns;
    public CigarettePackEntity cigarettePackProduced;
    public Dictionary<AgeBracket, PopularityLevel> popularityByAgeBracket;

    public ConglomerateEntity(ConglomerateData conglomerateData)
    {
        cigarettePackProduced = new CigarettePackEntity();
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

    public void SetConglomerateName(string conglomerateName)
    {
        this.conglomerateName = conglomerateName;
    }

    public void SpendMoney(int amountMillion)
    {
        totalMoney -= amountMillion;
    }

    public void EndFiscalYear()
    {
        EndAdCampaignFiscalYear();
        EndConglomerateFiscalYear();

        UpdateSmokerStatistics();
        UpdatePopulation();
    }

    private void LoadData(ConglomerateData conglomerateData)
    {
        conglomerateName = conglomerateData.conglomerateName;
        totalMoney = conglomerateData.startingMoneyMillion;
        newSmokerAcquisition = conglomerateData.newSmokerAcquisition;
        smokerRetention = conglomerateData.smokerRetention;
        productionCostPercentage = conglomerateData.productionCostPercentage / 100f;
        distributionCostPercentage = conglomerateData.distributionCostPercentage / 100f;

        continentName = conglomerateData.continentName;
        cigarettesPerPack = conglomerateData.cigarettesPerPack;
        cigarettePackPrice = conglomerateData.cigarettePackPrice;
        population = conglomerateData.populationMillion;
        populationGrowth = conglomerateData.populationGrowth / 1000f;
        smokerPercentage = conglomerateData.smokerPercentage / 100f;
        deathSmokerPercentage = conglomerateData.deathSmokerPercentage / 100f;
        cigarettePerDay = conglomerateData.cigarettePerDay;

        populationDistribution = new Dictionary<AgeBracket, float>
        {
            { AgeBracket.Kid, conglomerateData.populationDistribution.kid },
            { AgeBracket.Teenager, conglomerateData.populationDistribution.teenager },
            { AgeBracket.YoungAdult, conglomerateData.populationDistribution.youngadult },
            { AgeBracket.Adult, conglomerateData.populationDistribution.adult },
            { AgeBracket.Senior, conglomerateData.populationDistribution.senior }
        };
    }

    private void UpdateSmokerStatistics()
    {
        float nonSmokerPercentage = 1 - smokerPercentage;

        int nonSmokersPopulation = (int)(population * nonSmokerPercentage);
        int smokersPopulation = (int)(population * smokerPercentage);

        float addictionPercentage = (int)cigarettePackProduced.addiction / (float)AddictionLevel.Average;

        int newSmokers = (int)(nonSmokersPopulation * newSmokerAcquisition);
        int smokersKept = (int)(smokersPopulation * smokerRetention * addictionPercentage);

        int newTotalSmokers = newSmokers + smokersKept;

        smokerPercentage = (float)newTotalSmokers / population;
    }

    private void UpdatePopulation()
    {
        float toxicityPercentage = (int)cigarettePackProduced.toxicity / (float)ToxicityLevel.Average;

        int smokersPopulation = (int)(population * smokerPercentage);
        int numDeathFromSmoking = (int)(deathSmokerPercentage * smokersPopulation * toxicityPercentage);

        population = (int)(population * (1 + populationGrowth));
        population -= numDeathFromSmoking;

        if (population < 0) {
            population = 0;
        }
    }

    private void EndAdCampaignFiscalYear()
    {
        List<AdCampaignEntity> toRemove = new List<AdCampaignEntity>();

        // Get ad campaigns results
        foreach (AdCampaignEntity adCampaign in adCampaigns)
        {
            float adCampaignCost = adCampaign.EndFiscalYear();

            totalMoney -= adCampaignCost;
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

    private void EndConglomerateFiscalYear()
    {
        int smokersPopulation = (int)(population * smokerPercentage);

        float cigarettePackSoldPerSmoker = Env.DaysInAYear * cigarettePerDay / cigarettesPerPack;
        float totalCigarettePackMoney = smokersPopulation * cigarettePackSoldPerSmoker * cigarettePackPrice;

        float expensesPercentage = productionCostPercentage + distributionCostPercentage;
        float benefitPercentage = 1 - expensesPercentage;

        float expensesMoney = totalCigarettePackMoney * expensesPercentage;
        float benefitMoney = totalCigarettePackMoney * benefitPercentage;

        float gain = benefitMoney - expensesMoney;

        totalMoney += gain;
    }
}
