using System.Collections.Generic;

using static AdCampaignEntity;
using static CigarettePackEntity;
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
    private float populationGrowth;
    private float smokerPercentage;
    private float deathSmokerPercentage;
    private float cigarettePerDay;

    private Dictionary<AgeBracket, float> populationDistribution;
    private List<AdCampaignEntity> adCampaigns;
    private CigarettePackEntity cigarettePackProduced;
    private Dictionary<AgeBracket, PopularityLevel> popularityByAgeBracket;

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
        totalMoney = conglomerateData.startingMoney;
        newSmokerAcquisition = conglomerateData.newSmokerAcquisition;
        smokerRetention = conglomerateData.smokerRetention;
        productionCostPercentage = conglomerateData.productionCostPercentage / 100f;
        distributionCostPercentage = conglomerateData.distributionCostPercentage / 100f;

        continentName = conglomerateData.continentName;
        cigarettesPerPack = conglomerateData.cigarettesPerPack;
        cigarettePackPrice = conglomerateData.cigarettePackPrice;
        population = conglomerateData.population;
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
        string debug_msg = $"UpdateSmokerStatistics  {continentName}\n=====\n";

        float nonSmokerPercentage = 1 - smokerPercentage;

        long nonSmokersPopulation = (long)(population * nonSmokerPercentage);
        long smokersPopulation = (long)(population * smokerPercentage);

        float addictionPercentage = (int)cigarettePackProduced.addiction / (float)AddictionLevel.Average;

        long newSmokers = (long)(nonSmokersPopulation * newSmokerAcquisition);
        long smokersKept = (long)(smokersPopulation * smokerRetention * addictionPercentage);

        long newTotalSmokers = newSmokers + smokersKept;

        debug_msg += $"  nonSmokerPercentage: {nonSmokerPercentage}\n" +
            $"  nonSmokersPopulation: {nonSmokersPopulation}\n" +
            $"  smokersPopulation: {smokersPopulation}\n" +
            $"  addictionPercentage: {addictionPercentage}\n" +
            $"  newSmokers: {newSmokers}\n" +
            $"  smokersKept: {smokersKept}\n" +
            $"  newTotalSmokers: {newTotalSmokers}" +
            $"  smokerPercentage before : {smokerPercentage}";

        smokerPercentage = (float)newTotalSmokers / population;

        debug_msg += $"  smokerPercentage after : {smokerPercentage}";
        Env.PrintDebug(debug_msg);
    }

    private void UpdatePopulation()
    {
        string debug_msg = $"UpdatePopulation  {continentName}\n=====\n";

        float toxicityPercentage = (int)cigarettePackProduced.toxicity / (float)ToxicityLevel.Average;

        long smokersPopulation = (long)(population * smokerPercentage);
        long numDeathFromSmoking = (long)(deathSmokerPercentage * smokersPopulation * toxicityPercentage);

        debug_msg += $"  populationGrowthPercentage: {populationGrowth}\n" +
            $"  toxicityPercentage: {toxicityPercentage}\n" +
            $"  smokersPopulation: {smokersPopulation}\n" +
            $"  numDeathFromSmoking: {numDeathFromSmoking}\n" +
            $"  population before: {population}\n";

        population = (long)(population * (1 + populationGrowth));
        population -= numDeathFromSmoking;

        if (population < 0) {
            population = 0;
        }

        debug_msg += $"  population after: {population}\n";
        Env.PrintDebug(debug_msg);
    }

    private void EndAdCampaignFiscalYear()
    {
        string debug_msg = $"EndAdCampaignFiscalYear  {continentName}\n=====\n";

        List<AdCampaignEntity> toRemove = new List<AdCampaignEntity>();

        // Get ad campaigns results
        foreach (AdCampaignEntity adCampaign in adCampaigns)
        {
            float adCampaignCost = adCampaign.EndFiscalYear();

            debug_msg += $"  adCampaignCost: {adCampaignCost}\n";

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

        Env.PrintDebug(debug_msg);
    }

    private void HandleAdCampaignResult(AdCampaignEntity adCampaign)
    {
        string debug_msg = $"HandleAdCampaignResult  {continentName}\n=====\n";

        (AdType, AdQualityReception) result = adCampaign.GetResult();

        debug_msg += $"  newSmokerAcquisition before: {newSmokerAcquisition}\n" +
            $"  smokerRetention before: {smokerRetention}\n";

        newSmokerAcquisition += result.Item1 == AdType.NewSmokerAcquisition ?
            (int)result.Item2 * Env.NewSmokerAcquisitionIncrement : 0;

        smokerRetention += result.Item1 == AdType.SmokerRetention ?
            (int)result.Item2 * Env.SmokerRetentioIncrement : 0;

        debug_msg += $"  newSmokerAcquisition after: {newSmokerAcquisition}\n" +
            $"  smokerRetention after: {smokerRetention}\n";

        Env.PrintDebug(debug_msg);
    }

    private void EndConglomerateFiscalYear()
    {
        string debug_msg = $"EndConglomerateFiscalYear  {continentName}\n=====\n";

        long smokersPopulation = (long)(population * smokerPercentage);

        float cigarettePackSoldPerSmoker = Env.DaysInAYear * cigarettePerDay / cigarettesPerPack;
        float totalCigarettePackMoney = smokersPopulation * cigarettePackSoldPerSmoker * cigarettePackPrice;

        float expensesPercentage = productionCostPercentage + distributionCostPercentage;
        float benefitPercentage = 1 - expensesPercentage;

        float expensesMoney = totalCigarettePackMoney * expensesPercentage;
        float benefitMoney = totalCigarettePackMoney * benefitPercentage;

        float gain = benefitMoney - expensesMoney;

        debug_msg += $"  totalMoney before: {totalMoney}\n" +
            $"  cigarettePackSold: {cigarettePackSoldPerSmoker}\n" +
            $"  totalCigarettePackMoney: {totalCigarettePackMoney}\n" +
            $"  expensesPercentage: {expensesPercentage}\n" +
            $"  benefitPercentage: {benefitPercentage}\n" +
            $"  expensesMoney: {expensesMoney}\n" +
            $"  benefitMoney: {benefitMoney}\n" +
            $"  gain: {gain}\n";

        totalMoney += gain;

        debug_msg += $"  totalMoney after: {totalMoney}\n";

        Env.PrintDebug(debug_msg);
    }
}
