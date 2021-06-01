using Fungus;
using System;
using UnityEngine;

[Serializable]
public class ItemUseBlockRef
{
    [Tooltip("Using this Item will trigger the Fungus Block reference.")]
    public Item useItem;

    [Tooltip("The Fungus Block that will trigger on using the specified item.")]
    public BlockReference interactBlock;

    // Constructor
    public ItemUseBlockRef(Item interactionUseItem, BlockReference interactionFungusBlocks)
    {
        useItem = interactionUseItem;
        interactBlock = interactionFungusBlocks;
    }

    /// <summary>
    /// Checks the Item. If it matches the reference, it executes the associated Fungus Block.
    /// </summary>
    /// <param name="usedItem">Used item.</param>
    public bool CheckItemBlockExecute(Item usedItem)
    {
        bool itemMatch = false;
        if (usedItem == useItem)
        {
            if (interactBlock.Equals(null))
            {
                Debug.LogWarning("No Interact Block applied with Item " + useItem.name + " on Game Object.");

                // Maybe cause the character to play some kind of "I can't do this" shrug animation?
            }
            else
            {
                interactBlock.Execute();
                itemMatch = true;
            }
        }
        return itemMatch;
    }
}
