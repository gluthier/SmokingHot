using System;
using System.Collections.Generic;
using UnityEngine;

public class VotationAbolishAds : WorldEvent
{
    private int acceptMoney = 10;
    private int acceptChance = 75;
    private int refuseChance = 90;

    public VotationAbolishAds()
    {
        title = "Interdiction des publicit�s";
        description = "Des votations sont en cours pour interdire les publicit�s sur le tabac. Nos analystes proposent du lutter en faisant du lobbying politique.\n\n" +
            $"<b>Accepter</b>: Co�te {acceptMoney} M, {acceptChance}% de r�ussite de s'opposer au changement\n" +
            $"<b>Refuser</b>: {refuseChance}% de chance que les publicit�s soient interdites";
        
        acceptPositiveImpacts = new List<WorldEventImpact> {
            WorldEventImpact.AdCampaignExistence
        };
        acceptNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.Money
        };

        refusePositiveImpacts = new List<WorldEventImpact> { };
        refuseNegativeImpacts = new List<WorldEventImpact> {
            WorldEventImpact.AdCampaignExistence
        };
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