
using System;
using UnityEngine;

public class NoEvent : WorldEvent
{
    public NoEvent()
    {
        title = "Empty Event";
        description = "...";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
    }

    public override void RefuseEvent(CompanyEntity company)
    {
    }
}