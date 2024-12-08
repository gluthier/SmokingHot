
using System;
using UnityEngine;

public class NoEvent : WorldEvent
{
    public NoEvent()
    {
        description = "Il ne se passe rien.";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
    }

    public override void RefuseEvent(CompanyEntity company)
    {
    }
}