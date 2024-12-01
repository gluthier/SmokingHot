using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;
using static SimulationManager;

public class GameUI : MonoBehaviour
{
    private GameManager.GameState prevUIData;

    public Image background;
    public Image switchViewButtonBackground;
    public TextMeshProUGUI switchViewButtonText;

    private Color playerBackgroundColor = new Color32(3, 0, 36, 255);
    private Color playerSwitchViewButtonBackgroundColor = new Color32(219, 216, 242, 255);
    private Color playerSwitchViewButtonTextColor = new Color32(4, 1, 21, 255);

    private Color iaBackgroundColor = new Color32(26, 10, 0, 255);
    private Color iaSwitchViewButtonBackgroundColor = new Color32(234, 223, 195, 255);
    private Color iaSwitchViewButtonTextColor = new Color32(46, 17, 0, 255);

    private bool isPlayerCompanyShown;

    private TextMeshProUGUI companyName;
    private TextMeshProUGUI year;

    private TextMeshProUGUI money;
    private TextMeshProUGUI moneyDiff;
    private TextMeshProUGUI popularity;
    private TextMeshProUGUI popularityDiff;
    private TextMeshProUGUI consumers;
    private TextMeshProUGUI consumersDiff;

    private TextMeshProUGUI manufacturing;
    private TextMeshProUGUI manufacturingDiff;
    private TextMeshProUGUI lobbying;
    private TextMeshProUGUI lobbyingDiff;
    private TextMeshProUGUI adCampaigns;
    private TextMeshProUGUI adCampaignsDiff;

    private TextMeshProUGUI cigaretteToxicityLevel;
    private TextMeshProUGUI cigaretteToxicityLevelDiff;
    private TextMeshProUGUI cigaretteAddictionLevel;
    private TextMeshProUGUI cigaretteAddictionLevelDiff;

    private GameManager.GameState playerGameState;
    private GameManager.GameState iaGameState;

    void Awake()
    {
        companyName = FindTextField(Env.UI_companyGO);
        year = FindTextField(Env.UI_yearGO);

        money = FindTextField(Env.UI_moneyGO);
        moneyDiff = FindTextField(Env.UI_moneyDiffGO);
        popularity = FindTextField(Env.UI_popularityGO);
        popularityDiff = FindTextField(Env.UI_popularityDiffGO);
        consumers = FindTextField(Env.UI_consumersGO);
        consumersDiff = FindTextField(Env.UI_consumersDiffGO);

        manufacturing = FindTextField(Env.UI_manufacturingGO);
        manufacturingDiff = FindTextField(Env.UI_manufacturingDiffGO);
        lobbying = FindTextField(Env.UI_lobbyingGO);
        lobbyingDiff = FindTextField(Env.UI_lobbyingDiffGO);
        adCampaigns = FindTextField(Env.UI_adCampaignsGO);
        adCampaignsDiff = FindTextField(Env.UI_adCampaignsDiffGO);

        cigaretteToxicityLevel = FindTextField(Env.UI_cigaretteToxicityLevelGO);
        cigaretteToxicityLevelDiff = FindTextField(Env.UI_cigaretteToxicityLevelDiffGO);
        cigaretteAddictionLevel = FindTextField(Env.UI_cigaretteAddictionLevelGO);
        cigaretteAddictionLevelDiff = FindTextField(Env.UI_cigaretteAddictionLevelDiffGO);

        isPlayerCompanyShown = true;
        SetBackgroundColors();
    }

    public void PopulateMainUI(GameManager.GameState playerGameState, GameManager.GameState iaGameState,
        bool showUpdate)
    {
        this.playerGameState = playerGameState;
        this.iaGameState = iaGameState;

        SetValuesFromGameState(GetGameStateToShow(), showUpdate);

        prevUIData = playerGameState;
    }

    public void SwitchView()
    {
        isPlayerCompanyShown = !isPlayerCompanyShown;

        SetValuesFromGameState(GetGameStateToShow(), true);
        SetBackgroundColors();
    }

