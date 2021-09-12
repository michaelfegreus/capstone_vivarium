using UnityEngine;

public class PLAYER_STATE_BespokeAction : IState {
    /// <summary>
    /// This state is used for animations and actions that play out of the control of the player.
    /// For example, things like short cutscenes, or interactions with objects.
    /// </summary>

	private GameObject playerObject;

    PLAYER_movement_directional_2d movementScript;
    PLAYER_interaction interactionScript;
    PLAYER_animation animationScript;

    // Constructor:
    public PLAYER_STATE_BespokeAction(GameObject player){
        playerObject = player;
        movementScript = playerObject.GetComponent<PLAYER_movement_directional_2d>();
        interactionScript = playerObject.GetComponent<PLAYER_interaction>();
        animationScript = playerObject.GetComponent<PLAYER_animation>();
    }

	public void Enter(){
        Debug.Log("Player entered Bespoke Action state");
        // Disable free movement and control.
        movementScript.enabled = false;
        interactionScript.enabled = false;
        // Disable Wall Script check as well.
    }

	public void Execute(){

	}

	public void Exit(){

	}
}
