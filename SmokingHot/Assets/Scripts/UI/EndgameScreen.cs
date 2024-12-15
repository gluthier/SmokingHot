using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EndgameScreen : MonoBehaviour
{
    private List<TextMeshProUGUI> rankedCompanies = new List<TextMeshProUGUI> { };
    private List<TextMeshProUGUI> rankedMoney = new List<TextMeshProUGUI> { };
    private List<TextMeshProUGUI> rankedDeads = new List<TextMeshProUGUI> { };
    private TextMeshProUGUI title;

    private void Init()
    {
        title = FindTextField(Env.UI_EndgameScreenTitle);

        foreach (string company in Env.UI_EndgameRankedCompanies)
        {
            rankedCompanies.Add(FindTextField(company));
        }

        foreach (string money in Env.UI_EndgameRankedMoney)
        {
            rankedMoney.Add(FindTextField(money));
        }

        foreach (string dead in Env.UI_EndgameRankedDead)
        {
            rankedDeads.Add(FindTextField(dead));
        }
    }

    public void PopulateUI(List<CompanyEntity> sortedCompanies)
    {
        if (title == null)
        {
            Init();
        }

        sortedCompanies.Sort((a, b) => a.GetMoney().CompareTo(b.GetMoney()));
        sortedCompanies.Reverse();

        if (sortedCompanies[0].IsPlayer())
        {
            title.text = Env.VictoryMessage;
        }
        else
        {
            title.text = Env.DefeatMessage;
        }

        for (int i = 0; i < rankedCompanies.Count; ++i)
        {
            rankedCompanies[i].text = $"{sortedCompanies[i].GetCompanyName()}";
            rankedMoney[i].text =
                $"{Utils.GetDisplayableNum(sortedCompanies[i].GetMoney())} millions de francs";
            rankedDeads[i].text =
                $"{Utils.GetDisplayableNum(sortedCompanies[i].GetTotalConsumerDeads())} millions de décès";
        }
    }

    public void RestartGame()
    {
        FindFirstObjectByType<GameManager>().RestartSimulation();
        gameObject.SetActive(false);
    }

    private TextMeshProUGUI FindTextField(string gameObjectName)
    {
        return transform.Find(gameObjectName)
            .GetComponent<TextMeshProUGUI>();
    }    
}
