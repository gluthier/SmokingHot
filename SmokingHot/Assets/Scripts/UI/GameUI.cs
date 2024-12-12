using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;
using static SimulationManager;

public class GameUI : MonoBehaviour
{
    private bool isPlayerCompanyShown;

    public Image background;
    public Image switchViewButtonBackground;
    public TextMeshProUGUI switchViewButtonText;

    private TextMeshProUGUI companyName;
    private TextMeshProUGUI year;
    private Image yearLoader;

    private TextMeshProUGUI money;
    private TextMeshProUGUI moneyDiff;
    private TextMeshProUGUI consumers;
    private TextMeshProUGUI consumersDiff;
    private TextMeshProUGUI deads;
    private TextMeshProUGUI deadsDiff;
    private TextMeshProUGUI popularity;
    private TextMeshProUGUI popularityDiff;

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

    private GameObject iaStrategyGO;
    private TextMeshProUGUI iaStrategyText;

    private GameManager.GameState playerGameState;
    private GameManager.GameState prevPlayerGameState;

    private GameManager.GameState IAGameState;
    private GameManager.GameState prevIAGameState;

    void Awake()
    {
        companyName = FindTextField(Env.UI_companyGO);

        year = FindTextField(Env.UI_yearGO);
        yearLoader = transform.Find(Env.UI_yearLoaderGO)
                              .GetComponent<Image>();

        money = FindTextField(Env.UI_moneyGO);
        moneyDiff = FindTextField(Env.UI_moneyDiffGO);
        consumers = FindTextField(Env.UI_consumersGO);
        consumersDiff = FindTextField(Env.UI_consumersDiffGO);
        deads = FindTextField(Env.UI_deadsGO);
        deadsDiff = FindTextField(Env.UI_deadsDiffGO);
        popularity = FindTextField(Env.UI_popularityGO);
        popularityDiff = FindTextField(Env.UI_popularityDiffGO);

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

        iaStrategyGO = transform.Find(Env.UI_iaStrategyGO).gameObject;
        iaStrategyText = FindTextField(Env.UI_iaStrategyText);
        iaStrategyGO.SetActive(false);

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

        bool showChangeExceptFirstYear = prevPlayerGameState.year != 0;

        SetValuesFromGameState(GetGameStateToShow(), GetPrevGameStateToShow(), showChangeExceptFirstYear);
        SetBackgroundColors();
        ShowIAStrategy();
        SetButtonText();
    }

    public void UpdateYearLoaderWidth(float percent)
    {
        RectTransform yearLoaderRect = yearLoader.GetComponent<RectTransform>();

        Vector2 sizeDelta = yearLoaderRect.sizeDelta;
        sizeDelta.x = percent * Env.YearLoadingMaxWidth;
        yearLoaderRect.sizeDelta = sizeDelta;
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
    }

    private void ShowIAStrategy()
    {
        iaStrategyGO.SetActive(!isPlayerCompanyShown);
    }

    private void SetButtonText()
    {
        switchViewButtonText.text = isPlayerCompanyShown ? Env.IAButton : Env.PlayerButton;
    }

    private void SetValuesFromGameState(GameManager.GameState showGameState, GameManager.GameState prevGameState,
        bool showUpdate)
    {
        year.text = $"{Env.StartingYear + showGameState.year}";
        companyName.text = showGameState.companyName;

        SetTextField(money, moneyDiff,
            prevGameState.money, showGameState.money, showUpdate);

        SetPopularityField(prevGameState.popularity, showGameState.popularity, showUpdate);

        SetTextField(consumers, consumersDiff,
            prevGameState.numConsumers, showGameState.numConsumers, showUpdate);

        SetTextField(deads, deadsDiff,
            prevGameState.deads, showGameState.deads, showUpdate);

        SetTextField(manufacturing, manufacturingDiff,
            prevGameState.manufacturingCosts, showGameState.manufacturingCosts, showUpdate);

        SetTextField(lobbying, lobbyingDiff,
            prevGameState.lobbyingCosts, showGameState.lobbyingCosts, showUpdate);

        SetTextField(adCampaigns, adCampaignsDiff,
            prevGameState.adCampaignsCosts, showGameState.adCampaignsCosts, showUpdate);

        SetCigaretteCompositionFields(prevGameState.cigarettePackProduced, showGameState.cigarettePackProduced, showUpdate);

        iaStrategyText.text = showGameState.iaStrategy;
    }

    private void SetTextField(TextMeshProUGUI textField, TextMeshProUGUI diffField,
        float prevData, float currentData, bool showUpdate)
    {
        textField.text = $"{Utils.GetDisplayableNum(currentData)}";

        if (showUpdate)
        {
            float diff = currentData - prevData;
            SetDiffTextField(diff, diffField);
        }
    }

    private void SetPopularityField(PopularityLevel prevData, PopularityLevel currentData, bool showUpdate)
    {
        popularity.text =
            SimulationManager.GetPopularityDescription(currentData);

        if (showUpdate)
        {
            int diff = (int)currentData - (int)prevData;
            SetDiffTextField(diff, popularityDiff);
        }
    }

    private void SetCigaretteCompositionFields(CigarettePackEntity prevData, CigarettePackEntity currentData, bool showUpdate)
    {
        cigaretteToxicityLevel.text = currentData.GetToxicityDescription();
        cigaretteAddictionLevel.text = currentData.GetAddictionDescription();

        if (showUpdate &&
            prevData != null)
        {
            int diffToxicity = (int)currentData.toxicity - (int)prevData.toxicity;
            SetDiffTextField(diffToxicity, cigaretteToxicityLevelDiff);

            int diffAddiction = (int)currentData.addiction - (int)prevData.addiction;
            SetDiffTextField(diffAddiction, cigaretteAddictionLevelDiff);
        }
    }

    private void SetDiffTextField(float diff, TextMeshProUGUI field)
    {
        if (math.abs(diff) < 0.01)
        {
            field.text = "";
            field.color = Env.UI_NormalColor;
            return;
        }

        if (diff > 0)
        {
            field.text = $"+{Utils.GetDisplayableNum(diff)}";
        }
        else if (diff < 0)
        {
            field.text = $"{Utils.GetDisplayableNum(diff)}"; // minus sign already present in diff
        }

        field.color = Env.GetTextUIColorFromDiff(diff);
    }

    private TextMeshProUGUI FindTextField(string gameObjectName)
    {
        return transform.Find(gameObjectName)
            .GetComponent<TextMeshProUGUI>();
    }
}
