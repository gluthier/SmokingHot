
using System;
using UnityEngine;

public class NoEvent : WorldEvent
{
    public NoEvent()
    {
        title = "Rien de spécial";
        description = "Il ne se passe rien.";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
    }

    public override void RefuseEvent(CompanyEntity company)
    {
    }
}