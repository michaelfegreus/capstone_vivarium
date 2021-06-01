using UnityEngine.UI;

public class GAME_inventory_manager : SingletonPersistant<GAME_inventory_manager> {

	protected override void Awake(){
		base.Awake ();
        // Can add more awake stuff below.
        playerInventory = new Inventory(setDebugPlayerInventory);
    }



	// This debug inventory allows you to set Inventory Size and Items held in advance in the editor, so you can test out different carrying capacity and items.
	public Item[] setDebugPlayerInventory;

	public Inventory playerInventory;

	// What item is currently equipped?
	public Item equippedItem;
	// Reference to the equipped item HUD.
	public Image equippedItemHUD;

	// Whether the item menu is open. This information gets sent back to the GAME_STATE_Free state on the Game Manager to tell
	// the Player Manager whether it should be in the free state or menu state.
	bool inItemMenu;

    // Event delegate system for OnTriggerEnter. This is used to send signals to the event system in Fungus.
    public delegate void OnInventoryChange();
    public static event OnInventoryChange InventoryChangeEvent;


    void Start(){
		SetEquippedItemHUD ();
	}

	// Add an item during run time.
	// I added the bool type to streamline checking to see if there are empty slots. If it wasn't able to add more items, it returns false.
	public bool AddItem(Item newItem){
		bool canAddItem = false;
		// If there's an empty slot, add the Item asset to that slot.
		for (int i = 0; i < playerInventory.itemsHeld.Length; i++) {
			if (playerInventory.itemsHeld [i] == null) {
				playerInventory.itemsHeld [i] = newItem;
				canAddItem = true;
                // Signal an inventory change.
                if (InventoryChangeEvent != null)
                {
                    InventoryChangeEvent();
                }
				// This breaks the loop early if the item has been successfully added.
				return canAddItem;
			}
		}
		return canAddItem;
	}

	// Remove an item at run time.
	// If this returns false, then the item is not present in the inventory.
	public bool RemoveItem(Item itemToRemove){
		bool canRemoveItem = false;
		// If it finds the item, remove it from the slot.
		for (int i = 0; i < playerInventory.itemsHeld.Length; i++) {
			if (playerInventory.itemsHeld [i] == itemToRemove) {
				playerInventory.itemsHeld [i] = null;
				canRemoveItem = true;
                // Signal an inventory change.
                if (InventoryChangeEvent != null)
                {
                    InventoryChangeEvent();
                }
                // This breaks the loop early if the item has been successfully removed.
                return canRemoveItem;
			}
		}
		return canRemoveItem;
	}

	// Check to see if the item inventory's slots are all taken.
	public bool CheckFull(){
		bool full = true; // Starts as true, but flips to false as soon as it finds an exception.
		for (int i = 0; i < playerInventory.itemsHeld.Length; i++) {
			if (playerInventory.itemsHeld [i] == null) {
				full = false;
				return full;
			}
		}
		return full;
	}

	// Search through this inventory and see if a particular item is within it.
	public bool CheckInventoryForItem(Item checkingItem){
		bool hasItem = false; // Starts as true, but flips to false as soon as it finds the item.
		for (int i = 0; i < playerInventory.itemsHeld.Length; i++) {
			if (playerInventory.itemsHeld [i] == checkingItem) {
				hasItem = true;
				return hasItem;
			}
		}
		return hasItem;
	}

	public bool SetItemMenuInput(){
		if (!inItemMenu) {
			OpenItemMenu ();
			inItemMenu = true;
		} else {
			CloseItemMenu ();
			inItemMenu = false;
		}
		return inItemMenu;
	}

	// Interact with the GAME_menu_manager singleton and prepare a string array of all the item names to show on screen.
	// In the future, this is probably going to need to send the actual inventory item references instead if the menu needs to show the item descriptions and associated sprites...
	// ...The menu manager would parse through that and put up sprites and text on screen through an additional function in the menu manager script.
	void OpenItemMenu(){
		string[] itemNames = new string[playerInventory.itemsHeld.Length];
		for (int i = 0; i < playerInventory.itemsHeld.Length; i++) {
			if (playerInventory.itemsHeld [i] != null) { // If there's an item in the slot
				itemNames [i] = playerInventory.itemsHeld [i].inGameItemName;

			} else {
				itemNames [i] = "--- Empty ---";
			}
			//Debug.Log(playerInventory.itemsHeld [i].itemName);
			//Debug.Log(playerInventory.itemsHeld.Length);

		}
		GAME_menu_manager.Instance.SetMenu (itemNames, true, SelectMenuItem);
		PLAYER_manager.Instance.EnterMenuState ();
	}

	public void SelectMenuItem(int chosenIndex) {
		// 01/06/2019 -- Instead of doing the following, I'm going to set the selected item as the player's equipped item.
		//				 From there, the player can press another button handled elsewhere to actually use the item.
		/*if (playerInventory.itemsHeld [chosenIndex] != null) {
			Debug.Log ("Selected item " + playerInventory.itemsHeld [chosenIndex].itemName);
				 From there, the player can press another button handled elsewhere to actually use the item.

			// Get the player's interaction script, then tell it to use the item on what it's interacting with.
			// PLAYER_manager.Instance.playerInteraction.UseItemInput(playerInventory.itemsHeld[chosenIndex]);

		} else {
			Debug.Log ("Selected a slot with Null item.");
		}*/

		equippedItem = playerInventory.itemsHeld [chosenIndex];
		SetEquippedItemHUD ();

		//CloseItemMenu ();
		PLAYER_manager.Instance.EnterPreviousState ();
		inItemMenu = false;
	}

	void CloseItemMenu(){
		GAME_menu_manager.Instance.CloseMenu ();
		PLAYER_manager.Instance.EnterPreviousState ();
	}

    /// <summary>
    /// Sets the equipped item.
    /// </summary>
    /// <param name="chosenItem">Chosen item.</param>
    public void SetEquipItem(Item chosenItem)
    {
        equippedItem = chosenItem;
        SetEquippedItemHUD();
    }

	// Place an icon in the equipped item box HUD
	void SetEquippedItemHUD(){
		
		if (equippedItem != null) {
			equippedItemHUD.sprite = equippedItem.iconSprite; // Set the HUD sprite.
			equippedItemHUD.enabled = true;
		} else {
			equippedItemHUD.enabled = false;
		}
	}

	// Called from Actors checking for items via YarnSpinner.
	/*public bool CompareItemNames(string checkingItemName){
		bool hasItem = false; // Starts as true, but flips to false as soon as it finds the item.
		for (int i = 0; i < playerInventory.itemsHeld.Length; i++) {
			if (playerInventory.itemsHeld [i].inGameItemName.Trim().Equals(checkingItemName)) {
				hasItem = true;
				return hasItem;
			}
		}
		return hasItem;
	}
	// Called from Actors checking for items via YarnSpinner.
	public void RemoveItemByName(string checkingItemName){
		for (int i = 0; i < playerInventory.itemsHeld.Length; i++) {
			if (playerInventory.itemsHeld [i].inGameItemName.Trim().Equals(checkingItemName)) {
				RemoveItem (playerInventory.itemsHeld [i]);
				break;
			}
		}
	}*/
}