using System;
using System.Collections.Generic;
using UnityEngine;

public class UnethicalBonus : WorldEvent
{
    private int acceptMoney = 5;
    private int refuseChance = 50;

    public UnethicalBonus()
    {
        title = "Soutien inattendu";
        description = "Une fondation pro-tabac nous offre un prix pour nos cigarettes responsables.";

        description = "Une fondation pro-tabac nous offre un prix pour nos cigarettes responsables (ok: +2M, refus: 50% de monter l'image publique)";

        acceptPriceDescription =
            $"Donation de {acceptMoney} M";

        refusePriceDescription =
            $"{refuseChance}% d'augmenter l'image publique";

        acceptPositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money
        };
        acceptNegativeImpacts = new List<WorldEventImpact> { };

        refusePositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Popularity
        };
        refuseNegativeImpacts = new List<WorldEventImpact> { };
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.IncreaseParam(CompanyEntity.Param.Money, acceptMoney);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(refuseChance, company.ImpactPopularity, 1);
    }
}