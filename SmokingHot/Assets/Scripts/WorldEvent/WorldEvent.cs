
public abstract class WorldEvent
{
    public string title;
    public string description;

    public abstract void AcceptEvent(CompanyEntity company);

    public abstract void RefuseEvent(CompanyEntity company);
}