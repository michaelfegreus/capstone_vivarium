namespace Opsive.UltimateInventorySystem.DropsAndPickups
{
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using Opsive.UltimateInventorySystem.Interactions;
    using System;
    using UnityEngine;
    using EventHandler = Opsive.Shared.Events.EventHandler;
    using System.Collections;

    public class ItemPickupCustom : ItemPickup
    {
        [SerializeField] float delayToDisableObject =.6f;

        [Tooltip("The item collection where tools should be added when picking up the item if the tool collection is empty.")]
        [SerializeField] protected ItemCollectionID m_AddToToolEquipCollection = ItemCollectionPurpose.Equipped;

        // Set this up so I could access this OnInteract function publicly. That way, Pixel Crusher's Selector could call this thing's OnInteract method.
        public void AddToPlayerInventory()
        {
            Inventory playerInventory = GameManager.Instance.playerInventory;

            var itemCollection = playerInventory.GetItemCollection(m_AddToItemCollection);

            // If the thing you're picking up is a member of the Tool category, try to send it to the Equipment item collection instead of the Main item collection.
            if (ItemInfo.Item.Category.name == "Tool")
            {
                itemCollection = playerInventory.GetItemCollection(m_AddToToolEquipCollection);
            }


            if (itemCollection == null) {
                itemCollection = playerInventory.MainItemCollection;
            }

            TryAddItemToCollection(itemCollection);

        }

        /// <summary>
        /// Use this to make the item disappear after the animation of the grab happens.
        /// </summary>
        IEnumerator DelayDisable()
        {
            yield return new WaitForSeconds(delayToDisableObject);

            gameObject.SetActive(false);
        }

        public void CustomPickupDisable()
        {
            StartCoroutine(DelayDisable());
        }
    }
}