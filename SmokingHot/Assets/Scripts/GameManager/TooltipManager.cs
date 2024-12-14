using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public GameObject PublicityBuildingsTooltip;
    public GameObject PopularityBuildingsTooltip;
    public GameObject ManufacturingBuildingsTooltip;
    public GameObject CustomerSharesTooltip;
    public GameObject ChestTooltip;

    void Start()
    {
        HideAllTooltips();
    }

    public void ShowToolTipIfMatchTag(GameObject collider)
    {
        HideAllTooltips();

        if (collider.CompareTag(Env.PublicityBuildingsTag))
            PublicityBuildingsTooltip.SetActive(true);

        else if (collider.CompareTag(Env.PopularityBuildingsTag))
            PopularityBuildingsTooltip.SetActive(true);

        else if (collider.CompareTag(Env.ManufacturingBuildingsTag))
            ManufacturingBuildingsTooltip.SetActive(true);

        else if (collider.CompareTag(Env.CustomerSharesTag))
            CustomerSharesTooltip.SetActive(true);

        else if (collider.CompareTag(Env.ChestTag))
            ChestTooltip.SetActive(true);
    }

    public void HideAllTooltips()
    {
        PublicityBuildingsTooltip.SetActive(false);
        PopularityBuildingsTooltip.SetActive(false);
        ManufacturingBuildingsTooltip.SetActive(false);
        CustomerSharesTooltip.SetActive(false);
        ChestTooltip.SetActive(false);
    }
}
