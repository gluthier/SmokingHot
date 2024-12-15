using System;
using System.Collections.Generic;
using UnityEngine;

public class PopStarDied : WorldEvent
{
    private int acceptMoney = 20;
    private int acceptChanceGood = 10;
    private int acceptChanceBad = 40;
    private int refuseChance = 80;

    public PopStarDied()
    {
        description = "La plus grande popstar est décédée à cause d'un cancer du poumon. La réaction du publique est dévastatrice. Nos analystes proposent de faire une campagne publicitaire pour mettre en avant notre attention concernant la santé publique.";

        acceptPriceDescription =
            $"-{acceptMoney} millions de francs\n" +
            $"{acceptChanceGood}% +1 popularité\n" +
            $"{acceptChanceBad}% -1 popularité";

        refusePriceDescription =
            $"{refuseChance}% -1 popularité";

        acceptPositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Popularity
        };
        acceptNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money,
            WorldEventImpact.Popularity
        };

        refusePositiveImpacts = new List<WorldEventImpact> { };
        refuseNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Popularity
        };
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);

        DoActionIfPercentElse(acceptChanceGood, company.ImpactPopularity, 1,
            acceptChanceBad, company.ImpactPopularity, -1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(refuseChance, company.ImpactPopularity, -1);
    }
}