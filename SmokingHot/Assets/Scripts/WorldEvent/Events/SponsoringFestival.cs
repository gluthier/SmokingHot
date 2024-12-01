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
        title = "Parrainage d'un festival";
        description = "Nos analystes proposent de parrainer un festival de concerts de musique pour permettre de vendre nos cigarettes aux festivaliers.\n\n" +
            $"<b>Accepter</b>: Coûte {acceptMoney} M, augmente de {acceptNewConsumers} M les nouveaux consommateurs\n" +
            $"<b>Refuser</b>: réduit de {refuseNewConsumers} M les nouveaux consommateurs";

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