using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public GameObject PublicityBuildingsTooltip;
    public GameObject PopularityBuildingsTooltip;
    public GameObject ManufacturingBuildingsTooltip;
    public GameObject PlayerSharesTooltip;
    public GameObject ConcurrentSharesTooltip;
    public GameObject ChestTooltip;

    void Start()
    {
        HideAllTooltips();
    }

    public void ShowToolTipIfMatchTag(GameObject collider)
    {
        HideAllTooltips();

        if (collider.CompareTag(Env.PublicityBuildingsTag))
        {
            PublicityBuildingsTooltip.SetActive(true);
        }
        else if (collider.CompareTag(Env.PopularityBuildingsTag))
        {
            PopularityBuildingsTooltip.SetActive(true);
        }
        else if (collider.CompareTag(Env.ManufacturingBuildingsTag))
        {
            ManufacturingBuildingsTooltip.SetActive(true);
        }
        else if (collider.CompareTag(Env.CustomerSharesTag))
        {
            PlayerSharesTooltip.SetActive(true);
            ConcurrentSharesTooltip.SetActive(true);
        }
        else if (collider.CompareTag(Env.ChestTag))
        {
            ChestTooltip.SetActive(true);
        }
    }

    public void HideAllTooltips()
    {
        PublicityBuildingsTooltip.SetActive(false);
        PopularityBuildingsTooltip.SetActive(false);
        ManufacturingBuildingsTooltip.SetActive(false);
        PlayerSharesTooltip.SetActive(false);
        ConcurrentSharesTooltip.SetActive(false);
        ChestTooltip.SetActive(false);
    }
}
