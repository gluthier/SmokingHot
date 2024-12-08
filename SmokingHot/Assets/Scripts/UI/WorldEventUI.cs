using TMPro;
using UnityEngine;

public class WorldEventUI : MonoBehaviour
{
    private CompanyEntity playerCompany;

    private TextMeshProUGUI eventDescription;
    private TextMeshProUGUI acceptPriceDescription;
    private TextMeshProUGUI refusePriceDescription;

    private WorldEvent worldEvent;

    public delegate void FinishWorldEvent();
    public FinishWorldEvent finishWorldEvent;

    public void Init(CompanyEntity playerCompany)
    {
        this.playerCompany = playerCompany;

        eventDescription = transform.Find(Env.UI_EventDescription)
            .GetComponent<TextMeshProUGUI>();

        acceptPriceDescription = transform.Find(Env.UI_AcceptPriceDescription)
            .GetComponent<TextMeshProUGUI>();

        refusePriceDescription = transform.Find(Env.UI_RefusePriceDescription)
            .GetComponent<TextMeshProUGUI>();
    }

    public void DisplayEvent(WorldEvent worldEvent, FinishWorldEvent finishWorldEvent)
    {
        this.worldEvent = worldEvent;

        eventDescription.text = worldEvent.description;
        acceptPriceDescription.text = worldEvent.acceptPriceDescription;
        refusePriceDescription.text = worldEvent.refusePriceDescription;

        this.finishWorldEvent = finishWorldEvent;
    }

    public void OnAcceptButtonPressed()
    {
        worldEvent.AcceptEvent(playerCompany);
        finishWorldEvent();
    }

    public void OnRefuseButtonPressed()
    {
        worldEvent.RefuseEvent(playerCompany);
        finishWorldEvent();
    }
}
