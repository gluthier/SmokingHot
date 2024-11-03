using Newtonsoft.Json;
using UnityEngine;

public static class GameDataLoader
{
    public static GameData Load()
    {
        string filePath = System.IO.Path.Combine(
            Application.streamingAssetsPath, Env.GameDataJsonFileName);
        string json = System.IO.File.ReadAllText(filePath);

        return JsonConvert.DeserializeObject<GameData>(json);
    }
}

public class GameData
{
    public float totalYearSimulated;
    public float gameMinutesLength;
    public ContinentData[] continents;
}

public class ContinentData
{
    public string continentName;
    public float cigarettePackPrice;
    public long population;
    public PopulationDistributionData populationDistribution;
    public float smokerPercentage;
    public float deathSmokerPercentage;
    public float cigarettePerDay;
}

public class PopulationDistributionData
{
    public float kid;
    public float teenager;
    public float youngadult;
    public float adult;
    public float senior;
}