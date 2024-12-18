using System;
using System.Collections.Generic;
using UnityEngine;

public class SponsoringFestival : WorldEvent
{
    private int acceptMoney = 20;
    private int acceptNewConsumers = 4;
    private int refuseNewConsumers = 1;

    public SponsoringFestival()
    {
        description = "Nos analystes proposent de parrainer un festival de concerts de musique pour permettre de vendre nos cigarettes aux festivaliers.";

        acceptPriceDescription =
            $"-{acceptMoney} millions de francs\n" +
            $"+{acceptNewConsumers} millions de nouveaux consommateurs annuels";

        refusePriceDescription =
            $"-{refuseNewConsumers} millions de nouveaux consommateurs annuels";

        acceptPositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.NewConsumers
        };
        acceptNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money
        };

        refusePositiveImpacts = new List<WorldEventImpact> { };
        refuseNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.NewConsumers
        };
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);
        company.IncreaseParam(CompanyEntity.Param.NewConsumers, acceptNewConsumers);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.NewConsumers, refuseNewConsumers);
    }
}