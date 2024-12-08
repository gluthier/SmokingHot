using System;
using System.Collections.Generic;
using UnityEngine;

public class VotationIncreasePrice : WorldEvent
{
    private int acceptMoney = 30;
    private int acceptChance = 75;
    private int refuseChance = 90;
    private float cigarettePackPriceIncrease = 1.2f;

    public VotationIncreasePrice()
    {
        description = "Des votations sont en cours pour augmenter le prix des packs de cigarette. Nos analystes proposent du lutter en faisant des campagnes publicitaires ciblées et du lobbying politique.";

        acceptPriceDescription =
            $"-{acceptMoney} francs\n" +
            $"{acceptChance}% prix cigarettes inchangé";

        refusePriceDescription =
            $"{refuseChance}% prix cigarettes +{(100 * cigarettePackPriceIncrease) - 100}%";

        acceptPositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.CigarettePackPrice
        };
        acceptNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money
        };

        refusePositiveImpacts = new List<WorldEventImpact> { };
        refuseNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.CigarettePackPrice
        };
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);

        DoActionIfPercent(100 - acceptChance, company.MultiplyParam,
            CompanyEntity.Param.cigarettePackPrice, cigarettePackPriceIncrease);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(refuseChance, company.MultiplyParam,
            CompanyEntity.Param.cigarettePackPrice, cigarettePackPriceIncrease);
    }
}