using UnityEngine;

public class INTERACTABLE_pullgrass : MonoBehaviour, IInteractable {

    public Item[] randomItemDrops;
    
    public void OnInteract() {
        Item myItem;
        int randomIndex = Random.Range(0, randomItemDrops.Length);
        myItem = randomItemDrops[randomIndex];

        if (GAME_inventory_manager.Instance.AddItem(myItem) == true) {
            // If the item could be added to the player inventory, then destroy this Item Pickup object.
            Destroy(this.gameObject);
        }
        else {
            Debug.Log("Inventory full!");
        }
    }
}