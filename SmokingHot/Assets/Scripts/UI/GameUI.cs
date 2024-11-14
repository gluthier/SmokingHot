using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI conglomerateName;

    private TextMeshProUGUI continent;
    private TextMeshProUGUI population;
    private TextMeshProUGUI smokerPercentage;
    private TextMeshProUGUI deathSmokerPercentage;

    private TextMeshProUGUI money;
    private TextMeshProUGUI newSmokerAcquisition;
    private TextMeshProUGUI smokerRetention;

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

        continent = FindTextField(Env.UI_continentGO);
        population = FindTextField(Env.UI_populationGO);
        smokerPercentage = FindTextField(Env.UI_smokersGO);
        deathSmokerPercentage = FindTextField(Env.UI_deathSmokerPercentageGO);

        money = FindTextField(Env.UI_moneyGO);
        newSmokerAcquisition = FindTextField(Env.UI_newSmokerAcquisitionGO);
        smokerRetention = FindTextField(Env.UI_smokerRetentionGO);

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

        continent.text = $"{gameUIData.continent}";
        population.text = $"{gameUIData.population} M";
        smokerPercentage.text = $"{gameUIData.smokerPercentage * gameUIData.population} M";
        deathSmokerPercentage.text = $"{100 * gameUIData.deathSmokerPercentage} %";

        money.text = $"{gameUIData.money} M";
        newSmokerAcquisition.text = $"{100 * gameUIData.newSmokerAcquisition} %";
        smokerRetention.text = $"{100 * gameUIData.smokerRetention} %";

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
