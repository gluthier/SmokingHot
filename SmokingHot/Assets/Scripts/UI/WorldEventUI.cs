using TMPro;
using UnityEngine;

public class WorldEventUI : MonoBehaviour
{
    private GameManager gameManager;

    private TextMeshProUGUI eventTitle;
    private TextMeshProUGUI eventDescription;

    private WorldEventSO worldEvent;

    public delegate void FinishWorldEvent();
    public FinishWorldEvent finishWorldEvent;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;

        eventTitle = transform.Find(Env.UI_EventTitle)
            .GetComponent<TextMeshProUGUI>();

        eventDescription = transform.Find(Env.UI_EventDescription)
            .GetComponent<TextMeshProUGUI>();
    }

    public void DisplayEvent(WorldEventSO worldEvent, FinishWorldEvent finishWorldEvent)
    {
        this.worldEvent = worldEvent;

        eventTitle.text = worldEvent.title;
        eventDescription.text = worldEvent.description;

        this.finishWorldEvent = finishWorldEvent;
    }

    public void OnAcceptButtonPressed()
    {
        worldEvent.AcceptEvent(gameManager);
        finishWorldEvent();
    }

    public void OnRefuseButtonPressed()
    {
        worldEvent.RefuseEvent(gameManager);
        finishWorldEvent();
    }
}
