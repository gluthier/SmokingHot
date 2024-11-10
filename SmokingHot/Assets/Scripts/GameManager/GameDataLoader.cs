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
    public ConglomerateData[] conglomerates;
}

public class ConglomerateData
{
    public string conglomerateName;
    public string continentName;
    public double startingMoney;
    public int cigarettesPerPack;
    public float cigarettePackPrice;
    public long population;
    public PopulationDistributionData populationDistribution;
    public float smokerPercentage;
    public float deathSmokerPercentage;
    public float cigarettePerDay;
    public float newSmokerAcquisition;
    public float smokerRetention;
    public float productionCostPercentage;
    public float distributionCostPercentage;
}

public class PopulationDistributionData
{
    public float kid;
    public float teenager;
    public float youngadult;
    public float adult;
    public float senior;
}