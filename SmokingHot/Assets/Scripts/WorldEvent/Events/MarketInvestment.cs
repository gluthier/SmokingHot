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
        title = "Investissement dans de nouveaux march�s";
        description = "Nos analystes ont d�cid� qu'il fallait investir dans de nouveaux march�s pour aller chercher de nouveaux consommateurs.\n\n" +
            $"<b>Accepter</b>: Co�te {acceptMoney} M, augmente de {100* acceptConsumersIncrease}% le nombre de consomateurs actuels, augment de {acceptNewConsumers} M les nouveaux consommateurs\n" +
            $"<b>Refuser</b>: r�duit de {refuseBonusMoney} M les gains bonus annuels, r�duit de {refuseNewConsumers} M les nouveaux cconsommateurs";

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