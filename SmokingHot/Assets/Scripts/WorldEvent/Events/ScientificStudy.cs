using System;
using System.Collections.Generic;
using UnityEngine;

public class ScientificStudy : WorldEvent
{
    private int acceptMoney = 10;
    private int acceptChanceGood = 75;
    private int acceptChanceBad = 75;

    public ScientificStudy()
    {
        title = "Rapport scientifique";
        description = "Une nouvelle �tude scientifique d�montre les d�g�ts nocifs de la cigarette sur la sant�. Nos analystes proposent de discr�tiser l'�tude et l'�quipe de scientifiques derri�re.\n\n" +
            $"<b>Accepter</b>: Co�te {acceptMoney} M, {acceptChanceGood}% d'augmenter l'image publique, mais {acceptChanceBad}% de r�duire fortement l'image publique\n" +
            $"<b>Refuser</b>: R�duit l'image publique";

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
            acceptChanceBad, company.ImpactPopularity, -2);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ImpactPopularity(-1);
    }
}