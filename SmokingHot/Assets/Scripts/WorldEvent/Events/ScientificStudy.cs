using System;
using System.Collections.Generic;
using UnityEngine;

public class ScientificStudy : WorldEvent
{
    private int acceptMoney = 10;
    private int acceptChanceGood = 33;
    private int acceptChanceBad = 33;

    public ScientificStudy()
    {
        description = "Une nouvelle �tude scientifique d�montre les d�g�ts nocifs de la cigarette sur la sant�. Nos analystes proposent de discr�tiser l'�tude et l'�quipe de scientifiques derri�re.";

        acceptPriceDescription =
            $"-{acceptMoney} millions\n" +
            $"{acceptChanceGood}% +1 popularit�\n" +
            $"{acceptChanceBad}% -2 popularit�";

        refusePriceDescription =
            $"-1 popularit�";

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
            acceptChanceBad, company.ImpactPopularity, -2);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ImpactPopularity(-1);
    }
}