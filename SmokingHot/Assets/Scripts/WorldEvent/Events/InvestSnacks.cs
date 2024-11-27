using System;
using UnityEngine;

public class InvestSnacks : WorldEvent
{
    public InvestSnacks()
    {
        title = "InvestSnacks";
        description = "Proposition de rachat d'une entreprise vendant des snacks (ok: -60M et +4M par an, refus: réputation -1 car investisseurs fâchés)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, 60);
        company.IncreaseParam(CompanyEntity.Param.BonusMoney, 4);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ImpactReputation(-1);
    }
}