using System;
using UnityEngine;

public class PopStarDied : WorldEvent
{
    public PopStarDied()
    {
        title = "PopStarDied";
        description = "La plus grande pop-star internationale est décédée suite à des problèmes de santé lié à la consommation de cigarettes, il faut lutter avec des publicités pour améliorer l'image du publique (ok: -40M et 5% de chance d'augmenter l'image publique et 60% de chance de garder l'image publique actuelle, refus: 95% de chance de baisser l'image publique)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, 40);

        DoActionIfPercentElse(5, company.ImpactReputation, 1,
            60, company.ImpactReputation, -1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(95, company.ImpactReputation, -1);
    }
}