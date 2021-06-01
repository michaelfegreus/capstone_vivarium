using UnityEngine;

public class INTERACTABLE_item_pickup : MonoBehaviour, IInteractable {

	public Item myItem;

	public void OnInteract(){
		if (GAME_inventory_manager.Instance.AddItem (myItem) == true) {
            // The following code is to set flag if this is an Event Item
            EVENT_item eventItemScript;
            if ((eventItemScript = GetComponent<EVENT_item>()) != null)
            {
                eventItemScript.itemPickedUpFlag = true;
            }

			// If the item could be added to the player inventory, then disable this Item Pickup object.
			gameObject.SetActive(false);
		} else {
			Debug.Log ("Inventory full!");
		}
	}

    // I wonder if anyone will every look at this code one day. Will I have kids that look at this some day, or will my nephew see this and care about my work here?
}
