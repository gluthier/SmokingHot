using System.Collections.Generic;
using Unity.Mathematics;
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
    public float newConsumers;
    public float lostConsumers;
    public float deadConsumers;
    public float bonusMoney;

    public enum Param
    {
        Money,
        BonusMoney,
        cigarettePackPrice,
        NewConsumers,
        LostConsumers,
        DeadConsumers,
        Consumers
    }

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

    public void IncreaseParam(Param param, float amount)
    {
        switch (param)
        {
            case Param.Money:
                money += amount;
                break;
            case Param.BonusMoney:
                bonusMoney += amount;
                break;
            case Param.cigarettePackPrice:
                cigarettePackPrice += amount;
                break;
            case Param.NewConsumers:
                newConsumers += amount;
                break;
            case Param.LostConsumers:
                lostConsumers += amount;
                break;
            case Param.DeadConsumers:
                deadConsumers += amount;
                break;
            case Param.Consumers:
                numConsumers += amount;
                break;
            default:
                break;
        }
    }

    public void DecreaseParam(Param param, float amount)
    {
        IncreaseParam(param, -amount);
    }

    public void MultiplyParam(Param param, float amount)
    {
        switch (param)
        {
            case Param.Money:
                money *= amount;
                break;
            case Param.BonusMoney:
                bonusMoney *= amount;
                break;
            case Param.cigarettePackPrice:
                cigarettePackPrice *= amount;
                break;
            case Param.NewConsumers:
                newConsumers *= amount;
                break;
            case Param.LostConsumers:
                lostConsumers *= amount;
                break;
            case Param.DeadConsumers:
                deadConsumers *= amount;
                break;
            case Param.Consumers:
                numConsumers *= amount;
                break;
            default:
                break;
        }
    }

    public void DivideParam(Param param, float amount)
    {
        if (amount == 0)
            return;

        MultiplyParam(param, 1f / amount);
    }

    public void ImpactPopularity(int level)
    {
        int newPopularityLevel = (int)popularity + level;
        newPopularityLevel = 
            math.clamp(newPopularityLevel, (int)PopularityLevel.Hated, (int)PopularityLevel.Loved);

        popularity = (PopularityLevel)newPopularityLevel;
    }

    public void AbolishAds()
    {
        adCampaignsCosts = 0;
        ImpactPopularity(-1);
    }

    public float EndFiscalYear()
    {
        float moneyGained = EndCompanyFiscalYear();
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
        newConsumers = companyData.newConsumers;
        lostConsumers = companyData.lostConsumers;
        deadConsumers = companyData.deadConsumers;
        bonusMoney = companyData.bonusMoney;
    }

    private float EndCompanyFiscalYear()
    {
        int cigarettePackSoldPerSmoker = 180; // ~= 10 cigarettes per day
        float cigarettePackPriceMillion = cigarettePackPrice / 1000000f;
        float totalCigarettePackMoneyMillion = numConsumers * cigarettePackSoldPerSmoker * cigarettePackPriceMillion;

        float moneyGained = 
            totalCigarettePackMoneyMillion + bonusMoney - manufacturingCosts - lobbyingCosts - adCampaignsCosts;

        money += moneyGained;

        return moneyGained;
    }

    private void UpdateConsumersStats()
    {
        float deadConsumersTotal =
            deadConsumers * cigarettePackProduced.GetToxicityRatio();

        float lostConsumersTotal =
            lostConsumers / cigarettePackProduced.GetAddictionRatio();

        numConsumers += newConsumers - lostConsumersTotal - deadConsumersTotal;

        if (numConsumers < 0)
        {
            numConsumers = 0;
        }
    }
}
