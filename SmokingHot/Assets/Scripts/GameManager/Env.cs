using UnityEngine;

public static class Env
{

    public static string GameDataJsonFileName = "GameData.json";

    public static int DaysInAYear = 365;

    public static int PlayerConglomerateID = 2; // Europe

    public static float NewSmokerAcquisitionIncrement = 0.005f;
    public static float SmokerRetentioIncrement = 0.05f;

    public static string UI_congomerateGO = "Background/ConglomerateName";

    public static string UI_continentGO = "GameData/continent/Value";
    public static string UI_populationGO = "GameData/population/Value";
    public static string UI_smokersGO = "GameData/smokers/Value";
    public static string UI_deathSmokerPercentageGO = "GameData/deathSmokerPercentage/Value";

    public static string UI_moneyGO = "GameData/money/Value";
    public static string UI_newSmokerAcquisitionGO = "GameData/newSmokerAcquisition/Value";
    public static string UI_smokerRetentionGO = "GameData/smokerRetention/Value";

    public static string UI_CigaretteToxicityLevelGO = "GameData/CigaretteToxicityLevel/Value";
    public static string UI_CigaretteAddictionLevelGO = "GameData/CigaretteAddictionLevel/Value";

    public static string UI_KidPopularityLevelGO = "GameData/KidPopularityLevel/Value";
    public static string UI_TeenagerPopularityLevelGO = "GameData/TeenagerPopularityLevel/Value";
    public static string UI_YoungAdultPopularityLevelGO = "GameData/YoungAdultPopularityLevel/Value";
    public static string UI_AdultPopularityLevelGO = "GameData/AdultPopularityLevel/Value";
    public static string UI_SeniorPopularityLevelGO = "GameData/SeniorPopularityLevel/Value";

    public static string UI_adCampaignsGO = "GameData/adCampaigns/Value";


    #region DEBUG
    public const bool PRINT_DEBUG = false;
    public static void PrintDebug(string msg)
    {
        if (PRINT_DEBUG)
        {
            Debug.Log(msg);
        }
    }
    #endregion
}
