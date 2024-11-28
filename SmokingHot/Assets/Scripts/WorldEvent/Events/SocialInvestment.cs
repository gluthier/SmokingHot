using System;
using UnityEngine;

public class SocialInvestment : WorldEvent
{
    private int acceptMoney = 10;
    private int refuseChance = 33;

    public SocialInvestment()
    {
        title = "Investissement dans le social";
        description = "Nos analystes ont décidé qu'il faisait investir dans un événements caritatifs pour améliorer l'image publique en promouvant une image d'entreprise socialement responsable.\n\n" +
            $"<b>Accepter</b>: Coûte {acceptMoney} M, augmenter l'image publique\n" +
            $"<b>Refuser</b>: {refuseChance}% de réduire l'image publique";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);
        company.ImpactReputation(1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(refuseChance, company.ImpactReputation, -1);
    }
}