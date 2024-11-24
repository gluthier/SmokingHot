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
    public int totalYearSimulated;
    public int gameMinutesLength;
    public CompanyData companyTemplate;
}

public class CompanyData
{
    public string companyName;
    public float startingMoneyMillion;
    public float startingConsumersMillion;
    public float startingManufacturingMillion;
    public float startingLobbyingMillion;
    public float startingAdCampaignsMillion;
    public float cigarettePackPrice;
    public float deathSmokerPercentage;
    public float newSmokerAcquisition;
    public float smokerRetention;
    public float returnOnInvestment;
}