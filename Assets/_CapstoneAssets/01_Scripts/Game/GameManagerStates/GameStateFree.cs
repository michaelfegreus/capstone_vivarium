using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    public void Execute(){
		// Control hub for in-game functions that do not involve direct control over the player

	}

	public void Exit(){
        //PLAYER_manager.Instance.EnterPreviousState ();
        gameManager.menuInput.NotebookNavigation.OpenNotebookMenu.performed -= OpenMenu;
        gameManager.menuInput.NotebookNavigation.CloseNotebookMenu.performed -= CloseMenu;
    }

    void OpenMenu(InputAction.CallbackContext obj)
    {
        if(PlayerManager.Instance.GetPlayerState() is PlayerStateFreeControl && !gameManager.notebookMenuManager.menuOpen)
        {
            gameManager.notebookMenuManager.OpenMenu();
            PlayerManager.Instance.EnterMenuState();
        }
    }

    // Later this should be moved to GameStatePaused state, and opening the inventory can pause time in the game.
    void CloseMenu(InputAction.CallbackContext obj)
    {
        if (gameManager.notebookMenuManager.menuOpen)
        {
            gameManager.notebookMenuManager.CloseMenu();
            PlayerManager.Instance.EnterFreeState();
        }
    }
}
