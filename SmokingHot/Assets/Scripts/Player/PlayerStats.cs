using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;


public enum StatType
{
    HEALTH,
    STRESS,
    CIGARETTE_ADDICTION,
    ALCOOL_ADDICTION,
    ATTACK_SPEED,
    ATTACK_DAMAGE,
}


public class PlayerStats : MonoBehaviour
{
    private GameManager gameManager;

    public record StatValues
    {
        public int min { get; set; }
        public int amount { get; set; }
        public int max { get; set; }

        public StatValues(int minValue, int defaultValue, int maxValue)
        {
            min = minValue;
            amount = defaultValue;
            max = maxValue;
        }
    }

    // Dictionary StatType: (amount, maxAmount)
    private Dictionary<StatType, StatValues> stats;


    public void Init(GameManager a_gameManager)
    {
        gameManager = a_gameManager;
        stats = new Dictionary<StatType, StatValues>();
        PlayerResetStats();
    }

    public void UpdateStatsEndRun()
    {
        if (stats.TryGetValue(StatType.CIGARETTE_ADDICTION, out var cigAddictionValues))
        {
            if (cigAddictionValues.amount > Env.EndRunCigaretteAddictionHpLossTriggerValue)
                DecreaseMaxHealth(Env.EndRunHpLossCigaretteAddiction);
        }

        if (stats.TryGetValue(StatType.ALCOOL_ADDICTION, out var alcAddictionValues))
        {
            if (alcAddictionValues.amount > Env.EndRunAlcoolAddictionHpLossTriggerValue)
                DecreaseMaxHealth(Env.EndRunHpLossAlcoolAddiction);
        }

        if (stats.TryGetValue(StatType.HEALTH, out var healthValues))
            healthValues.amount = healthValues.max;

        if (stats.TryGetValue(StatType.ATTACK_SPEED, out var attackSpeedValues))
            attackSpeedValues.amount = Env.DefaultAttackSpeed;

        if (stats.TryGetValue(StatType.ATTACK_DAMAGE, out var attackDamageValues))
            attackDamageValues.amount = Env.DefaultAttackDamage;

        _updateUI();
    }

    public void HitPlayer(int damage)
    {
        Increase(StatType.STRESS, Env.PlayerIsHitStressIncrement);
        Decrease(StatType.HEALTH, damage);

        _updateUI();
    }

    public void Increase(StatType statType, int amount)
    {
        if (stats.TryGetValue(statType, out var statValues))
        {
            DecreaseHealthIfAddictionValueIsOverMax(statType, statValues, amount);

            statValues.amount = Utils.AddOrMax(statValues.amount, amount, statValues.max);
        }

        _updateUI();
    }

    public void Decrease(StatType statType, int amount)
    {
        if (stats.TryGetValue(statType, out var statValues))
        {
            statValues.amount = Utils.SubOrMin(statValues.amount, amount, statValues.min);
        }

        _updateUI();
    }

    public int GetAmount(StatType statType)
    {
        int amount = -1;

        if (stats.TryGetValue(statType, out var statValues))
        {
            amount = statValues.amount;
        }

        return amount;
    }

    public void DecreaseMaxHealth(int amount)
    {
        if (stats.TryGetValue(StatType.HEALTH, out var healthValues))
        {
            healthValues.max = Utils.SubOrMin(healthValues.max, amount, Env.DefaultMinMaxHealth);
        }

        _updateUI();
    }

    private void DecreaseHealthIfAddictionValueIsOverMax(StatType statType, StatValues statValues, int amount)
    {
        if (statType == StatType.CIGARETTE_ADDICTION ||
            statType == StatType.ALCOOL_ADDICTION)
        {
            int potentialOverAmount = statValues.amount + amount;

            if (potentialOverAmount > statValues.max)
            {
                // Each addiction point over max limit results in -1 HP per point
                Decrease(StatType.HEALTH, statValues.max - potentialOverAmount);
            }
        }
    }

    private void PlayerResetStats()
    {
        stats = new()
        {
            { StatType.HEALTH,
                new StatValues(Env.DefaultMinHealth, Env.DefaultHealth, Env.DefaultMaxHealth) },

            { StatType.STRESS,
                new StatValues(Env.DefaultMinStress, Env.DefaultStress, Env.DefaultMaxStress) },

            { StatType.CIGARETTE_ADDICTION,
                new StatValues(Env.DefaultMinCigAddiction, Env.DefaultCigAddiction, Env.DefaultMaxCigAddiction) },

            { StatType.ALCOOL_ADDICTION,
                new StatValues(Env.DefaultMinAlcAddiction, Env.DefaultAlcAddiction, Env.DefaultMaxAlcAddiction) },

            { StatType.ATTACK_SPEED,
                new StatValues(Env.DefaultMinAttackSpeed, Env.DefaultAttackSpeed, Env.DefaultMaxAttackSpeed) },

            { StatType.ATTACK_DAMAGE,
                new StatValues(Env.DefaultMinAttackDamage, Env.DefaultAttackDamage, Env.DefaultMaxAttackDamage) }
        };

        _updateUI();
    }

    private void _updateUI()
    {
        gameManager.UpdatePlayerStatsUIText(stats);
    }
}
