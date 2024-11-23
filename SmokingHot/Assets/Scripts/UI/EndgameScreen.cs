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
        var simulationConglomerates = simulationManager.getConglomerates();
        var sortedConglomerates = new List<ConglomerateEntity>(simulationConglomerates);
        var playerConglomerate = simulationConglomerates[Env.PlayerConglomerateID];

        sortedConglomerates.Sort((a, b) => a.totalMoney.CompareTo(b.totalMoney));

        if (sortedConglomerates[0] == playerConglomerate)
        {
            title.text = "You won!";
        }
        else
        {
            title.text = "You lost!";
        }

        for (int i = 0; i < rankedConglomerates.Count; ++i) {
            rankedConglomerates[i].text = $"{sortedConglomerates[i].conglomerateName}";
        }
    }

    private TextMeshProUGUI FindTextField(string gameObjectName)
    {
        return transform.Find(gameObjectName)
            .GetComponent<TextMeshProUGUI>();
    }    
}
