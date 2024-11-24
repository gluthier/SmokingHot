using System.Collections.Generic;
using Unity.Mathematics;
using static CigarettePackEntity;
using static SimulationManager;

public class CompanyEntity
{
    public bool isPlayer;

    // Shown simulation data
    public string companyName;
    public float money;
    public PopularityLevel popularity;
    public float consumers;
    public float manufacturing;
    public float lobbying;
    public float adCampaigns;
    public CigarettePackEntity cigarettePackProduced;

    // Hidden simulation data
    private float cigarettePackPrice;
    private float deathSmokerPercentage;
    private float newSmokerAcquisition;
    private float smokerRetention;
    private float returnOnInvestment;

    public CompanyEntity(CompanyData conglomerateData, bool isPlayer)
    {
        this.isPlayer = isPlayer;

        cigarettePackProduced = new CigarettePackEntity();
        popularity = PopularityLevel.Neutral;

        LoadData(conglomerateData);
    }

    public void SetCompanyName(string companyName)
    {
        this.companyName = companyName;
    }

    public void SpendMoney(float amount)
    {
        money -= amount;
    }

    public void ImpactReputation(int level)
    {
        int newPopularityLevel = (int)popularity + level;
        newPopularityLevel = 
            math.clamp(newPopularityLevel, (int)PopularityLevel.Hated, (int)PopularityLevel.Loved);

        popularity = (PopularityLevel)newPopularityLevel;
    }

    public float EndFiscalYear()
    {
        float moneyGained = EndConglomerateFiscalYear();
        UpdateConsumersStats();

        return moneyGained;
    }

    private void LoadData(CompanyData companyData)
    {
        companyName = companyData.companyName;
        money = companyData.startingMoneyMillion;
        consumers = companyData.startingConsumersMillion;
        manufacturing = companyData.startingManufacturingMillion;
        lobbying = companyData.startingLobbyingMillion;
        adCampaigns = companyData.startingAdCampaignsMillion;

        cigarettePackPrice = companyData.cigarettePackPrice;
        deathSmokerPercentage = companyData.deathSmokerPercentage;
        newSmokerAcquisition = companyData.newSmokerAcquisition;
        smokerRetention = companyData.smokerRetention;
        returnOnInvestment = companyData.returnOnInvestment;
    }

    private float EndConglomerateFiscalYear()
    {
        int cigarettePackSoldPerSmoker = 180; // ~= 10 cigarettes per day
        float cigarettePackPriceMillion = cigarettePackPrice / 1000000f;
        float totalCigarettePackMoneyMillion = consumers * cigarettePackSoldPerSmoker * cigarettePackPriceMillion;

        float returnOnInvestmentsMillion = money * returnOnInvestment;

        float moneyGained = totalCigarettePackMoneyMillion + returnOnInvestmentsMillion -
            manufacturing - lobbying - adCampaigns;

        money += moneyGained;
        return moneyGained;
    }

    private void UpdateConsumersStats()
    {
        float toxicityPercentage = (int)cigarettePackProduced.toxicity / (float)ToxicityLevel.Average;

        float consumersDeadFromSmoking = deathSmokerPercentage/100f * consumers * toxicityPercentage;
        consumers -= consumersDeadFromSmoking;

        if (consumers < 0) {
            consumers = 0;
        }

        float newConsumers = consumers * newSmokerAcquisition;
        float lostConsumers = consumers * (1 - smokerRetention);

        consumers += newConsumers - lostConsumers;
    }
}
