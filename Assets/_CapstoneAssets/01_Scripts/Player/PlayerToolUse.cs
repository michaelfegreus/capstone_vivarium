using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Opsive.Shared.Game;
using Opsive.UltimateInventorySystem.Core.AttributeSystem;
using Opsive.UltimateInventorySystem.Core.DataStructures;
using Opsive.UltimateInventorySystem.ItemActions;
using Opsive.UltimateInventorySystem.Core.InventoryCollections;
using Opsive.UltimateInventorySystem.Core;

public class PlayerToolUse : MonoBehaviour
{
    //InputAction toolInput; // Cache the action since it will continously be referenced.
    ItemSlotCollection equipmentItemCollection; // Cache the Tool equipment item collection

    PlayerInput toolInput;

    [SerializeField] ItemUser playerItemUser;

    private void Start()
    {
        toolInput = new PlayerInput();
        toolInput.Enable();

        playerItemUser = GameManager.Instance.playerInventory.GetComponent<ItemUser>();
        equipmentItemCollection = GameManager.Instance.playerInventory.GetItemCollection("Equipment") as ItemSlotCollection;
    }

    private void OnEnable()
    {
        if (toolInput != null)
        {
            toolInput.Enable();
            toolInput.FreeControl.Tool.performed += UseTool;
        }
    }

    private void OnDisable()
    {
        toolInput.Disable();
        toolInput.FreeControl.Tool.performed -= UseTool;
    }

    void UseTool(InputAction.CallbackContext obj)
    {
        Debug.Log("Started using tool action");

        ItemInfo equippedTool = equipmentItemCollection.GetItemInfoAtSlot(0); // Get item at slot 0 because right now you can only equip one tool at a time like classic Harvest Moon. (You'll have to expand on this if you want multiple tools equipped at once like Zelda 64 C-buttons.)

        if (equippedTool.ItemAmount.Amount > 0) {
            // First, check to see if there's an Item Usable that uses this tool.
            if (PlayerManager.Instance.playerInteraction.playerSelector.UseItemSelection(equippedTool))
            {
                Debug.Log("Ran an ItemUsable event through a tool use.");
            }
            // Otherwise, run the tool's action by case.
            else
            {
                Debug.Log("Tried to run tool action.");

                if(TryUseTool(equippedTool, playerItemUser))
                {
                    Debug.Log("Made it through a tool item action attempt");
                }
                

                
                /*
                foreach(ToolAction element in toolActions)
                {
                    if (equippedTool.ItemAmount.Item.ItemDefinition.InherentlyContains(element.requiredItem.ItemDefinition))
                    {
                        element.toolInterface.ToolAction();
                    }
                    break;
                }
                */
            }
        }
    }

    /// <summary>
    /// Check if the action can be invoked.
    /// </summary>
    /// <param name="itemInfo">The item.</param>
    /// <param name="itemUser">The item user (can be null).</param>
    /// <returns>True if the action can be invoked.</returns>
    protected bool TryUseTool(ItemInfo itemInfo, ItemUser itemUser)
    {
        //Cannot use the item if null.
        if (itemInfo.Item == null) { return false; }

        //Get the Attribute with your scriptable object 
        var attribute = itemInfo.Item.GetAttribute<Attribute<ItemActionSet>>("ToolAction");

        //Make sure the attribute exists
        if (attribute == null) { return false; }

        // Get the scriptable object (in this case the Item Action Set)
        var actionSet = attribute.GetValue();
        if (actionSet == null) { return false; }

        Debug.Log(actionSet.name);
        //Since the Item Action has a list of items we need to choose which one we will use by specifying the index (or we could have executed all the actions).
        //if (m_ActionIndex < 0 || actionSet.ItemActionCollection.Count >= m_ActionIndex) { return false; }

        bool m_UseOne = true;

        // Use the item action on a single item, or the entire amount of item?
        if (m_UseOne) { itemInfo = (1, itemInfo); }

        //Call the function on the scriptable Object.
        // actionSet.ItemActionCollection[m_ActionIndex].InvokeAction(itemInfo, itemUser);

        // ^^ Modified to be a foreach loop for every action

        Debug.Log("Item action set count: " + actionSet.ItemActionCollection.Count);
        Debug.Log("Tool that I tried to use: " + itemInfo.Item.name);

        for (int i = 0; i < actionSet.ItemActionCollection.Count; i++)
        {
            actionSet.ItemActionCollection[i].InvokeAction(itemInfo, itemUser);
        }


        bool m_RemoveOnUse = false;

        // You may wish to remove the item once used.
        if (m_RemoveOnUse)
        {
            itemInfo.ItemCollection?.RemoveItem(itemInfo);
        }

        return true;

    }
    /*
    void CaseToolUse(ItemInfo equippedTool)
    {
        var toolType = equippedTool.Item.GetAttribute<Attribute<string>>("Name");

        if (toolType != null)
        {
            string toolTypeValue = toolType.GetValue();

            Debug.Log("Tool type: " + toolTypeValue);

            switch (toolTypeValue)
            {
                case "Shovel":
                    UseShovel();
                    break;
            }
        }
    }
    
    void UseShovel()
    {
        int layermask = LayerMask.GetMask("DiggableGround");

        Ray ray = new Ray(PlayerManager.Instance.playerMovement.playerMovementModule.transform.position + new Vector3(0, 0, -1), Vector3.forward);

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layermask);

        if (rayHit.collider != null)
        {
            Debug.Log("Dig on this spot.");
        }
        else
        {
            Debug.Log("Found nothing to dig.");
        }
    }*/
}