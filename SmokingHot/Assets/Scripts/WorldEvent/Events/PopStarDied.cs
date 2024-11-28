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
        title = "Reine de la pop d�c�d�e";
        description = "La plus grande pop-star internationale est d�c�d�e suite � des probl�mes de sant� li� � la consommation de cigarettes. La r�action du publique est devastatrice. Nos analystes proposent de faire une campagnes publicitaires montrant nos efforts pour r�duire les d�g�ts du tabac sur la sant�.\n\n" +
            $"<b>Accepter</b>: Co�te {acceptMoney} M, {acceptChanceGood}% de r�ussite d'augmenter l'image publique, mais {acceptChanceBad}% de la r�duire\n" +
            $"<b>Refuser</b>: {refuseChance}% de r�duire l'image publique";
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