using System;
using UnityEngine;

public class FineAddiction : WorldEvent
{
    public FineAddiction()
    {
        title = "FineAddiction";
        description = "Amendes car des études ont été menées qui démontres le niveau exagérément addictif des cigarettes (ok: -25M et 90% de chance de baisser l'image publique, refus: on lutte juridiquement: -15M et 33% de chance de ne pas changer l'image publique mais 33% de chance de baisser drastiquement l'image publique)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.Money, -25);

        System.Random random = new System.Random();
        int chance = random.Next(100);

        if (chance < 90)
        {
            company.ImpactReputation(-1);
        }
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.Money, -15);

        System.Random random = new System.Random();
        int chance = random.Next(100);

        if (chance < 33)
        {
            company.ImpactReputation(-1);
        }
        else if (chance >= 67)
        {
            company.ImpactReputation(-2);
        }
    }
}