using System;
using System.Collections.Generic;
using UnityEngine;

public class InvestSnacks : WorldEvent
{
    private int acceptMoney = 60;
    private int acceptNewConsumers = 4;
    private int refuseBonusMoney = 3;

    public InvestSnacks()
    {
        description = "Une proposition de rachat d'entreprise agroalimentaire spécialisée dans les snacks addictifs. Nos analystes pensent que cela pourrait nous apporter de nouveaux clients sur le long terme. Refuser rendrait fâchés quelques actionnaires.";

        acceptPriceDescription =
            $"-{acceptMoney} millions de francs\n" +
            $"+{acceptNewConsumers} millions de nouveaux consommateurs annuels";

        refusePriceDescription =
            $"+{refuseBonusMoney} millions de francs annuels";

        acceptPositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.NewConsumers
        };
        acceptNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money
        };

        refusePositiveImpacts = new List<WorldEventImpact> { };
        refuseNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.BonusMoney
        };
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);
        company.IncreaseParam(CompanyEntity.Param.NewConsumers, acceptNewConsumers);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.BonusMoney, refuseBonusMoney);
    }
}