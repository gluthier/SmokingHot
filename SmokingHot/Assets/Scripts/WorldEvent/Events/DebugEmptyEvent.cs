
using System;
using UnityEngine;

public class DebugEmptyEvent : WorldEvent
{
    public DebugEmptyEvent()
    {
        title = "Debug Empty Event";
        description = "No description";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        Debug.Log("DebugEmptyEvent accept");
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        Debug.Log("DebugEmptyEvent refuse");
    }
}