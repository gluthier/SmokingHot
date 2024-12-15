using System;
using System.Collections.Generic;
using UnityEngine;

public class SocialInvestment : WorldEvent
{
    private int acceptMoney = 10;
    private int refuseChance = 33;

    public SocialInvestment()
    {
        description = "Nos analystes ont décidé qu'il faisait investir dans un événement caritatif pour améliorer l'image publique en promouvant une image d'entreprise socialement responsable.";

        acceptPriceDescription =
            $"-{acceptMoney} millions de francs\n" +
            $"+1 popularité";

        refusePriceDescription =
            $"{refuseChance}% -1 popularité";

        acceptPositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Popularity
        };
        acceptNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money
        };

        refusePositiveImpacts = new List<WorldEventImpact> { };
        refuseNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Popularity
        };
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);
        company.ImpactPopularity(1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(refuseChance, company.ImpactPopularity, -1);
    }
}