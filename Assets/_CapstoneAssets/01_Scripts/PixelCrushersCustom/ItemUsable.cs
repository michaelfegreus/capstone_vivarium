﻿using PixelCrushers.DialogueSystem;
using UnityEngine.Events;
using Opsive.Shared.Game;
using Opsive.UltimateInventorySystem.Core.AttributeSystem;
using Opsive.UltimateInventorySystem.Core.DataStructures;
using Opsive.UltimateInventorySystem.ItemActions;
using System;
using UnityEngine;

public class ItemUsable : Usable
{
    public UsableItemEvent[] usableItemEvents;

    public void OnItemUse(ItemInfo _usedItem)
    {
        foreach (UsableItemEvent element in usableItemEvents)
        {
            // Check if you used the proper item.
            if (_usedItem.ItemAmount.Item.ItemDefinition.InherentlyContains(element.requiredItem.Item.ItemDefinition))
            {
                // Make sure the event hasn't been disabled after already being used
                if (!element.eventDisabled)
                {

                    // Now check if you have the right amount of required item. If you do, invoke the event.
                    if (GameManager.Instance.playerInventory.HasItem(element.requiredItem))
                    {
                        Debug.Log("Used the right item, and you have enough of them in your inventory.");

                        element.onItemUse.Invoke();
                        if (element.consumeItem == true)
                        {
                            Debug.Log("Consumed " + element.requiredItem.Amount + element.requiredItem.Item.name + "(s) from the player's inventory.");
                        }
                        if (element.disableEventAfterInvoked)
                        {
                            element.eventDisabled = true;
                        }
                        break; // Break the loop of comparing items after the first successful invocation.
                    }

                    // Else if you had the right item but not enough of it.
                    else
                    {
                        Debug.Log("Used the right item, but you don't have enough of them in your inventory.");
                    }

                }
            }
        }
        // Should add a functionality where it sends a message to Dialogue System saying that there was no effect if an item was no event was used.
    }

    [Serializable]
    public class UsableItemEvent
    {
        [Tooltip("Remove Amount of item from inventory on successful use.")]
        public bool consumeItem;
        [Tooltip("Prevent the player from using the same item and invoking this UsableItemEvent a second time")]
        public bool disableEventAfterInvoked = true;
        [HideInInspector] public bool eventDisabled = false;
        [Tooltip("The item you need to use invoke this event.")]
        public ItemAmount requiredItem;
        [Tooltip("Invoked when a Selector or ProximitySelector is highlighting this Usable, and an item is used from the custom UIS item action.")]
        public UnityEvent onItemUse;
        
    }
}