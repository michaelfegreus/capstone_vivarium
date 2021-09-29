using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;

namespace MoreMountains.InventoryEngine
{
    [CreateAssetMenu(fileName = "MaterialItem", menuName = "Vivarium/InventoryEngine/Material Item")]
    [Serializable]
    /// <summary>
    /// Basic materials that can be found. Stackable, sellable, usable in repair crafting.
    /// </summary>
    public class MaterialItem : InventoryItem
    {
        // The constructor sets the target inventory.
        public MaterialItem()
        {
            TargetInventoryName = "MainInventory";
            MaximumStack = 99;
            ItemClass = ItemClasses.Material;
        }

        [Tooltip("How much it costs to purchase this item at a store.")]
        public float purchaseCost;
        [Tooltip("How much the player can get selling this item.")]
        public float resaleValue;
    }
}