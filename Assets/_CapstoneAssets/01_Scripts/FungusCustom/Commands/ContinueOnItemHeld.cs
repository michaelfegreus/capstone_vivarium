using UnityEngine;
using Fungus;

[CommandInfo("Vivarium Conditionals",
             "Continue on Item Held",
             "Only continue this if the Player is holding (or not holding) a specific item in the inventory.")]

public class ContinueOnItemHeld : Command
{
    [SerializeField] protected Item targetItem;
    [Tooltip("Uncheck this if the Command should only continue when the Player is NOT holding the specific item.")]
    [SerializeField] protected bool continueOnHoldItem = true;

    protected virtual void OnEnable()
    {
        // Run this first just to check initially in case the player already has the item.
        OnInventoryChangeCheck();
        GAME_inventory_manager.InventoryChangeEvent += OnInventoryChangeCheck;
    }

    protected virtual void OnDisable()
    {
        GAME_inventory_manager.InventoryChangeEvent -= OnInventoryChangeCheck;
    }

    private void OnInventoryChangeCheck()
    {
        bool hasItem = GAME_manager.Instance.inventoryManager.CheckInventoryForItem(targetItem);

        if (continueOnHoldItem)
        {
            if (hasItem)
            {
                Continue();
            }
        }
        else
        {
            if (!hasItem)
            {
                Continue();
            }
        }
    }
}
