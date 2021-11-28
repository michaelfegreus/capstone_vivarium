using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.Shared.Game;
using Opsive.UltimateInventorySystem.Core.AttributeSystem;
using Opsive.UltimateInventorySystem.Core.DataStructures;
using Opsive.UltimateInventorySystem.ItemActions;

public class UseItemActionCustom : ItemAction
{
    protected override bool CanInvokeInternal(ItemInfo itemInfo, ItemUser itemUser)
    {
        // Let the player Use Item if they are highlighting any usable at all.
        // This will just keep them guessing and trying to puzzle out which usables they can use an item on.

        return PlayerManager.Instance.playerInteraction.playerSelector.SelectingUsable();
    }

    protected override void InvokeActionInternal(ItemInfo itemInfo, ItemUser itemUser)
    {
        
        // If the player is highlighting an ItemUsable with their Selector, run its functionality.
        if (PlayerManager.Instance.playerInteraction.playerSelector.SelectingItemUsable())
        {
            PlayerManager.Instance.playerInteraction.playerSelector.UseItemSelection(itemInfo);
        }
        // Otherwise, run a default "can't use that item here" functionality.
        else
        {
            PlayerManager.Instance.playerAnimation.PlayAnimationState("WrongItemShrug");
            Debug.Log("Oak's words echoed... 'There's a time and place for everything, but not now.'");
        }

    }
}
