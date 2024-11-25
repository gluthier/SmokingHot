using TMPro;
using UnityEngine;

public class WorldEventUI : MonoBehaviour
{
    private CompanyEntity playerCompany;

    private TextMeshProUGUI eventTitle;
    private TextMeshProUGUI eventDescription;

    private WorldEvent worldEvent;

    public delegate void FinishWorldEvent();
    public FinishWorldEvent finishWorldEvent;

    public void Init(CompanyEntity playerCompany)
    {
        this.playerCompany = playerCompany;

        eventTitle = transform.Find(Env.UI_EventTitle)
            .GetComponent<TextMeshProUGUI>();

        eventDescription = transform.Find(Env.UI_EventDescription)
            .GetComponent<TextMeshProUGUI>();
    }

    public void DisplayEvent(WorldEvent worldEvent, FinishWorldEvent finishWorldEvent)
    {
        this.worldEvent = worldEvent;

        eventTitle.text = worldEvent.title;
        eventDescription.text = worldEvent.description;

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
