using UnityEngine;
using MoreMountains.InventoryEngine;

[RequireComponent(typeof(InventoryDisplay))]
public class InventoryDisplayNavigationToggle : MonoBehaviour
{
    InventoryDisplay myInventoryDisplay;

    private void Start()
    {
        myInventoryDisplay = GetComponent<InventoryDisplay>();
    }

    public void SetNavigation(bool navigationEnabled) {
        myInventoryDisplay.enabled = navigationEnabled;
    }
}
