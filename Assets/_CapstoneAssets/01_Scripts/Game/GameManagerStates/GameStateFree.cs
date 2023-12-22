using UnityEngine;
using UnityEngine.InputSystem;
using PixelCrushers.DialogueSystem;

public class GameStateFree : IState {

	private GameObject gameManagerObject;
    private GameManager gameManager;
    //InputAction openMenu; // Cache the input since it will continously be referenced.
    //InputAction closeMenu;

    // Constructor:
    public GameStateFree(GameObject gameManagerObj){
		gameManagerObject = gameManagerObj;
        gameManager = gameManagerObj.GetComponent<GameManager>();
	}

	public void Enter(){
        gameManager.menuInput.NotebookNavigation.OpenNotebookMenu.performed += OpenMenu;
        gameManager.menuInput.NotebookNavigation.CloseNotebookMenu.performed += CloseMenu;
        gameManager.menuInput.NotebookNavigation.OpenItemHotbar.performed += OpenItemHotbar;
        gameManager.menuInput.NotebookNavigation.CloseItemHotbar.performed += CloseItemHotbar;

    }

    public void Execute(){
		// Control hub for in-game functions that do not involve direct control over the player

	}


    //TODO: This isn't getting called EVER!@??!@?!?!?!?
	public void Exit(){
        //PLAYER_manager.Instance.EnterPreviousState ();
        gameManager.menuInput.NotebookNavigation.OpenNotebookMenu.performed -= OpenMenu;
        gameManager.menuInput.NotebookNavigation.CloseNotebookMenu.performed -= CloseMenu;
        gameManager.menuInput.NotebookNavigation.OpenItemHotbar.performed -= OpenItemHotbar;
        // gameManager.menuInput.NotebookNavigation.CloseItemHotbar.performed -= CloseItemHotbar;
    }

    void OpenMenu(InputAction.CallbackContext obj)
    {
        if(PlayerManager.Instance.GetPlayerState() is PlayerStateFreeControl && !gameManager.notebookMenuManager.menuOpen)
        {
            gameManager.notebookMenuManager.OpenMenu();
            gameManager.hideQuestHUD();
            PlayerManager.Instance.EnterMenuState();
            PlayerManager.Instance.playerAnimation.PlayAnimationState("TurnToNotebook");

        }
    }

    // Later this should be moved to GameStatePaused state, and opening the inventory can pause time in the game.
    void CloseMenu(InputAction.CallbackContext obj)
    {
        if (gameManager.notebookMenuManager.menuOpen)
        {
            gameManager.notebookMenuManager.CloseMenu();
            gameManager.showQuestHUD();
            PlayerManager.Instance.playerAnimation.PlayAnimationState("CloseNotebook");
            //PlayerManager.Instance.EnterFreeState();
        }
    }

    //What to do when closing or opening the hotbar
    void OpenItemHotbar(InputAction.CallbackContext obj)
    {
        if (PlayerManager.Instance.GetPlayerState() is PlayerStateFreeControl && !gameManager.ItemHotbarManager.hotbarOpen && !gameManager.notebookMenuManager.menuOpen && !DialogueManager.IsConversationActive)
        {
            PlayerManager.Instance.EnterMenuState();
            gameManager.ItemHotbarManager.OpenHotbarPanel();
        }

    }

    void CloseItemHotbar(InputAction.CallbackContext obj)
    {
        Debug.Log("Close item hotbar from GameStateFree");
        if (gameManager.ItemHotbarManager.hotbarOpen)
        {
            PlayerManager.Instance.EnterFreeState();
            gameManager.ItemHotbarManager.CloseHotbarPanel();
        }

    }
}
