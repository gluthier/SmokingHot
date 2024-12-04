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
        title = "Reine de la pop décédée";

        description = "La plus grande pop-star internationale est décédée suite à des problèmes de santé lié à la consommation de cigarettes. La réaction du publique est devastatrice. Nos analystes proposent de faire une campagnes publicitaires montrant nos efforts pour réduire les dégâts du tabac sur la santé.";

        acceptPriceDescription =
            $"Coûte {acceptMoney} M\n" +
            $"{acceptChanceGood}% de réussite d'augmenter l'image publique\n" +
            $"{acceptChanceBad}% de la réduire";

        refusePriceDescription =
            $"{refuseChance}% de réduire l'image publique";

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