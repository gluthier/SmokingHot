using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    private TextMeshProUGUI healthText;
    private TextMeshProUGUI stressText;
    private TextMeshProUGUI cigAddText;
    private TextMeshProUGUI alcAddText;

    private TextMeshProUGUI cigaretteText;
    private TextMeshProUGUI alcoolText;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void Start()
    {
        healthText = transform.Find("Stats/Health/HealthValue")
            .GetComponent<TextMeshProUGUI>();
        stressText = transform.Find("Stats/Stress/StressValue")
            .GetComponent<TextMeshProUGUI>();
        cigAddText = transform.Find("Stats/CigAdd/CigAddValue")
            .GetComponent<TextMeshProUGUI>();
        alcAddText = transform.Find("Stats/AlcAdd/AlcAddValue")
            .GetComponent<TextMeshProUGUI>();

        cigaretteText = transform.Find("Inventory/Cigarette/CigaretteValue")
            .GetComponent<TextMeshProUGUI>();
        alcoolText = transform.Find("Inventory/Alcool/AlcoolValue")
            .GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUIText(int health, int healthMax,
        int stress, int stressMax,
        int cigAdd, int cigAddMax,
        int alcAdd, int alcAddMax,
        int cigarette, int alcool)
    {
        UpdateStatsText(health, healthMax, stress, stressMax, cigAdd, cigAddMax, alcAdd, alcAddMax);
        UpdateInventoryText(cigarette, alcool);
    }

    public void UpdateStatsText(int health, int healthMax,
        int stress, int stressMax,
        int cigAdd, int cigAddMax,
        int alcAdd, int alcAddMax)
    {
        healthText.text = $"{health}/{healthMax}";
        stressText.text = $"{stress}/{stressMax}";
        cigAddText.text = $"{cigAdd}/{cigAddMax}";
        alcAddText.text = $"{alcAdd}/{alcAddMax}";
    }

    public void UpdateInventoryText(int cigarette, int alcool)
    {
        cigaretteText.text = $"{cigarette}";
        alcoolText.text = $"{alcool}";
    }
}
