using System;
using UnityEngine;

public class VotationIncreasePrice : WorldEvent
{
    public VotationIncreasePrice()
    {
        title = "VotationIncreasePrice";
        description = "Votation pour faire augmenter le prix du packet de cigarette de 20% par volonté de prévention, il faut lutter avec du lobbying (ok: -30M et 75% de chance de garder le même prix du packet, refus: 95% de chance d'augmentation du prix)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, 30);

        DoActionIfPercent(25, company.MultiplyParam,
            CompanyEntity.Param.cigarettePackPrice, 1.2f);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(95, company.MultiplyParam,
            CompanyEntity.Param.cigarettePackPrice, 1.2f);
    }
}