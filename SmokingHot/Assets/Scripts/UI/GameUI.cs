using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;
using static SimulationManager;

public class GameUI : MonoBehaviour
{
    public Image background;
    public Image switchViewButtonBackground;
    public TextMeshProUGUI switchViewButtonText;

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
    private GameManager.GameState prevPlayerGameState;

    private GameManager.GameState IAGameState;
    private GameManager.GameState prevIAGameState;

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
        prevPlayerGameState = this.playerGameState;
        prevIAGameState = this.IAGameState;

        this.playerGameState = playerGameState;
        this.IAGameState = iaGameState;

        SetValuesFromGameState(GetGameStateToShow(), GetPrevGameStateToShow(), showUpdate);
    }

    public void SwitchView()
    {
        isPlayerCompanyShown = !isPlayerCompanyShown;

        SetValuesFromGameState(GetGameStateToShow(), GetPrevGameStateToShow(), true);
        SetBackgroundColors();
        SetButtonText();
    }

    private GameManager.GameState GetGameStateToShow()
    {
        return isPlayerCompanyShown ? playerGameState : IAGameState;
    }

    private GameManager.GameState GetPrevGameStateToShow()
    {
        return isPlayerCompanyShown ? prevPlayerGameState : prevIAGameState;
    }

    private void SetBackgroundColors()
    {
        background.color =
            isPlayerCompanyShown ? Env.playerBackgroundColor : Env.iaBackgroundColor;

        switchViewButtonBackground.color =
            isPlayerCompanyShown ? Env.iaSwitchViewButtonBackgroundColor : Env.playerSwitchViewButtonBackgroundColor;

        switchViewButtonText.color =
            isPlayerCompanyShown ? Env.iaSwitchViewButtonTextColor : Env.playerSwitchViewButtonTextColor;
    }

    private void SetButtonText()
    {
        switchViewButtonText.text = isPlayerCompanyShown ? Env.IAButton : Env.PlayerButton;
    }

    private void SetValuesFromGameState(GameManager.GameState showGameState, GameManager.GameState prevGameState,
        bool showUpdate)
    {
        year.text = $"{showGameState.year + 1}";
        companyName.text = showGameState.companyName;

        SetTextField(money, moneyDiff,
            prevGameState.money, showGameState.money, showUpdate);

        SetPopularityField(prevGameState.popularity, showGameState.popularity, showUpdate);

        SetTextField(consumers, consumersDiff,
            prevGameState.numConsumers, showGameState.numConsumers, showUpdate);

        SetTextField(manufacturing, manufacturingDiff,
            prevGameState.manufacturingCosts, showGameState.manufacturingCosts, showUpdate);

        SetTextField(lobbying, lobbyingDiff,
            prevGameState.lobbyingCosts, showGameState.lobbyingCosts, showUpdate);

        SetTextField(adCampaigns, adCampaignsDiff,
            prevGameState.adCampaignsCosts, showGameState.adCampaignsCosts, showUpdate);

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
