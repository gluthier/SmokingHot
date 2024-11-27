using System;
using UnityEngine;

public class AbolishAds : WorldEvent
{
    public AbolishAds()
    {
        title = "AbolishAds";
        description = "Interdiction de faire des publicités sur le tabac, il faut lutter avec du lobbying (ok: -10M et 75% de chance de continuer les publicités, refus. 95% de chance d'interdiction des pubs)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, 10);

        DoActionIfPercent(25, company.AbolishAds);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        DoActionIfPercent(95, company.AbolishAds);
    }
}