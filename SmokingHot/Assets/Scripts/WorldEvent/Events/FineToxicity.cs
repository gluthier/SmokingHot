using System;
using System.Collections.Generic;
using UnityEngine;

public class FineToxicity : WorldEvent
{
    private int acceptMoney = 15;
    private int acceptChance = 75;
    private int refuseMoney = 20;
    private int refuseChanceGood = 33;
    private int refuseChanceBad = 33;

    public FineToxicity()
    {
        description = "Nous avons �t� amend�s � cause du niveau de toxicit� trop �lev� de nos cigarettes. Nos analystes proposent de refuser en luttant juridiquement contre, risquant le quitte ou double.";

        acceptPriceDescription =
            $"-{acceptMoney} millions de francs\n" +
            $"{acceptChance}% -1 popularit�";

        refusePriceDescription =
            $"-{refuseMoney} millions de francs\n" +
            $"{refuseChanceGood}% +1 popularit�\n" +
            $"{refuseChanceBad}% -2 popularit�";

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