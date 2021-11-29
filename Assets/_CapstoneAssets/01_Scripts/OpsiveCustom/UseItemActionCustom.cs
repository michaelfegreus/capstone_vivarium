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
        // Try to use item
        if (!PlayerManager.Instance.playerInteraction.playerSelector.UseItemSelection(itemInfo))
        {
            // If fail, play the wrong item shrug animation.
            PlayerManager.Instance.playerAnimation.PlayAnimationState("WrongItemShrug");
        }

    }
}
