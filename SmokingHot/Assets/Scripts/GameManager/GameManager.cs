using System.Collections.Generic;
using UnityEngine;

using static PlayerInventory;
using static PlayerStats;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    private UIManager uiManager;

    private GameObject enemyPrefab;

    private Transform bulletHolder;
    private Transform enemyHolder;

    void Start()
    {
        if (playerManager == null)
            playerManager = FindAnyObjectByType<PlayerManager>();

        if (uiManager == null)
            uiManager = FindAnyObjectByType<UIManager>();

        playerManager.Init(this);
        uiManager.Init(this);

        enemyPrefab = Resources.Load(Env.EnemyPrefab) as GameObject;

        bulletHolder = transform.Find(Env.BulletHolder);
        enemyHolder = transform.Find(Env.EnemyHolder);

        InstantiateEnemy();
    }

    public Transform GetBulletHolder()
    {
        return bulletHolder;
    }

    public void UpdatePlayerStatsUIText(Dictionary<StatType, StatValues> stats)
    {
        stats.TryGetValue(StatType.HEALTH, out var healthValues);
        stats.TryGetValue(StatType.STRESS, out var stressValues);
        stats.TryGetValue(StatType.CIGARETTE_ADDICTION, out var cigAddictionValues);
        stats.TryGetValue(StatType.ALCOOL_ADDICTION, out var alcAddictionValues);

        uiManager.UpdateStatsText(healthValues.amount, healthValues.max,
            stressValues.amount, stressValues.max,
            cigAddictionValues.amount, cigAddictionValues.max,
            alcAddictionValues.min, alcAddictionValues.max);
    }

    public void UpdatePlayerInventoryUIText(Dictionary<InventoryType, InventoryValues> inventory)
    {
        inventory.TryGetValue(InventoryType.CIGARETTE, out var cigaretteValues);
        inventory.TryGetValue(InventoryType.ALCOOL, out var alcoolValues);

        uiManager.UpdateInventoryText(cigaretteValues.amount, alcoolValues.amount);
    }

    private void InstantiateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, Env.DefaultEnemySpawnPosition, Quaternion.Euler(0, -90, 0), enemyHolder);

        enemy.GetComponent<EnemyManager>().Init(this);
    }
}
