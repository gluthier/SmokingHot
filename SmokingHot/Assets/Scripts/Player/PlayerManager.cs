using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;

    private PlayerController controller;
    private PlayerStats stats;
    private PlayerInventory inventory;

    private GameObject playerBulletPrefab;

    private int numCigaretteConsumedInThisRoom;
    private int numAlcoolConsumedInThisRoom;

    public void Init(GameManager a_gameManager)
    {
        gameManager = a_gameManager;

        controller = gameObject.GetComponent<PlayerController>();
        controller.Init(a_gameManager, this);

        stats = gameObject.GetComponent<PlayerStats>();
        stats.Init(a_gameManager);

        inventory = gameObject.GetComponent<PlayerInventory>();
        inventory.Init(a_gameManager);

        playerBulletPrefab = Resources.Load(Env.PlayerBulletPrefab) as GameObject;

        ResetPlayerConsumption();
    }

    public void PlayerAttack()
    {
        GameObject bullet = Instantiate(playerBulletPrefab, transform.position, transform.rotation, gameManager.GetBulletHolder());

        bullet.GetComponent<PlayerBulletManager>()
            .SetDamage(stats.GetAmount(StatType.ATTACK_DAMAGE));
    }

    public void PlayerIsHit(int damage)
    {
        stats.HitPlayer(damage);
    }

    public void ConsumeCigarette(int amount)
    {
        if (inventory.HasEnough(InventoryType.CIGARETTE, amount))
        {
            inventory.Decrease(InventoryType.CIGARETTE, amount);
            numCigaretteConsumedInThisRoom += amount;

            // Double the effects if the player has consumed alcool in this room
            int stressAmount = Env.CigaretteStressReliever;
            if (numAlcoolConsumedInThisRoom > 0)
                stressAmount += Env.CigaretteStressReliever;

            int healthAmount = Env.CigaretteHealthReliever;
            if (numAlcoolConsumedInThisRoom > 0)
                healthAmount += Env.CigaretteHealthReliever;

            stats.Decrease(StatType.STRESS, stressAmount);
            stats.Increase(StatType.HEALTH, healthAmount);
            stats.Increase(StatType.ATTACK_SPEED, Env.CigaretteAttackSpeedIncrease);
            stats.Increase(StatType.CIGARETTE_ADDICTION, numCigaretteConsumedInThisRoom);
        }
    }

    public void ConsumeAlcool(int amount)
    {
        if (inventory.HasEnough(InventoryType.ALCOOL, amount))
        {
            inventory.Decrease(InventoryType.ALCOOL, amount);
            numAlcoolConsumedInThisRoom += amount;

            stats.Decrease(StatType.STRESS, Env.AlcoolStressReliever);
            stats.Increase(StatType.HEALTH, Env.AlcoolHealthReliever);
            stats.Increase(StatType.ATTACK_SPEED, Env.AlcoolAttackDamageIncrease);
            stats.Increase(StatType.CIGARETTE_ADDICTION, numAlcoolConsumedInThisRoom);
        }
    }

    public void GetAlcool()
    {
        inventory.Increase(InventoryType.ALCOOL, 1);
    }

    public void StartRun()
    {
        stats.Increase(StatType.STRESS, Env.StartRunStressIncrement);
    }

    public void EndRun()
    {
        stats.UpdateStatsEndRun();
    }

    public void EnterRoom()
    {
        ResetPlayerConsumption();

        if (stats.GetAmount(StatType.CIGARETTE_ADDICTION) > Env.EnterRoomAutomaticCigaretteTrigger)
            ConsumeCigarette(1);

        if (stats.GetAmount(StatType.ALCOOL_ADDICTION) > Env.EnterRoomAutomaticAlcoolTrigger)
            ConsumeAlcool(1);
    }

    public void ExitRoom()
    {
        if (numCigaretteConsumedInThisRoom == 0)
            stats.Decrease(StatType.CIGARETTE_ADDICTION, Env.ExitRoomCigaretteAddictionDecrease);

        if (numAlcoolConsumedInThisRoom == 0)
            stats.Decrease(StatType.ALCOOL_ADDICTION, Env.ExitRoomAlcoolAddictionDecrease);

        if (stats.GetAmount(StatType.CIGARETTE_ADDICTION) == 0)
            stats.Increase(StatType.ATTACK_SPEED, Env.ExitRoomAttackSpeedIncrease);

        if (stats.GetAmount(StatType.ALCOOL_ADDICTION) == 0)
            stats.Increase(StatType.ATTACK_DAMAGE, Env.ExitRoomAttackDamageIncrease);
    }


    private void ResetPlayerConsumption()
    {
        numCigaretteConsumedInThisRoom = 0;
        numAlcoolConsumedInThisRoom = 0;
    }
}
