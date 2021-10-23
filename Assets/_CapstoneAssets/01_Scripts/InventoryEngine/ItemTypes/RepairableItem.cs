using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;

namespace MoreMountains.InventoryEngine
{
    [CreateAssetMenu(fileName = "RepairableItem", menuName = "Vivarium/InventoryEngine/Repairable Item")]
    [Serializable]
    /// <summary>
    /// An item that can be repaired and turned into something useful at the work bench in the Workshop.
    /// Generally I envision these being key items, but maybe there could be some that repair and turn into general items.
    /// </summary>
    public class RepairableItem : InventoryItem
    {
        // The constructor sets the target inventory.
        public RepairableItem()
        {
            TargetInventoryName = "KeyItemInventory";
            ItemClass = ItemClasses.KeyItem;

        }

        [Header("Repair")]

        [Tooltip("The player needs to supply the following items at the crafting bench to repair this.")]
        public InventoryItem[] repairIngredients;
        [Tooltip("What this item turns into when repaired.")]
        public InventoryItem[] repairedItemResult;

    }
}