using System;
using System.Collections.Generic;
using UnityEngine;

public class VotationIncreasePrice : WorldEvent
{
    private int acceptMoney = 30;
    private int acceptChance = 75;
    private int refuseChance = 90;
    private float cigarettePackPriceDecease = 1.2f;

    public VotationIncreasePrice()
    {
        description = "Des votations sont en cours pour augmenter la taxe sur le prix des packs de cigarette. Nos analystes proposent du lutter en faisant des campagnes publicitaires ciblées et du lobbying politique.";

        acceptPriceDescription =
            Env.ColorizeNegativeText($"-{acceptMoney} millions de francs\n") +
            Env.ColorizePositiveText($"{acceptChance}% taxe cigarettes inchangé");

        refusePriceDescription =
            Env.ColorizeNegativeText($"{refuseChance}% taxe cigarettes +{(100 * cigarettePackPriceDecease) - 100}%");

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

        DoActionIfPercent(100 - acceptChance, company.DivideParam,
            CompanyEntity.Param.cigarettePackPrice, cigarettePackPriceDecease);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(refuseChance, company.DivideParam,
            CompanyEntity.Param.cigarettePackPrice, cigarettePackPriceDecease);
    }
}