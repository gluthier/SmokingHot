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
    public float numConsumers;
    public float manufacturingCosts;
    public float lobbyingCosts;
    public float adCampaignsCosts;
    public CigarettePackEntity cigarettePackProduced;

    // Hidden simulation data
    public float cigarettePackPrice;
    public float deathSmokerPercentage;
    public float newSmokerAcquisition;
    public float smokerRetention;
    public float returnOnInvestment;

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

    public void AbolishAds()
    {
        adCampaignsCosts = 0;
        ImpactReputation(-1);
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
        numConsumers = companyData.startingConsumersMillion;
        manufacturingCosts = companyData.startingManufacturingMillion;
        lobbyingCosts = companyData.startingLobbyingMillion;
        adCampaignsCosts = companyData.startingAdCampaignsMillion;

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
        float totalCigarettePackMoneyMillion = numConsumers * cigarettePackSoldPerSmoker * cigarettePackPriceMillion;

        float returnOnInvestmentsMillion = money * returnOnInvestment;

        float moneyGained = totalCigarettePackMoneyMillion + returnOnInvestmentsMillion -
            manufacturingCosts - lobbyingCosts - adCampaignsCosts;

        money += moneyGained;
        return moneyGained;
    }

    private void UpdateConsumersStats()
    {
        float toxicityRatio =
            cigarettePackProduced.GetToxicityRatio();

        float addictionRatio =
            cigarettePackProduced.GetAddictionRatio();

        float consumersDeadFromSmoking = deathSmokerPercentage/100f * numConsumers * toxicityRatio;
        numConsumers -= consumersDeadFromSmoking;

        if (numConsumers < 0) {
            numConsumers = 0;
        }

        newSmokerAcquisition *= addictionRatio;
        float newConsumers = numConsumers * newSmokerAcquisition;
        float lostConsumers = numConsumers * (1 - smokerRetention);

        numConsumers += newConsumers - lostConsumers;
    }
}
