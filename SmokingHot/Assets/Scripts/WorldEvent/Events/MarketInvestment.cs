using System;
using System.Collections.Generic;
using UnityEngine;

public class MarketInvestment : WorldEvent
{
    private int acceptMoney = 80;
    private float acceptConsumersIncrease = 1.2f;
    private int acceptNewConsumers = 10;
    private int refuseBonusMoney = 30;
    private int refuseNewConsumers = 5;

    public MarketInvestment()
    {
        description = "Nos analystes ont décidé qu'il fallait investir dans de nouveaux marchés pour aller chercher de nouveaux consommateurs.";

        acceptPriceDescription =
            Env.ColorizeNegativeText($"-{acceptMoney} millions de francs\n") +
            Env.ColorizePositiveText($"+{(100 * acceptConsumersIncrease) - 100}% de consommateurs\n") +
            Env.ColorizePositiveText($"+{acceptNewConsumers} millions de nouveaux consommateurs annuels");

        refusePriceDescription =
            Env.ColorizeNegativeText($"-{refuseBonusMoney} millions de francs annuels\n") +
            Env.ColorizeNegativeText($"-{refuseNewConsumers} millions de nouveaux consommateurs annuels");

        acceptPositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Consumers,
            WorldEventImpact.NewConsumers
        };
        acceptNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money
        };

        refusePositiveImpacts = new List<WorldEventImpact> { };
        refuseNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.BonusMoney,
            WorldEventImpact.NewConsumers
        };
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);
        company.MultiplyParam(CompanyEntity.Param.Consumers, acceptConsumersIncrease);
        company.IncreaseParam(CompanyEntity.Param.NewConsumers, acceptNewConsumers);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.BonusMoney, refuseBonusMoney);
        company.DecreaseParam(CompanyEntity.Param.NewConsumers, refuseNewConsumers);
    }
}