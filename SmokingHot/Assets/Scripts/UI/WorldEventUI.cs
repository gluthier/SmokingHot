using TMPro;
using UnityEngine;

public class WorldEventUI : MonoBehaviour
{
    private TextMeshProUGUI eventTitle;
    private TextMeshProUGUI eventDescription;

    WorldEvent.AcceptEvent acceptEvent;
    WorldEvent.RefuseEvent refuseEvent;

    public delegate void FinishWorldEvent();
    public FinishWorldEvent finishWorldEvent;

    public void Init()
    {
        eventTitle = transform.Find(Env.UI_EventTitle)
            .GetComponent<TextMeshProUGUI>();

        eventDescription = transform.Find(Env.UI_EventDescription)
            .GetComponent<TextMeshProUGUI>();
    }

    public void DisplayEvent(WorldEvent worldEvent, FinishWorldEvent finishWorldEvent)
    {
        eventTitle.text = worldEvent.title;
        eventDescription.text = worldEvent.description;
        acceptEvent = worldEvent.acceptEvent;
        refuseEvent = worldEvent.refuseEvent;

        this.finishWorldEvent = finishWorldEvent;
    }

    public void OnAcceptButtonPressed()
    {
        acceptEvent();
        finishWorldEvent();
    }

    public void OnRefuseButtonPressed()
    {
        refuseEvent();
        finishWorldEvent();
    }
}
