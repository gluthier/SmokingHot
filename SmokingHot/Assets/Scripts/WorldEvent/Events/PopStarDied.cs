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
        title = "Reine de la pop d�c�d�e";

        description = "La plus grande pop-star internationale est d�c�d�e suite � des probl�mes de sant� li� � la consommation de cigarettes. La r�action du publique est devastatrice. Nos analystes proposent de faire une campagnes publicitaires montrant nos efforts pour r�duire les d�g�ts du tabac sur la sant�.";

        acceptPriceDescription =
            $"Co�te {acceptMoney} M\n" +
            $"{acceptChanceGood}% de r�ussite d'augmenter l'image publique\n" +
            $"{acceptChanceBad}% de la r�duire";

        refusePriceDescription =
            $"{refuseChance}% de r�duire l'image publique";

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