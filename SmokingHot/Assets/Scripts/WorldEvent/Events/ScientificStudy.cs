using System;
using UnityEngine;

public class ScientificStudy : WorldEvent
{
    public ScientificStudy()
    {
        title = "ScientificStudy";
        description = "Nouvelle étude scientifique démontre les dégâts nocifs de la cigarette, il faut lutter en faisant des publicités mensongers contredisant l'étude et discrétisant les chercheurs derrière (ok: -30M et 33% de chance de ne pas changer l'image publique mais 33% de chance de le baisser drastiquement si la vérité éclate, refus: baisse de l'image publique)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.ModifyParam(CompanyEntity.Param.Money, -30);

        System.Random random = new System.Random();
        int chance = random.Next(100);

        if (chance < 33)
        {
            company.ImpactReputation(-1);
        }
        else if (chance >= 67)
        {
            company.ImpactReputation(-2);
        }
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.ImpactReputation(-1);
    }
}