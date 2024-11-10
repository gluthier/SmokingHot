
public class CigarettePackEntity
{
    public enum ToxicityLevel
    {
        Average = 100,
        Bad = 101,
        VeryBad = 102
    }

    public enum AddictionLevel
    {
        Average = 100,
        Addictive = 101,
        VeryAddictive = 102
    }

    public ToxicityLevel toxicity;
    public AddictionLevel addiction;

    public CigarettePackEntity()
    {
        toxicity = ToxicityLevel.Average;
        addiction = AddictionLevel.Average;
    }

    // Has side effect of changing toxicity level if too addictive
    public void SetAddictionLevel(AddictionLevel addictionLevel)
    {
        addiction = addictionLevel;

        if (addiction == AddictionLevel.VeryAddictive)
        {
            toxicity = ToxicityLevel.VeryBad;
        }
        else if (addiction == AddictionLevel.Addictive)
        {
            if (toxicity == ToxicityLevel.Average)
            {
                toxicity = ToxicityLevel.Bad;
            }
        }
    }

    // No side effects
    public void SetToxicityLevel(ToxicityLevel toxicityLevel)
    {
        toxicity = toxicityLevel;
    }
}