    private GameManager.GameState GetGameStateToShow()
    {
        return isPlayerCompanyShown ? playerGameState : iaGameState;
    }

    private void SetBackgroundColors()
    {
        background.color =
            isPlayerCompanyShown ? playerBackgroundColor : iaBackgroundColor;

        switchViewButtonBackground.color =
            isPlayerCompanyShown ? playerSwitchViewButtonBackgroundColor : iaSwitchViewButtonBackgroundColor;

        switchViewButtonText.color =
            isPlayerCompanyShown ? playerSwitchViewButtonTextColor : iaSwitchViewButtonTextColor;
    }

    private void SetValuesFromGameState(GameManager.GameState showGameState, bool showUpdate)
    {
        year.text = $"{showGameState.year + 1}";
        companyName.text = showGameState.companyName;

        SetTextField(money, moneyDiff,
            prevUIData.money, showGameState.money, showUpdate);

        SetPopularityField(prevUIData.popularity, showGameState.popularity, showUpdate);

        SetTextField(consumers, consumersDiff,
            prevUIData.numConsumers, showGameState.numConsumers, showUpdate);

        SetTextField(manufacturing, manufacturingDiff,
            prevUIData.manufacturingCosts, showGameState.manufacturingCosts, showUpdate);

        SetTextField(lobbying, lobbyingDiff,
            prevUIData.lobbyingCosts, showGameState.lobbyingCosts, showUpdate);

        SetTextField(adCampaigns, adCampaignsDiff,
            prevUIData.adCampaignsCosts, showGameState.adCampaignsCosts, showUpdate);

        cigaretteToxicityLevel.text = showGameState.cigarettePackProduced.GetToxicityDescription();
        cigaretteAddictionLevel.text = showGameState.cigarettePackProduced.GetAddictionDescription();
    }

    private void SetPopularityField(PopularityLevel prevData, PopularityLevel currentData, bool showUpdate)
    {
        popularity.text =
            SimulationManager.GetPopularityDescription(currentData);

        if (showUpdate)
        {
            int diff = (int)currentData - (int)prevData;

            if (diff == 0)
            {
                popularityDiff.text = "";
                popularityDiff.color = Env.UI_NormalColor;
                return;
            }

            if (diff > 0)
                popularityDiff.text = $" +{diff} {Env.MillionUnit}";
            if (diff < 0)
                popularityDiff.text = $" {diff} {Env.MillionUnit}"; // minus sign already present in diff

            popularityDiff.color = Env.GetTextUIColorFromDiff(diff);
        }
    }

    private void SetTextField(TextMeshProUGUI textField, TextMeshProUGUI diffField,
        float prevData, float currentData, bool showUpdate)
    {
        textField.text = $"{GetDisplayableNum(currentData)} {Env.MillionUnit}";

        if (showUpdate)
        {
            float diff = currentData - prevData;

            if (math.abs(diff) < 0.01)
            {
                diffField.text = "";
                diffField.color = Env.UI_NormalColor;
                return;
            }

            if (diff > 0)
                diffField.text = $" +{GetDisplayableNum(diff)} {Env.MillionUnit}";
            if (diff < 0)
                diffField.text = $" {GetDisplayableNum(diff)} {Env.MillionUnit}"; // minus sign already present in diff

            diffField.color = Env.GetTextUIColorFromDiff(diff);
        }
    }

    private string GetDisplayableNum(float num)
    {
        string numDisplay = "";

        if (num > 100000 ||
            num < -100000 ||
            IsNumDisplayedInteger(num))
        {
            numDisplay = ((int)num).ToString();
        }
        else
        {
            numDisplay = num.ToString("F2");
        }

        return numDisplay;
    }

    private bool IsNumDisplayedInteger(float num)
    {
        float parseNum = num;
        float.TryParse(num.ToString("F2"), out parseNum);

        // test if float is an integer
        return parseNum == math.floor(parseNum);
    }

    private TextMeshProUGUI FindTextField(string gameObjectName)
    {
        return transform.Find(gameObjectName)
            .GetComponent<TextMeshProUGUI>();
    }
}
