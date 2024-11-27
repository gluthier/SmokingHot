using System;
using UnityEngine;

public class UnethicalBonus : WorldEvent
{
    public UnethicalBonus()
    {
        title = "UnethicalBonus";
        description = "La fondation pro-tabac nous offre un prix pour nos cigarettes responsables (ok: +2M, refus: 50% de monter l'image publique)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.IncreaseParam(CompanyEntity.Param.Money, 2);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(50, company.ImpactReputation, 1);
    }
}