using System;
using UnityEngine;

public class AbolishAds : WorldEvent
{
    public AbolishAds()
    {
        title = "AbolishAds";
        description = "Interdiction de faire des publicités sur le tabac, il faut lutter avec du lobbying (ok: -50M et 75% de chance de continuer les publicités, refus. 95% de chance d'interdiction des pubs)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.Money, -50);

        System.Random random = new System.Random();
        int chance = random.Next(100);

        if (chance < 25)
        {
            company.AbolishAds();
        }
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);

        if (chance < 95)
        {
            company.AbolishAds();
        }
    }
}