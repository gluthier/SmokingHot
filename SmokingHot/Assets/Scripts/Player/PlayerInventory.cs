using System.Collections.Generic;
using UnityEngine;


public enum InventoryType
{
    CIGARETTE,
    ALCOOL
}


public class PlayerInventory : MonoBehaviour
{
    private GameManager gameManager;

    public record InventoryValues
    {
        public int min { get; set; }
        public int amount { get; set; }
        public int max { get; set; }

        public InventoryValues(int minValue, int defaultValue, int maxValue)
        {
            min = minValue;
            amount = defaultValue;
            max = maxValue;
        }
    }

    // Dictionary StatType: (amount, maxAmount)
    private Dictionary<InventoryType, InventoryValues> inventory;


    public void Init(GameManager a_gameManager)
    {
        gameManager = a_gameManager;
        PlayerResetInventory();
    }

    public bool HasEnough(InventoryType type, int amount)
    {
        bool hasEnough = false;

        if (inventory.TryGetValue(type, out var inventoryValues))
        {
            hasEnough = inventoryValues.amount >= amount;
        }

        return hasEnough;
    }

    public void Increase(InventoryType inventoryType, int amount)
    {
        if (inventory.TryGetValue(inventoryType, out var statValues))
        {
            statValues.amount = Utils.AddOrMax(statValues.amount, amount, statValues.max);
        }

        _updateUI();
    }

    public void Decrease(InventoryType inventoryType, int amount)
    {
        if (inventory.TryGetValue(inventoryType, out var inventoryValues))
        {
            inventoryValues.amount = Utils.SubOrMin(inventoryValues.amount, amount, inventoryValues.min);
        }

        _updateUI();
    }

    private void PlayerResetInventory()
    {
        inventory = new()
        {
            { InventoryType.CIGARETTE,
                new InventoryValues(Env.DefaultMinNumCigarette, Env.DefaultNumCigarette, Env.DefaultMaxNumCigarette) },
            { InventoryType.ALCOOL,
                new InventoryValues(Env.DefaultMinNumAlcool, Env.DefaultNumAlcool, Env.DefaultMaxNumAlcool) }
        };

        _updateUI();
    }

    private void _updateUI()
    {
        gameManager.UpdatePlayerInventoryUIText(inventory);
    }
}
