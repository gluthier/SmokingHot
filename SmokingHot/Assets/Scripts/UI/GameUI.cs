using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI conglomerateName;
    private TextMeshProUGUI money;
    private TextMeshProUGUI population;
    private TextMeshProUGUI smokerPercentage;
    private TextMeshProUGUI newSmokerAcquisition;
    private TextMeshProUGUI smokerRetention;
    private TextMeshProUGUI deathSmokerPercentage;
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
        money = FindTextField(Env.UI_moneyGO);
        population = FindTextField(Env.UI_populationGO);
        smokerPercentage = FindTextField(Env.UI_smokerPercentageGO);
        newSmokerAcquisition = FindTextField(Env.UI_newSmokerAcquisitionGO);
        smokerRetention = FindTextField(Env.UI_smokerRetentionGO);
        deathSmokerPercentage = FindTextField(Env.UI_deathSmokerPercentageGO);
        CigaretteToxicityLevel = FindTextField(Env.UI_CigaretteToxicityLevelGO);
        CigaretteAddictionLevel = FindTextField(Env.UI_CigaretteAddictionLevelGO);
        KidPopularityLevel = FindTextField(Env.UI_KidPopularityLevelGO);
        TeenagerPopularityLevel = FindTextField(Env.UI_TeenagerPopularityLevelGO);
        YoungAdultPopularityLevel = FindTextField(Env.UI_YoungAdultPopularityLevelGO);
        AdultPopularityLevel = FindTextField(Env.UI_AdultPopularityLevelGO);
        SeniorPopularityLevel = FindTextField(Env.UI_SeniorPopularityLevelGO);
        adCampaigns = FindTextField(Env.UI_adCampaignsGO);
    }

    public void PopulateMainUI(GameManager.GameUIData gameUIData)
    {
        conglomerateName.text = gameUIData.conglomerateName;

        money.text = gameUIData.money.ToString();
        population.text = gameUIData.population.ToString();

        smokerPercentage.text = gameUIData.smokerPercentage.ToString();
        newSmokerAcquisition.text = gameUIData.newSmokerAcquisition.ToString();
        smokerRetention.text = gameUIData.smokerRetention.ToString();
        deathSmokerPercentage.text = gameUIData.deathSmokerPercentage.ToString();

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
    }

    private TextMeshProUGUI FindTextField(string gameObjectName)
    {
        return transform.Find(gameObjectName)
            .GetComponent<TextMeshProUGUI>();
    }
}
