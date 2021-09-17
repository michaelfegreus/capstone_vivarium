using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;

namespace MoreMountains.InventoryEngine
{
    [CreateAssetMenu(fileName = "ToolItem", menuName = "Vivarium/InventoryEngine/Tool Item")]
    [Serializable]
    /// <summary>
    /// Tools are stored in the Workshop. One can be equipped at a time and brought with you, similar to Harvest Moon.
    /// </summary>
    public class ToolItem : InventoryItem
    {
        // The constructor sets the target inventory.
        public ToolItem()
        {
            TargetInventoryName = "ToolStorageInventory";
            ItemClass = ItemClasses.Tool;
            Equippable = true;
            TargetEquipmentInventoryName = "ToolEquippedInventory";
        }        
    }
}