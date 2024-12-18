using System;
using System.Collections.Generic;
using UnityEngine;

public class UnethicalBonus : WorldEvent
{
    private int acceptMoney = 50;
    private int refuseChance = 50;

    public UnethicalBonus()
    {
        description = "Une fondation pro-tabac nous offre un prix pour nos cigarettes responsables.";

        acceptPriceDescription =
            Env.ColorizePositiveText($"+{acceptMoney} millions de francs");

        refusePriceDescription =
            Env.ColorizePositiveText($"{refuseChance}% +1 popularité");

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