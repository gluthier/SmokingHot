using System;
using System.Collections.Generic;
using System.Linq;

public abstract class WorldEvent
{
    public enum WorldEventImpact
    {
        Money,
        Popularity,
        Consumers,
        ManufacturingCosts,
        LobbyingCosts,
        AdCampaignExistence,
        AdCampaignsCosts,
        CigarettePackProducedToxicity,
        CigarettePackProducedAddiction,
        CigarettePackPrice,
        NewConsumers,
        LostConsumers,
        DeadConsumers,
        BonusMoney
    }

    public List<WorldEventImpact> acceptPositiveImpacts = new();
    public List<WorldEventImpact> acceptNegativeImpacts = new();
    public List<WorldEventImpact> refusePositiveImpacts = new();
    public List<WorldEventImpact> refuseNegativeImpacts = new();

    public string title;
    public string description;
    public string acceptPriceDescription;
    public string refusePriceDescription;

    public abstract void AcceptEvent(CompanyEntity company);

    public abstract void RefuseEvent(CompanyEntity company);

    public void HandleEventBasedOnInterests(CompanyEntity iaCompany, params WorldEventImpact[] interestedInEvents)
    {
        if (IsAcceptPositiveImpactedBy(interestedInEvents))
        {
            AcceptEvent(iaCompany);
        }
        else if (IsRefusePositiveImpactedBy(interestedInEvents))
        {
            RefuseEvent(iaCompany);
        }
        else if (IsAcceptNegativeImpactedBy(interestedInEvents))
        {
            RefuseEvent(iaCompany);
        }
        else if (IsRefuseNegativeImpactedBy(interestedInEvents))
        {
            AcceptEvent(iaCompany);
        }
        else
        {
            // Refuse by default
            RefuseEvent(iaCompany);
        }
    }

    private bool IsAcceptPositiveImpactedBy(params WorldEventImpact[] worldEvents)
    {
        return acceptPositiveImpacts.Any(x => worldEvents.Contains(x));
    }

    private bool IsAcceptNegativeImpactedBy(params WorldEventImpact[] worldEvents)
    {
        return acceptNegativeImpacts.Any(x => worldEvents.Contains(x));
    }

    private bool IsRefusePositiveImpactedBy(params WorldEventImpact[] worldEvents)
    {
        return refusePositiveImpacts.Any(x => worldEvents.Contains(x));
    }

    private bool IsRefuseNegativeImpactedBy(params WorldEventImpact[] worldEvents)
    {
        return refuseNegativeImpacts.Any(x => worldEvents.Contains(x));
    }

    public delegate void ConsequenceAction();
    public static void DoActionIfPercent(int percentage, ConsequenceAction action)
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);

        if (chance < percentage)
        {
            action();
        }
    }

    public static void DoActionIfPercentElse(int percentageFirst, ConsequenceAction actionFirst,
        int percentageSecond, ConsequenceAction actionSecond)
    {
        System.Random random = new System.Random();
        int chanceFirst = random.Next(100);
        int chanceSecond = random.Next(100);

        if (chanceFirst < percentageFirst)
        {
            actionFirst();
        }
        else if (chanceSecond < percentageSecond)
        {
            actionSecond();
        }
    }

    public delegate void ConsequenceActionWithParams(CompanyEntity.Param param, float amount);
    public static void DoActionIfPercent(int percentage, ConsequenceActionWithParams action, CompanyEntity.Param param, float amount)
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);

        if (chance < percentage)
        {
            action(param, amount);
        }
    }

    public static void DoActionIfPercentElse(int percentageFirst, ConsequenceActionWithParams actionFirst, CompanyEntity.Param paramFirst, float amountFirst,
        int percentageSecond, ConsequenceActionWithParams actionSecond, CompanyEntity.Param paramSecond, float amountSecond)
    {
        System.Random random = new System.Random();
        int chanceFirst = random.Next(100);
        int chanceSecond = random.Next(100);

        if (chanceFirst < percentageFirst)
        {
            actionFirst(paramFirst, amountFirst);
        }
        else if (chanceSecond < percentageSecond)
        {
            actionSecond(paramSecond, amountSecond);
        }
    }

    public delegate void ConsequenceActionWithParam(int amount);
    public static void DoActionIfPercent(int percentage, ConsequenceActionWithParam action, int amount)
    {
        System.Random random = new System.Random();
        int chance = random.Next(100);

        if (chance < percentage)
        {
            action(amount);
        }
    }

    public static void DoActionIfPercentElse(int percentageFirst, ConsequenceActionWithParam actionFirst, int amountFirst,
        int percentageSecond, ConsequenceActionWithParam actionSecond, int amountSecond)
    {
        System.Random random = new System.Random();
        int chanceFirst = random.Next(100);
        int chanceSecond = random.Next(100);

        if (chanceFirst < percentageFirst)
        {
            actionFirst(amountFirst);
        }
        else if (chanceSecond < percentageSecond)
        {
            actionSecond(amountSecond);
        }
    }
}