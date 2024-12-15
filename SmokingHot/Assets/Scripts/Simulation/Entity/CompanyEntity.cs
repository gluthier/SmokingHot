using System.Collections.Generic;
using Unity.Mathematics;
using static SimulationManager;

public class CompanyEntity
{
    private bool isPlayer;

    // Shown simulation data
    private string companyName;
    private float money;
    private PopularityLevel popularity;
    private float numConsumers;
    private float manufacturingCosts;
    private float lobbyingCosts;
    private float adCampaignsCosts;
    private CigarettePackEntity cigarettePackProduced;

    // Hidden simulation data
    private float cigarettePackPrice;
    private float newConsumers;
    private float lostConsumers;
    private float deadConsumers;
    private float bonusMoney;

    public enum Param
    {
        Money,
        BonusMoney,
        cigarettePackPrice,
        NewConsumers,
        LostConsumers,
        DeadConsumers,
        Consumers,
        AdCampaignsCosts,
        ManufacturingCosts,
        LobbyingCosts
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

    public string GetCompanyName()
    {
        return this.companyName;
    }

    public float GetMoney()
    {
        return this.money;
    }

    public float GetConsumers()
    {
        return numConsumers;
    }

    public bool IsPlayer()
    {
        return this.isPlayer;
    }

    public CigarettePackEntity GetCigarettePack()
    {
        return cigarettePackProduced;
    }

    public GameManager.GameState RetrieveCompanyGameState(int yearPassed, string iaStrategy)
    {
        return new GameManager.GameState
        {
            year = yearPassed,
            companyName = companyName,
            money = money,
            numConsumers = numConsumers,
            deads = deadConsumers,
            popularity = popularity,
            manufacturingCosts = manufacturingCosts,
            lobbyingCosts = lobbyingCosts,
            adCampaignsCosts = adCampaignsCosts,
            cigarettePackProduced = cigarettePackProduced,
            cigarettePackPrice = cigarettePackPrice,
            deadConsumers = deadConsumers,
            newConsumers = newConsumers,
            lostConsumers = lostConsumers,
            yearlyMoneyBonus = bonusMoney,
            iaStrategy = iaStrategy
        };
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
            case Param.AdCampaignsCosts:
                adCampaignsCosts += amount;
                break;
            case Param.ManufacturingCosts:
                manufacturingCosts += amount;
                break;
            case Param.LobbyingCosts:
                lobbyingCosts += amount;
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
            case Param.AdCampaignsCosts:
                adCampaignsCosts *= amount;
                break;
            case Param.ManufacturingCosts:
                manufacturingCosts *= amount;
                break;
            case Param.LobbyingCosts:
                lobbyingCosts *= amount;
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

    public void EndFiscalYear()
    {
        EndCompanyFiscalYear();
        UpdateConsumersStats();
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

    private void EndCompanyFiscalYear()
    {
        int cigarettePackSoldPerSmoker = 180; // ~= 10 cigarettes per day
        float cigarettePackPriceMillion = cigarettePackPrice / 1000000f;
        float totalCigarettePackMoneyMillion = numConsumers * cigarettePackSoldPerSmoker * cigarettePackPriceMillion;

        float moneyGained = 
            totalCigarettePackMoneyMillion + bonusMoney - manufacturingCosts - lobbyingCosts - adCampaignsCosts;

        money += moneyGained;
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
