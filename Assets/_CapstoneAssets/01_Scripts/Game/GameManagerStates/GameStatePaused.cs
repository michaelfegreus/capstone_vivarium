using System;
using UnityEngine;

public class GameStatePaused : IState {

	private GameObject gameManagerObject;

	private Action _EnterFreeState;


	// Constructor:
	public GameStatePaused(GameObject gameManager, Action _EnterFreeState){
		gameManagerObject = gameManager;
		//this._SetPauseEnabled = _SetPauseEnabled;
		this._EnterFreeState = _EnterFreeState;
		//PLAYER_manager.Instance.
	}

	public void Enter(){
		//_SetPauseEnabled (true);
		Time.timeScale = 0f;
	}

	public void Execute(){
		
	}

	public void Exit(){
		//_SetPauseEnabled (false);
		Time.timeScale = 1f;
	}
}
