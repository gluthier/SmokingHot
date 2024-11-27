using System;
using UnityEngine;

public class SocialInvestment : WorldEvent
{
    public SocialInvestment()
    {
        title = "SocialInvestment";
        description = "Nos analystes ont d�cid� qu'il faisait investir dans des activit�s socialement responsable comme si parrainage d'�v�nements caritatifs pour am�liorer l'image publique (ok: -10M et am�lioration de l'image publique, refus: 33% diminution de l'image publique)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, 10);
        company.ImpactReputation(1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(33, company.ImpactReputation, -1);
    }
}