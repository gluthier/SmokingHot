using System;
using UnityEngine;

public class VotationAbolishAds : WorldEvent
{
    private int acceptMoney = 10;
    private int acceptChance = 75;
    private int refuseChance = 90;

    public VotationAbolishAds()
    {
        title = "Interdiction des publicités";
        description = "Des votations sont en cours pour interdire les publicités sur le tabac. Nos analystes proposent du lutter en faisant du lobbying politique.\n\n" +
            $"<b>Accepter</b>: Coûte {acceptMoney} M, {acceptChance}% de réussite de s'opposer au changement\n" +
            $"<b>Refuser</b>: {refuseChance}% de chance que les publicités soient interdites";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, acceptMoney);

        DoActionIfPercent(100 - acceptChance, company.AbolishAds);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(refuseChance, company.AbolishAds);
    }
}