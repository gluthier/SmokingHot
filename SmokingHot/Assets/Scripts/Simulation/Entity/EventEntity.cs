
public class EventEntity
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

    private EventType eventType;
    private ImpactFactor impactFactor;
    private float costToAccept;
    private float costToReject;

}
