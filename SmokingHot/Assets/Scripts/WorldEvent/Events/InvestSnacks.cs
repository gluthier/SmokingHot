using System;
using UnityEngine;

public class InvestSnacks : WorldEvent
{
    public InvestSnacks()
    {
        title = "InvestSnacks";
        description = "Proposition de rachat d'une multi-nationale vendant des snacks (ok: -100M et +8M par an, refus: réputation -1 car investisseurs fâchés)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.Money, -100);
        company.ModifyParam(CompanyEntity.Param.BonusMoney, 8);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ImpactReputation(-1);
    }
}