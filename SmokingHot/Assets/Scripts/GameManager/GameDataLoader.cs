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
    public CompanyData playerCompany;
    public CompanyData iaCompany;
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
    public float newConsumers;
    public float lostConsumers;
    public float deadConsumers;
    public float bonusMoney;
}