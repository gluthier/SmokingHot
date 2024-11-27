using System;
using UnityEngine;

public class SocialInvestment : WorldEvent
{
    public SocialInvestment()
    {
        title = "SocialInvestment";
        description = "Nos analystes ont décidé qu'il faisait investir dans des activités socialement responsable comme si parrainage d'événements caritatifs pour améliorer l'image publique (ok: -10M et amélioration de l'image publique, refus: 33% diminution de l'image publique)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.Money, -10);
        company.ImpactReputation(1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);

        if (chance < 33)
        {
            company.ImpactReputation(-1);
        }
    }
}