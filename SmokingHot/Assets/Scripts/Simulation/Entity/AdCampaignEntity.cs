using System.Collections.Generic;
using static SimulationManager;

public class AdCampaignEntity
{
    public enum AdType
    {
        NewSmokerAcquisition,
        SmokerRetention
    }

    public enum AdQualityReception
    {
        Bad = -1,
        Neutral = 0,
        Good = 1
    }

    private float priceByDay;
    private int durationInDays;
    private AgeBracket ageBracketTarget;
    private AdQualityReception qualityReception;
    private AdType adType;

    public AdCampaignEntity(float priceByDay, int durationInDays,
        AgeBracket ageBracketTarget, AdQualityReception qualityReception, AdType adType)
    {
        this.priceByDay = priceByDay;
        this.durationInDays = durationInDays;
        this.ageBracketTarget = ageBracketTarget;
        this.qualityReception = qualityReception;
        this.adType = adType;
    }

    public float EndFiscalYear()
    {
        int durationInDaysThisYear;
        if (durationInDays >= Env.DaysInAYear)
        {
            durationInDaysThisYear = Env.DaysInAYear;
            durationInDays -= Env.DaysInAYear;
        }
        else
        {
            durationInDaysThisYear = durationInDays;
            durationInDays = 0;
        }

        return durationInDaysThisYear * priceByDay;
    }

    public (AdType, AdQualityReception) GetResult()
    {
        return (adType, qualityReception);
    }

    public int GetDurationRemaining()
    {
        return durationInDays;
    }
}