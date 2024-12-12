using UnityEngine;
using System.Collections.Generic;

public static class Env
{

    public static string GameDataJsonFileName = "GameData.json";
    public static string WorldEventSOFolder = "WorldEventData";

    public static int DaysInAYear = 365;
    public static int WorldEventFrequencyYear = 5;

    public static string PlayerButton = "Joueur";
    public static string IAButton = "Concurrent";
    public static float MinCoefficientCostForIAToInvestSkill = 3f;

    public static int StartingYear = 2024;
    public static float YearLoadingMaxWidth = 72;

    public static Color UI_IncreaseColor = new Color(29 / 255f, 163 / 255f, 56 / 255f);
    public static Color UI_DecreaseColor = new Color(224 / 255f, 47 / 255f, 47 / 255f);
    public static Color UI_NormalColor = new Color(255 / 255f, 255 / 255f, 255 / 255f);

    public static Color playerBackgroundColor = new Color32(3, 0, 36, 240);
    public static Color playerSwitchViewButtonBackgroundColor = new Color32(219, 216, 242, 255);

    public static Color iaBackgroundColor = new Color32(26, 10, 0, 240);
    public static Color iaSwitchViewButtonBackgroundColor = new Color32(242, 238, 227, 255);

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

    public static string UI_companyGO = "Background/CompanyName";

    public static string UI_yearGO = "Background/Report/Year";
    public static string UI_yearLoaderGO = "Background/Report/Year/Loader";

    public static string UI_moneyGO = "GameData/money/Value";
    public static string UI_moneyDiffGO = "GameData/money/Diff";
    public static string UI_consumersGO = "GameData/consumers/Value";
    public static string UI_consumersDiffGO = "GameData/consumers/Diff";
    public static string UI_deadsGO = "GameData/deads/Value";
    public static string UI_deadsDiffGO = "GameData/deads/Diff";
    public static string UI_popularityGO = "GameData/popularity/Value";
    public static string UI_popularityDiffGO = "GameData/popularity/Diff";

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

    public static string UI_iaStrategyGO = "GameData/IA_Strategy";
    public static string UI_iaStrategyText = "GameData/IA_Strategy/Strat";

    public static string UI_EventDescription = "Background/Description";
    public static string UI_AcceptPriceDescription = "Background/AcceptPrice";
    public static string UI_RefusePriceDescription = "Background/RefusePrice";

    public static string UI_EndgameScreenTitle = "Title";
    public static List<string> UI_EndgameRankedCompanies = new List<string>{
        "Ranking/Row1/Company",
        "Ranking/Row2/Company"
    };
    public static List<string> UI_EndgameRankedMoney = new List<string>{
        "Ranking/Row1/Money",
        "Ranking/Row2/Money"
    };
    public static string VictoryMessage = "Vous avez gagné!";
    public static string DefeatMessage = "Vous avez perdu.";

    public static string IA_Strategy_MaxMoney = "maximiser ses gains directs";
    public static string IA_Strategy_MaxCustommer = "maximiser le nombre de ses consommateurs";
    public static string IA_Strategy_MinCosts = "minimiser ses coûts";

    public static string IA_Strategy_InvestInManufacturing = " et d'investir dans la production";
    public static string IA_Strategy_InvestInAds = " et d'investir dans les publicités";
    public static string IA_Strategy_InvestInPopularity = " et d'investir dans la popularité";
}
