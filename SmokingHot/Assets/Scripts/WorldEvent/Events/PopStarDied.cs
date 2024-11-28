using System;
using UnityEngine;

public class PopStarDied : WorldEvent
{
    private int acceptMoney = 20;
    private int acceptChanceGood = 10;
    private int acceptChanceBad = 40;
    private int refuseChance = 80;

    public PopStarDied()
    {
        title = "Reine de la pop décédée";
        description = "La plus grande pop-star internationale est décédée suite à des problèmes de santé lié à la consommation de cigarettes. La réaction du publique est devastatrice. Nos analystes proposent de faire une campagnes publicitaires montrant nos efforts pour réduire les dégâts du tabac sur la santé.\n\n" +
            $"<b>Accepter</b>: Coûte {acceptMoney} M, {acceptChanceGood}% de réussite d'augmenter l'image publique, mais {acceptChanceBad}% de la réduire\n" +
            $"<b>Refuser</b>: {refuseChance}% de réduire l'image publique";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);

        DoActionIfPercentElse(acceptChanceGood, company.ImpactReputation, 1,
            acceptChanceBad, company.ImpactReputation, -1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(refuseChance, company.ImpactReputation, -1);
    }
}