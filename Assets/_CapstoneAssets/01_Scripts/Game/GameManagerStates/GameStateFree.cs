using UnityEngine;

public class GameStateFree : IState {

	private GameObject gameManagerObject;


	// Constructor:
	public GameStateFree(GameObject gameManager){
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
