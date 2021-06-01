using UnityEngine;
using Fungus;

public class LEVEL_interactable_fungus : MonoBehaviour, IInteractable, IItemTarget
{
    // Use this component for interactables that live in the level hierarchy that need to call a Fungus Block.
    // This is in contrast to Event Object

    [Tooltip("The block to run on interaction.")]
    public BlockReference interactBlock;

    [Tooltip("Additional Blocks to run on using specified items.")]
    public ItemUseBlockRef[] itemInteractions;

    public void OnInteract()
    {
        if (interactBlock.Equals(null))
        {
            Debug.Log("No Interact Block applied to Level Interactable " + gameObject.name);
        }
        else
        {
            interactBlock.Execute();
        }
    }

    public void OnItemUse(Item itemUsed)
    {
        for (int i = 0; i < itemInteractions.Length; i++)
        {
            if (itemInteractions[i].CheckItemBlockExecute(itemUsed))
            {
                return;
            }
        }
    }
}