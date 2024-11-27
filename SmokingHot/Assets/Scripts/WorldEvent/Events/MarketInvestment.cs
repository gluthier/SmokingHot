using System;
using UnityEngine;

public class MarketInvestment : WorldEvent
{
    public MarketInvestment()
    {
        title = "MarketInvestment";
        description = "Nos analystes ont décidé qu'il fallait investir dans de nouveaux marchés pour chercher de nouveaux consommateurs (ok: -100M et +20% de consommateurs et +1M acquisition nouveaux consommateurs, refus:-5% nouveaux consommateurs et -2M acquisition niveau consommateurs)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.Money, -100);
        company.ModifyParam(CompanyEntity.Param.Consumers, 1.2f);
        company.ModifyParam(CompanyEntity.Param.NewConsumers, 1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.Consumers, 0.95f);
        company.ModifyParam(CompanyEntity.Param.NewConsumers, -2);
    }
}