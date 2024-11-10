
public class CigarettePackEntity
{
    public enum ToxicityLevel
    {
        VeryBad,
        Bad,
        Average
    }

    public enum AddictionLevel
    {
        VeryAddictive,
        Addictive,
        Average
    }

    private ToxicityLevel cigaretteToxicity;
    private AddictionLevel cigaretteAddiction;

    public CigarettePackEntity(ConglomerateData continentData)
    {
        cigaretteToxicity = ToxicityLevel.Average;
        cigaretteAddiction = AddictionLevel.Average;
    }
}
