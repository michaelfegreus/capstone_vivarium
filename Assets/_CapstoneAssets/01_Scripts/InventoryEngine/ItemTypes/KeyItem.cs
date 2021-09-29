using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;

namespace MoreMountains.InventoryEngine
{
    [CreateAssetMenu(fileName = "KeyItem", menuName = "Vivarium/InventoryEngine/Key Item")]
    [Serializable]
    /// <summary>
    /// A key item. Generally there will only be a single instance of these collectable in the game, and they'll often have relevance to quests.
    /// </summary>
    public class KeyItem : InventoryItem
    {
        // The constructor sets the target inventory.
        public KeyItem()
        {
            TargetInventoryName = "KeyItemInventory";
            ItemClass = ItemClasses.KeyItem;

        }
    }
}