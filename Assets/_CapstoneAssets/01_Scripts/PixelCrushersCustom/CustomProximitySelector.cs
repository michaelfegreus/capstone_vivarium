using PixelCrushers.DialogueSystem;
using UnityEngine.Events;
using Opsive.UltimateInventorySystem.Core.DataStructures;
using UnityEngine;

public class CustomProximitySelector : PixelCrushers.DialogueSystem.CustomProximitySelector
{
    // This code was generously written by Tony Li as a means to run events when a Proximity Selector uses something.

    public UnityEvent onUse;

    public override void UseCurrentSelection()
    {
        base.UseCurrentSelection();
        onUse.Invoke();
    }

    public bool SelectingUsable()
    {
        if (CurrentUsable != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool SelectingItemUsable()
    {
        if (!SelectingUsable())
        {
            return false;
        }
        else
        {
            if (CurrentUsable.GetType().Equals(typeof(ItemUsable)))
            {
                Debug.Log("Current usable IS an ItemUsable.");
                return true;
            }
            else
            {
                Debug.Log("Current usable NOT an ItemUsable.");
                return false;
            }
        }
    }

    public bool UseItemSelection(ItemInfo usedItem)
    {
        bool succesfullyUsedItem = false;
        if (SelectingItemUsable())
        {
            ItemUsable currentItemUsable = CurrentUsable.GetComponent<ItemUsable>();
            succesfullyUsedItem = currentItemUsable.TryItemUse(usedItem);
        }
        return succesfullyUsedItem;
    }
}