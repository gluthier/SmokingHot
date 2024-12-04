using UnityEngine;
using System.Collections.Generic;

public static class Env
{

    public static string GameDataJsonFileName = "GameData.json";
    public static string WorldEventSOFolder = "WorldEventData";

    public static int DaysInAYear = 365;
    public static int WorldEventFrequencyYear = 5;

    public static float iaMalusPercentage = 0.8f;

    public static string PlayerButton = "Joueur";
    public static string IAButton = "Concurrent";

    public static string MillionUnit = "M";
    public static string PercentUnit = "%";

    public static Color UI_IncreaseColor = new Color(29 / 255f, 163 / 255f, 56 / 255f);
    public static Color UI_DecreaseColor = new Color(224 / 255f, 47 / 255f, 47 / 255f);
    public static Color UI_NormalColor = new Color(255 / 255f, 255 / 255f, 255 / 255f);

    public static Color playerBackgroundColor = new Color32(3, 0, 36, 240);
    public static Color playerSwitchViewButtonBackgroundColor = new Color32(219, 216, 242, 255);
    public static Color playerSwitchViewButtonTextColor = new Color32(4, 1, 21, 255);

    public static Color iaBackgroundColor = new Color32(26, 10, 0, 240);
    public static Color iaSwitchViewButtonBackgroundColor = new Color32(234, 223, 195, 255);
    public static Color iaSwitchViewButtonTextColor = new Color32(46, 17, 0, 255);

    public static Color GetTextUIColorFromDiff(float diff)
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
    public static string UI_companyGO = "Background/CompanyName";

    public static string UI_moneyGO = "GameData/money/Value";
    public static string UI_moneyDiffGO = "GameData/money/Diff";
    public static string UI_popularityGO = "GameData/popularity/Value";
    public static string UI_popularityDiffGO = "GameData/popularity/Diff";
    public static string UI_consumersGO = "GameData/consumers/Value";
    public static string UI_consumersDiffGO = "GameData/consumers/Diff";

    public static string UI_manufacturingGO = "GameData/manufacturing/Value";
    public static string UI_manufacturingDiffGO = "GameData/manufacturing/Diff";
    public static string UI_lobbyingGO = "GameData/lobbying/Value";
    public static string UI_lobbyingDiffGO = "GameData/lobbying/Diff";
    public static string UI_adCampaignsGO = "GameData/adCampaigns/Value";
    public static string UI_adCampaignsDiffGO = "GameData/adCampaigns/Diff";

    public static string UI_cigaretteToxicityLevelGO = "GameData/cigaretteToxicityLevel/Value";
    public static string UI_cigaretteToxicityLevelDiffGO = "GameData/cigaretteToxicityLevel/Diff";
    public static string UI_cigaretteAddictionLevelGO = "GameData/cigaretteAddictionLevel/Value";
    public static string UI_cigaretteAddictionLevelDiffGO = "GameData/cigaretteAddictionLevel/Diff";

    public static string UI_EventTitle = "Background/Title";
    public static string UI_EventDescription = "Background/Description";
    public static string UI_AcceptPriceDescription = "Background/AcceptPrice";
    public static string UI_RefusePriceDescription = "Background/RefusePrice";

    public static string UI_EndgameScreenTitle = "Endgame/Title";
    public static List<string> UI_EndgameRankedConglomerates = new List<string>{
        "Endgame/Rank1",
        "Endgame/Rank2",
        "Endgame/Rank3",
        "Endgame/Rank4",
    };
}
