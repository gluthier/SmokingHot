using UnityEngine;

public static class Env
{

    public static string GameDataJsonFileName = "GameData.json";

    public static int DaysInAYear = 365;
    public static int WorldEventFrequencyYear = 5;
    public static int PlayerConglomerateID = 2; // Europe

    public static float NewSmokerAcquisitionIncrement = 0.005f;
    public static float SmokerRetentioIncrement = 0.05f;

    public static string MillionUnit = "M";
    public static string PercentUnit = "%";

    public static Color UI_IncreaseColor = new Color(29 / 255f, 163 / 255f, 56 / 255f);
    public static Color UI_DecreaseColor = new Color(224 / 255f, 47 / 255f, 47 / 255f);
    public static Color UI_NormalColor = new Color(255 / 255f, 255 / 255f, 255 / 255f);

    public static Color GetTextUIColorFromDiff(double diff)
    {
        if (diff > 0)
        {
            return UI_IncreaseColor;
        }
        else if (diff < 0)
        {
            return UI_DecreaseColor;
        }
        else
        {
            return UI_NormalColor;
        }
    }

    public static string UI_yearGO = "SimulationData/Year/Value";
    public static string UI_congomerateGO = "Background/ConglomerateName";
    public static string UI_continentGO = "GameData/continent/Value";
    public static string UI_populationGO = "GameData/population/Value";
    public static string UI_populationDiffGO = "GameData/population/Diff";
    public static string UI_smokersGO = "GameData/smokers/Value";
    public static string UI_smokersDiffGO = "GameData/smokers/Diff";
    public static string UI_deathSmokerPercentageGO = "GameData/deathSmokerPercentage/Value";
    public static string UI_deathSmokerPercentageDiffGO = "GameData/deathSmokerPercentage/Diff";
    public static string UI_moneyGO = "GameData/money/Value";
    public static string UI_moneyDiffGO = "GameData/money/Diff";
    public static string UI_newSmokerAcquisitionGO = "GameData/newSmokerAcquisition/Value";
    public static string UI_newSmokerAcquisitionDiffGO = "GameData/newSmokerAcquisition/Diff";
    public static string UI_smokerRetentionGO = "GameData/smokerRetention/Value";
    public static string UI_smokerRetentionDiffGO = "GameData/smokerRetention/Diff";
    public static string UI_CigaretteToxicityLevelGO = "GameData/CigaretteToxicityLevel/Value";
    public static string UI_CigaretteAddictionLevelGO = "GameData/CigaretteAddictionLevel/Value";
    public static string UI_KidPopularityLevelGO = "GameData/KidPopularityLevel/Value";
    public static string UI_TeenagerPopularityLevelGO = "GameData/TeenagerPopularityLevel/Value";
    public static string UI_YoungAdultPopularityLevelGO = "GameData/YoungAdultPopularityLevel/Value";
    public static string UI_AdultPopularityLevelGO = "GameData/AdultPopularityLevel/Value";
    public static string UI_SeniorPopularityLevelGO = "GameData/SeniorPopularityLevel/Value";
    public static string UI_adCampaignsGO = "GameData/adCampaigns/Value";

    public static string UI_EventTitle = "Background/Title";
    public static string UI_EventDescription = "Background/Description";
}
