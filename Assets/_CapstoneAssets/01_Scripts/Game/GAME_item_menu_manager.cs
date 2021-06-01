using UnityEngine;

public class GAME_item_menu_manager : MonoBehaviour
{
    public GameObject itemMenuDialog;
    public ItemMenuButton[] itemMenuButtons;

    private void Update()
    {
        if (Input.GetKeyDown(GAME_manager.Instance.inputManager.itemMenuButton1) || Input.GetKeyDown(GAME_manager.Instance.inputManager.itemMenuButton2))
        {
            // Can only enter menu when the player is in Free Control state.
            if (PLAYER_manager.Instance.GetPlayerState() is PLAYER_STATE_FreeControl)
            {
                SetMenu();

            }
            else if(PLAYER_manager.Instance.GetPlayerState() is PLAYER_STATE_Menu && itemMenuDialog.activeInHierarchy)
            {
                itemMenuDialog.SetActive(false);
            }
        }
    }

    private void SetMenu()
    {
        for (int i = 0; i < itemMenuButtons.Length; i++)
        {
            itemMenuButtons[i].myItemRef = GAME_manager.Instance.inventoryManager.playerInventory.itemsHeld[i];
        }
        itemMenuDialog.SetActive(true);
    }

    /*
    // Is the menu currently in use?
    bool inMenu;

    public GameObject itemMenuDialog;
    public Transform buttonGroup;

    // Get a reference to all the Text Mesh options.
    SuperTextMesh[] itemMenuText;

    private void Start()
    {
        // Set up the SuperTextMesh itemMenuText array.
        itemMenuText = new SuperTextMesh[buttonGroup.childCount];
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            itemMenuText[i] = buttonGroup.GetChild(i).GetComponentInChildren<SuperTextMesh>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(GAME_manager.Instance.inputManager.itemMenuButton1) || Input.GetKeyDown(GAME_manager.Instance.inputManager.itemMenuButton2)){
            if (!inMenu)
            {
                SetMenu();
                inMenu = true;
            }
            else
            {
                itemMenuDialog.SetActive(false);
                inMenu = false;
            }
        }
    }

    private void SetMenu()
    {
        for(int i = 0; i < itemMenuText.Length; i++)
        {
            if (GAME_manager.Instance.inventoryManager.playerInventory.itemsHeld[i] != null)
            {
                itemMenuText[i].text = GAME_manager.Instance.inventoryManager.playerInventory.itemsHeld[i].inGameItemName;
            }
            else
            {
                itemMenuText[i].text = "---Empty---";
            }
        }
        itemMenuDialog.SetActive(true);
    }*/
}
