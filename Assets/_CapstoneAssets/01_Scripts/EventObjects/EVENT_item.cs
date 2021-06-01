using UnityEngine;

public class EVENT_item : EventObject
{
    // This is for other objects to look at the event item and find out when it's been picked up by the player.
    [HideInInspector] public bool itemPickedUpFlag;
    // Delegate to run function on flag goal met.
    public delegate void ItemPickupDelegate();
    [HideInInspector] public ItemPickupDelegate itemDelegate;

    [Header("Item Attributes")]
    // Item type given to this Event Item
    public Item itemType;

    [Header("Components")]
    public SpriteRenderer spriteRend;
    public INTERACTABLE_item_pickup itemInteractScript;

    public override void OnEnable()
    {
        // Make sure to use the base class' OnEnable scripting as well.
        base.OnEnable();

        itemPickedUpFlag = false;

        // Set the name of this game object as the Item Type name
        gameObject.name = itemType.name + "(EventItem)";

        // Set the Item in the interaction script.
        itemInteractScript.myItem = itemType;

        // Display the Item sprite in the game overworld.
        // Might eventually set up the overworld sprite as something separate from the iconSprite?
        spriteRend.sprite = itemType.iconSprite;
    }

    public override void OnDisable()
    {
        // v Added automatically. Nice! Thanks MS Visual Studio.
        base.OnDisable();

        if (itemPickedUpFlag && itemDelegate != null)
        {
            itemDelegate(); 
        }
    }
}
