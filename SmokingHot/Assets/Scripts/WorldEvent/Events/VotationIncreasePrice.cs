using System;
using UnityEngine;

public class VotationIncreasePrice : WorldEvent
{
    private int acceptMoney = 30;
    private int acceptChance = 75;
    private int refuseChance = 90;
    private float cigarettePackPriceIncrease = 1.2f;

    public VotationIncreasePrice()
    {
        title = "Augmentation des prix des cigarettes";
        description = "Des votations sont en cours pour augmenter le prix des packs de cigarette. Nos analystes proposent du lutter en faisant des campagnes publicitaires ciblées et du lobbying politique.\n\n" +
            $"<b>Accepter</b>: Coûte {acceptMoney} M, {acceptChance}% de réussite de s'opposer au changement\n" +
            $"<b>Refuser</b>: {refuseChance}% que le prix des packs de cigarettes soit augmenté de {100*cigarettePackPriceIncrease}%";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);

        DoActionIfPercent(100 - acceptChance, company.MultiplyParam,
            CompanyEntity.Param.cigarettePackPrice, cigarettePackPriceIncrease);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(refuseChance, company.MultiplyParam,
            CompanyEntity.Param.cigarettePackPrice, cigarettePackPriceIncrease);
    }
}