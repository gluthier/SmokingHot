
public class WorldEvent
{
    public enum EventType
    {
        Political,
        Advertisement,
        RnD // R&D: research and developpement
    }

    public enum ImpactFactor
    {
        Negative,
        Positive
    }

    public delegate void AcceptEvent();
    public AcceptEvent acceptEvent;

    public delegate void RefuseEvent();
    public RefuseEvent refuseEvent;

    public string title;
    public string description;
    private EventType eventType;
    private ImpactFactor impactFactor;

    public WorldEvent(string title, string description,
        EventType eventType, ImpactFactor impactFactor,
        AcceptEvent acceptEvent, RefuseEvent refuseEvent)
    {
        this.title = title;
        this.description = description;
        this.eventType = eventType;
        this.impactFactor = impactFactor;
        this.acceptEvent = acceptEvent;
        this.refuseEvent = refuseEvent;
    }
}
