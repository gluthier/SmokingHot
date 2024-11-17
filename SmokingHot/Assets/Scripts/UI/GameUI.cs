using UnityEngine;
using TMPro;
using static GameManager;
using Unity.Mathematics;

public class GameUI : MonoBehaviour
{
    private GameManager.GameUIData prevUIData;

    private TextMeshProUGUI conglomerateName;
    private TextMeshProUGUI year;

    private TextMeshProUGUI continent;
    private TextMeshProUGUI population;
    private TextMeshProUGUI populationDiff;
    private TextMeshProUGUI smokerPercentage;
    private TextMeshProUGUI smokerPercentageDiff;
    private TextMeshProUGUI deathSmokerPercentage;
    private TextMeshProUGUI deathSmokerPercentageDiff;

    private TextMeshProUGUI money;
    private TextMeshProUGUI moneyDiff;
    private TextMeshProUGUI newSmokerAcquisition;
    private TextMeshProUGUI newSmokerAcquisitionDiff;
    private TextMeshProUGUI smokerRetention;
    private TextMeshProUGUI smokerRetentionDiff;

    private TextMeshProUGUI CigaretteToxicityLevel;
    private TextMeshProUGUI CigaretteAddictionLevel;

    private TextMeshProUGUI KidPopularityLevel;
    private TextMeshProUGUI TeenagerPopularityLevel;
    private TextMeshProUGUI YoungAdultPopularityLevel;
    private TextMeshProUGUI AdultPopularityLevel;
    private TextMeshProUGUI SeniorPopularityLevel;

    private TextMeshProUGUI adCampaigns;

    void Awake()
    {
        conglomerateName = FindTextField(Env.UI_congomerateGO);
        year = FindTextField(Env.UI_yearGO);

        continent = FindTextField(Env.UI_continentGO);
        population = FindTextField(Env.UI_populationGO);
        populationDiff = FindTextField(Env.UI_populationDiffGO);
        smokerPercentage = FindTextField(Env.UI_smokersGO);
        smokerPercentageDiff = FindTextField(Env.UI_smokersDiffGO);
        deathSmokerPercentage = FindTextField(Env.UI_deathSmokerPercentageGO);
        deathSmokerPercentageDiff = FindTextField(Env.UI_deathSmokerPercentageDiffGO);

        money = FindTextField(Env.UI_moneyGO);
        moneyDiff = FindTextField(Env.UI_moneyDiffGO);
        newSmokerAcquisition = FindTextField(Env.UI_newSmokerAcquisitionGO);
        newSmokerAcquisitionDiff = FindTextField(Env.UI_newSmokerAcquisitionDiffGO);
        smokerRetention = FindTextField(Env.UI_smokerRetentionGO);
        smokerRetentionDiff = FindTextField(Env.UI_smokerRetentionDiffGO);

        CigaretteToxicityLevel = FindTextField(Env.UI_CigaretteToxicityLevelGO);
        CigaretteAddictionLevel = FindTextField(Env.UI_CigaretteAddictionLevelGO);

        KidPopularityLevel = FindTextField(Env.UI_KidPopularityLevelGO);
        TeenagerPopularityLevel = FindTextField(Env.UI_TeenagerPopularityLevelGO);
        YoungAdultPopularityLevel = FindTextField(Env.UI_YoungAdultPopularityLevelGO);
        AdultPopularityLevel = FindTextField(Env.UI_AdultPopularityLevelGO);
        SeniorPopularityLevel = FindTextField(Env.UI_SeniorPopularityLevelGO);

        adCampaigns = FindTextField(Env.UI_adCampaignsGO);
    }

    public void PopulateMainUI(GameManager.GameUIData gameUIData, bool showUpdate)
    {

        conglomerateName.text = gameUIData.conglomerateName;
        year.text = $"{gameUIData.year + 1}";

        continent.text = $"{gameUIData.continent}";

        SetTextField(population, populationDiff,
            Env.MillionUnit, prevUIData.population, gameUIData.population, showUpdate);

        SetTextField(smokerPercentage, smokerPercentageDiff,
            Env.MillionUnit, prevUIData.smokerPercentage, gameUIData.smokerPercentage, showUpdate);

        SetTextField(deathSmokerPercentage, deathSmokerPercentageDiff,
            Env.PercentUnit, prevUIData.deathSmokerPercentage, gameUIData.deathSmokerPercentage, showUpdate);

        SetTextField(money, moneyDiff,
            Env.MillionUnit, prevUIData.money, gameUIData.money, showUpdate);

        SetTextField(newSmokerAcquisition, newSmokerAcquisitionDiff,
            Env.PercentUnit, prevUIData.newSmokerAcquisition, gameUIData.newSmokerAcquisition, showUpdate);

        SetTextField(smokerRetention, smokerRetentionDiff,
            Env.PercentUnit, prevUIData.smokerRetention, gameUIData.smokerRetention, showUpdate);

        CigaretteToxicityLevel.text = gameUIData.cigarettePackProduced.GetToxicityDescription();
        CigaretteAddictionLevel.text = gameUIData.cigarettePackProduced.GetAddictionDescription();

        KidPopularityLevel.text = SimulationManager.GetPopularityDescription(
            gameUIData.popularityByAgeBracket[SimulationManager.AgeBracket.Kid]);
        TeenagerPopularityLevel.text = SimulationManager.GetPopularityDescription(
            gameUIData.popularityByAgeBracket[SimulationManager.AgeBracket.Teenager]);
        YoungAdultPopularityLevel.text = SimulationManager.GetPopularityDescription(
            gameUIData.popularityByAgeBracket[SimulationManager.AgeBracket.YoungAdult]);
        AdultPopularityLevel.text = SimulationManager.GetPopularityDescription(
            gameUIData.popularityByAgeBracket[SimulationManager.AgeBracket.Adult]);
        SeniorPopularityLevel.text = SimulationManager.GetPopularityDescription(
            gameUIData.popularityByAgeBracket[SimulationManager.AgeBracket.Senior]);

        // Todo: show list of ad campaigns?
        adCampaigns.text = $"{gameUIData.adCampaigns.Count} running";

        prevUIData = gameUIData;
    }
        
    private void SetTextField(TextMeshProUGUI textField, TextMeshProUGUI diffField,
        string unit, float prevData, float currentData, bool showUpdate)
    {
        textField.text = $"{GetDisplayableNum(currentData)} {unit}";

        if (showUpdate)
        {
            float diff = currentData - prevData;

            // Changes text field only if difference is notable
            if (math.abs(diff) < 0.01)
            {
                diffField.text = "";
                diffField.color = Env.UI_NormalColor;
                return;
            }

            if (diff > 0)
                diffField.text = $" +{GetDisplayableNum(diff)} {unit}";
            if (diff < 0)
                diffField.text = $" {GetDisplayableNum(diff)} {unit}"; // minus sign already present in diff

            diffField.color = Env.GetTextUIColorFromDiff(diff);
        }
    }

    private string GetDisplayableNum(float num)
    {
        string numDisplay = "";

        if (num >  100000 ||
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
