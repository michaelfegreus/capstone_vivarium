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
            if (GameManager.Instance.notebookMenuManager.menuOpen)
            {
                PlayerManager.Instance.playerAnimation.PlayAnimationState("WrongItemShrug");
            }
            else
            {
                PlayerManager.Instance.playerAnimation.PlayAnimationState("WrongItemShrugHotbar");
            }

        }
        // If item use SUCCEEDS, CLOSE the inventory screen so player can engage in dialogue or some other shit like watch an animation
        else
        {
            if (GameManager.Instance.notebookMenuManager.menuOpen)
            {
                GameManager.Instance.notebookMenuManager.CloseMenu();
                if (!itemInfo.Item.name.Equals("Folding Shovel") && !itemInfo.Item.name.Equals("Watering Can") && itemInfo.Item.Category.name != "Seeds")
                {
                    PlayerManager.Instance.playerAnimation.PlayAnimationState("CloseNotebookStayInMenu");
                }
            }
            else
            {
                GameManager.Instance.ItemHotbarManager.CloseHotbarPanel(true);
            }

        }

    }
}
