using System;
using UnityEngine;

public class FineToxicity : WorldEvent
{
    public FineToxicity()
    {
        title = "FineToxicity";
        description = "Amendes car des �tudes ont �t� men�es qui d�montres le niveau exag�r�ment de toxicit� des cigarettes (ok: -25M et 90% de chance de baisser l'image publique, refus: on lutte juridiquement: -15M et 33% de chance de ne pas changer l'image publique mais 33% de chance de baisser drastiquement l'image publique)";
    }

    public override void AcceptEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, 25);

        DoActionIfPercent(90, company.ImpactReputation, -1);
    }

    public override void RefuseEvent(CompanyEntity company)
    {
        company.DecreaseParam(CompanyEntity.Param.Money, 15);

        DoActionIfPercentElse(33, company.ImpactReputation, -1,
            33, company.ImpactReputation, -2);
    }
}