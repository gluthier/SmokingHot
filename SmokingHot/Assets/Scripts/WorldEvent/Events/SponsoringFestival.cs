using System;
using UnityEngine;

public class SponsoringFestival : WorldEvent
{
    public SponsoringFestival()
    {
        title = "SponsoringFestival";
        description = "Parrainage de soir�es de concerts de musique vendant de l'alcool pas cher, avec la volont� de faire plus fumer les personnes une fois quelques verres consomm�s (ok: -20M et +4M nouveaux consommateurs par an, refus: -3M nouveaux consommateurs par an)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, 20);
        company.IncreaseParam(CompanyEntity.Param.NewConsumers, 4);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.NewConsumers, 3);
    }
}