using UnityEngine;

public class GAME_STATE_Free : IState {

	private GameObject gameManagerObject;


	// Constructor:
	public GAME_STATE_Free(GameObject gameManager){
		gameManagerObject = gameManager;
	}

	public void Enter(){
		
	}

	public void Execute(){
		// Control hub for in-game functions that do not involve direct control over the player

	}

	public void Exit(){
		//PLAYER_manager.Instance.EnterPreviousState ();
	}
}
