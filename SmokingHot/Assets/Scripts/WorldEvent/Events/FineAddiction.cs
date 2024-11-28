using System;
using UnityEngine;

public class FineAddiction : WorldEvent
{
    private int acceptMoney = 15;
    private int acceptChance = 75;
    private int refuseMoney = 20;
    private int refuseChanceGood = 33;
    private int refuseChanceBad = 33;

    public FineAddiction()
    {
        title = "Amende";
        description = "Nous avons �t� amend� � cause du niveau d'addiciton trop �lev� de nos cigarettes. Nos analystes proposent de refuser en luttant juridiquement contre, risquant le quitte ou double.\n\n" +
            $"<b>Accepter</b>: Co�te {acceptMoney} M, {acceptChance}% de r�duire l'image publique\n" +
            $"<b>Refuser</b>: Co�te {refuseMoney} M, {refuseChanceGood}% d'augmenter l'image publique, mais {refuseChanceBad}% de r�duire fortement l'image publique";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);

        DoActionIfPercent(acceptChance, company.ImpactReputation, -1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, refuseMoney);

        DoActionIfPercentElse(refuseChanceGood, company.ImpactReputation, 1,
            refuseChanceBad, company.ImpactReputation, -2);
    }
}