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
    public static float MinCoefficientCostForIAToInvestSkill = 4f;

    public static int StartingYear = 2025;
    public static float YearLoadingMaxWidth = 72;

    public static Color UI_IncreaseColor = new Color(29 / 255f, 163 / 255f, 56 / 255f);
    public static Color UI_DecreaseColor = new Color(224 / 255f, 47 / 255f, 47 / 255f);
    public static Color UI_NormalColor = new Color(255 / 255f, 255 / 255f, 255 / 255f);

    public static Color playerBackgroundColor = new Color32(3, 0, 36, 240);
    public static Color playerSwitchViewButtonBackgroundColor = new Color32(219, 216, 242, 255);

    public static Color iaBackgroundColor = new Color32(75, 27, 0, 240);
    public static Color iaSwitchViewButtonBackgroundColor = new Color32(231, 170, 165, 255);

    public static Color GetTextUIColorFromDiff(float diff, bool positiveIsGood = true)
    {
        if (diff > 0)
        {
            return positiveIsGood ? UI_IncreaseColor : UI_DecreaseColor;
        }
        else if (diff < 0)
        {
            return positiveIsGood ? UI_DecreaseColor : UI_IncreaseColor;
        }
        else
        {
            return UI_NormalColor;
        }
    }

    public static string UI_companyGO = "Background/CompanyName";

    public static string UI_yearGO = "Background/Report/Year";
    public static string UI_yearLoaderGO = "Background/Report/Year/Loader";

    public static string UI_moneyGO = "Background/GameData/money/Value";
    public static string UI_moneyDiffGO = "Background/GameData/money/Diff";
    public static string UI_consumersGO = "Background/GameData/consumers/Value";
    public static string UI_consumersDiffGO = "Background/GameData/consumers/Diff";
    public static string UI_deadsGO = "Background/GameData/deads/Value";
    public static string UI_deadsDiffGO = "Background/GameData/deads/Diff";
    public static string UI_popularityGO = "Background/GameData/popularity/Value";
    public static string UI_popularityDiffGO = "Background/GameData/popularity/Diff";

    public static string UI_manufacturingGO = "Background/GameData/manufacturing/Value";
    public static string UI_manufacturingDiffGO = "Background/GameData/manufacturing/Diff";
    public static string UI_lobbyingGO = "Background/GameData/lobbying/Value";
    public static string UI_lobbyingDiffGO = "Background/GameData/lobbying/Diff";
    public static string UI_adCampaignsGO = "Background/GameData/adCampaigns/Value";
    public static string UI_adCampaignsDiffGO = "Background/GameData/adCampaigns/Diff";

    public static string UI_cigaretteToxicityLevelGO = "Background/GameData/cigaretteToxicityLevel/Value";
    public static string UI_cigaretteToxicityLevelDiffGO = "Background/GameData/cigaretteToxicityLevel/Diff";
    public static string UI_cigaretteAddictionLevelGO = "Background/GameData/cigaretteAddictionLevel/Value";
    public static string UI_cigaretteAddictionLevelDiffGO = "Background/GameData/cigaretteAddictionLevel/Diff";

    public static string UI_iaStrategyGO = "Background/GameData/IA_Strategy";
    public static string UI_iaStrategyText = "Background/GameData/IA_Strategy/Strat";

    public static string UI_EventDescription = "Background/Description";
    public static string UI_AcceptPriceDescription = "Background/AcceptPrice";
    public static string UI_RefusePriceDescription = "Background/RefusePrice";

    public static string UI_EndgameScreenTitle = "Background/Title";
    public static List<string> UI_EndgameRankedCompanies = new List<string>{
        "Background/Ranking1/Row1/Company",
        "Background/Ranking2/Row2/Company"
    };
    public static List<string> UI_EndgameRankedMoney = new List<string>{
        "Background/Ranking1/Row1/Money",
        "Background/Ranking2/Row2/Money"
    };
    public static List<string> UI_EndgameRankedDead = new List<string>{
        "Background/Ranking1/Row1Deads/Deads",
        "Background/Ranking2/Row2Deads/Deads"
    };
    public static string VictoryMessage = "Vous avez gagné!";
    public static string DefeatMessage = "Vous avez perdu.";

    public static string IA_Strategy_MaxMoney = "maximiser ses gains directs";
    public static string IA_Strategy_MaxCustommer = "maximiser le nombre de ses consommateurs";
    public static string IA_Strategy_MinCosts = "minimiser ses coûts";

    public static string IA_Strategy_InvestInManufacturing = " et d'investir dans la production";
    public static string IA_Strategy_InvestInAds = " et d'investir dans les publicités";
    public static string IA_Strategy_InvestInPopularity = " et d'investir dans la popularité";

    public static string PublicityBuildingsTag = "PublicityBuildings";
    public static string PopularityBuildingsTag = "PopularityBuildings";
    public static string ManufacturingBuildingsTag = "ManufacturingBuildings";
    public static string CustomerSharesTag = "CustomerShares";
    public static string ChestTag = "Chest";
}
