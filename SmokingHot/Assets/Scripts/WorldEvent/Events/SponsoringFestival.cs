using System;
using UnityEngine;

public class SponsoringFestival : WorldEvent
{
    public SponsoringFestival()
    {
        title = "SponsoringFestival";
        description = "Parrainage de soirées de concerts de musique vendant de l'alcool pas cher, avec la volonté de faire plus fumer les personnes une fois quelques verres consommés (ok: -60M et +5M nouveaux consommateurs par an, refus: -3M nouveaux consommateurs par an)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.Money, -60);
        company.ModifyParam(CompanyEntity.Param.NewConsumers, 5);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.NewConsumers, -3);
    }
}