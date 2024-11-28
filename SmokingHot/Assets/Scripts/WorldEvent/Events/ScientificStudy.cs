using System;
using UnityEngine;

public class ScientificStudy : WorldEvent
{
    private int acceptMoney = 10;
    private int acceptChanceGood = 75;
    private int acceptChanceBad = 75;

    public ScientificStudy()
    {
        title = "Rapport scientifique";
        description = "Une nouvelle étude scientifique démontre les dégâts nocifs de la cigarette sur la santé. Nos analystes proposent de discrétiser l'étude et l'équipe de scientifiques derrière.\n\n" +
            $"<b>Accepter</b>: Coûte {acceptMoney} M, {acceptChanceGood}% d'augmenter l'image publique, mais {acceptChanceBad}% de réduire fortement l'image publique\n" +
            $"<b>Refuser</b>: Réduit l'image publique";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);

        DoActionIfPercentElse(acceptChanceGood, company.ImpactReputation, 1,
            acceptChanceBad, company.ImpactReputation, -2);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ImpactReputation(-1);
    }
}