using System;
using System.Collections.Generic;
using UnityEngine;

public class FineAddiction : WorldEvent
{
    private int acceptMoney = 15;
    private int acceptChance = 75;
    private int refuseMoney = 20;
    private int refuseChanceGood = 33;
    private int refuseChanceBad = 33;

    public FineAddiction()
    {
        title = "Amende";

        description = "Nous avons été amendé à cause du niveau d'addiciton trop élevé de nos cigarettes. Nos analystes proposent de refuser en luttant juridiquement contre, risquant le quitte ou double.";

        acceptPriceDescription =
            $"Coûte {acceptMoney} M\n" +
            $"{acceptChance}% de réduire l'image publique";

        refusePriceDescription =
            $"Coûte {refuseMoney} M\n" +
            $"{refuseChanceGood}% d'augmenter l'image publique\n" +
            $"{refuseChanceBad}% de réduire fortement l'image publique";

        acceptPositiveImpacts = new List<WorldEventImpact> { };
        acceptNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money,
            WorldEventImpact.Popularity
        };

        refusePositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Popularity
        };
        refuseNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money,
            WorldEventImpact.Popularity
        };
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);

        DoActionIfPercent(acceptChance, company.ImpactPopularity, -1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, refuseMoney);

        DoActionIfPercentElse(refuseChanceGood, company.ImpactPopularity, 1,
            refuseChanceBad, company.ImpactPopularity, -2);
    }
}