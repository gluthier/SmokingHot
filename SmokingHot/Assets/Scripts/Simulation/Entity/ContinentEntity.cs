using System.Collections.Generic;
using static SimulationManager;

public class ContinentEntity
{
    private string continentName;
    private float cigarettePackPrice;
    private long population;
    private float smokerPercentage;
    private float deathSmokerPercentage;
    private float cigarettePerDay;
    private Dictionary<AgeBracket, float> populationDistribution;

    public ContinentEntity(ContinentData continentData)
    {
        LoadData(continentData);
    }

    public void LoadData(ContinentData continentData)
    {
        continentName = continentData.continentName;
        cigarettePackPrice = continentData.cigarettePackPrice;
        population = continentData.population;
        smokerPercentage = continentData.smokerPercentage;
        deathSmokerPercentage = continentData.deathSmokerPercentage;
        cigarettePerDay = continentData.cigarettePerDay;

        populationDistribution = new Dictionary<AgeBracket, float>
        {
            { AgeBracket.Kid, continentData.populationDistribution.kid },
            { AgeBracket.Teenager, continentData.populationDistribution.teenager },
            { AgeBracket.YoungAdult, continentData.populationDistribution.youngadult },
            { AgeBracket.Adult, continentData.populationDistribution.adult },
            { AgeBracket.Senior, continentData.populationDistribution.senior }
        };
    }
}
