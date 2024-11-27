
public class CigarettePackEntity
{
    public enum ToxicityLevel
    {
        Average,
        Bad,
        VeryBad
    }

    public enum AddictionLevel
    {
        Average,
        Addictive,
        VeryAddictive
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

    public string GetToxicityDescription()
    {
        switch (toxicity)
        {
            case ToxicityLevel.Average:
                return "Average";
            case ToxicityLevel.Bad:
                return "Bad";
            case ToxicityLevel.VeryBad:
                return "Very bad";
            default:
                return "";
        }
    }

    public string GetAddictionDescription()
    {
        switch (addiction)
        {
            case AddictionLevel.Average:
                return "Average";
            case AddictionLevel.Addictive:
                return "Addictive";
            case AddictionLevel.VeryAddictive:
                return "Very addictive";
            default:
                return "";
        }
    }

    public float GetToxicityRatio()
    {
        float toxicityRatio;
        switch (toxicity)
        {
            case ToxicityLevel.Bad:
                toxicityRatio = 1.05f;
                break;
            case ToxicityLevel.VeryBad:
                toxicityRatio = 1.1f;
                break;
            case ToxicityLevel.Average:
            default:
                toxicityRatio = 1f;
                break;
        }
        return toxicityRatio;
    }

    public float GetAddictionRatio()
    {
        float addictionRatio;
        switch (addiction)
        {
            case AddictionLevel.Addictive:
                addictionRatio = 1.05f;
                break;
            case AddictionLevel.VeryAddictive:
                addictionRatio = 1.1f;
                break;
            case AddictionLevel.Average:
            default:
                addictionRatio = 1f;
                break;
        }
        return addictionRatio;
    }
}
