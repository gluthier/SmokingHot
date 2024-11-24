using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EndgameScreen : MonoBehaviour
{
    private List<TextMeshProUGUI> rankedConglomerates = new List<TextMeshProUGUI>{};
    private TextMeshProUGUI title;
    public SimulationManager simulationManager;

    void Awake()
    {
        title = FindTextField(Env.UI_EndgameScreenTitle);

        foreach (string conglomerate in Env.UI_EndgameRankedConglomerates)
        {
            rankedConglomerates.Add(FindTextField(conglomerate));
        }
    }

    void Start()
    {
        CompanyEntity playerCompany = simulationManager.GetPlayerCompany();

        List<CompanyEntity> sortedCompanies = new List<CompanyEntity>
        {
            playerCompany,
            simulationManager.GetIACompany()
        };

        sortedCompanies.Sort((a, b) => a.money.CompareTo(b.money));

        if (sortedCompanies[0].isPlayer)
        {
            title.text = "You won!";
        }
        else
        {
            title.text = "You lost!";
        }

        for (int i = 0; i < rankedConglomerates.Count; ++i) {
            rankedConglomerates[i].text = $"{sortedCompanies[i].companyName}";
        }
    }

    private TextMeshProUGUI FindTextField(string gameObjectName)
    {
        return transform.Find(gameObjectName)
            .GetComponent<TextMeshProUGUI>();
    }    
}
