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
        title = "Augmentation des prix des cigarettes";

        description = "Des votations sont en cours pour augmenter le prix des packs de cigarette. Nos analystes proposent du lutter en faisant des campagnes publicitaires cibl�es et du lobbying politique.";

        acceptPriceDescription =
            $"Co�te {acceptMoney} M\n" +
            $"{acceptChance}% de r�ussite de s'opposer au changement";

        refusePriceDescription =
            $"{refuseChance}% que le prix des packs de cigarettes soit augment� de {100 * cigarettePackPriceIncrease}%";

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