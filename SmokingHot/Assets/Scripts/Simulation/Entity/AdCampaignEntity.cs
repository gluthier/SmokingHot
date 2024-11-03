using static SimulationManager;

public class AdCampaignEntity
{
    public enum AdQualityReception
    {
        Bad,
        Neutral,
        Good
    }

    public enum AdType
    {
        NewSmokerAcquisition,
        SmokerRetention
    }

    private float priceByYear;
    private float duration;
    private AgeBracket ageBracketTarget;
    private SmokerType smokerTypeTarget;
    private AdQualityReception qualityReception;
    private AdType adType;
}
