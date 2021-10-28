﻿using System;
using UnityEngine;

public class PlayerStateMenu : IState {

	private GameObject playerObject;

	PlayerMovement movementScript;

	// Constructor:
	public PlayerStateMenu(GameObject player){
		playerObject = player;
		movementScript = playerObject.GetComponent<PlayerMovement>();
		Debug.Log ("Player entered Menu state");
	}

	public void Enter(){
		// Disable the scripts that the player should have when they are free to move around.
		//playerObject.GetComponent<PLAYER_movement_directional_2d> ().enabled = false; --- Keep movement script running so it doesn't awkwardly jerk the character to a stop in disabling the script.
		playerObject.GetComponent<PlayerInteraction> ().enabled = false;
	}

	public void Execute(){
		movementScript.SetMovementAxes(0f, 0f);
	}

	public void Exit(){

	}

}