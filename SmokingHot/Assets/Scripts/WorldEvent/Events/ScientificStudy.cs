using System;
using UnityEngine;

public class ScientificStudy : WorldEvent
{
    public ScientificStudy()
    {
        title = "ScientificStudy";
        description = "Nouvelle �tude scientifique d�montre les d�g�ts nocifs de la cigarette, il faut lutter en faisant des publicit�s mensongers contredisant l'�tude et discr�tisant les chercheurs derri�re (ok: -30M et 33% de chance de ne pas changer l'image publique mais 33% de chance de le baisser drastiquement si la v�rit� �clate, refus: baisse de l'image publique)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, 30);

        DoActionIfPercentElse(33, company.ImpactReputation, -1,
            33, company.ImpactReputation, -2);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ImpactReputation(-1);
    }
}