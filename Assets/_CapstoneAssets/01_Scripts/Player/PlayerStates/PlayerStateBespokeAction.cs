using UnityEngine;

public class PlayerStateBespokeAction : IState {
    /// <summary>
    /// This state is used for animations and actions that play out of the control of the player.
    /// For example, things like short cutscenes, or interactions with objects.
    /// </summary>

	private GameObject playerObject;

    PlayerMovement movementScript;
    PlayerInteraction interactionScript;
    PlayerAnimation animationScript;

    // Constructor:
    public PlayerStateBespokeAction(GameObject player){
        playerObject = player;
        movementScript = playerObject.GetComponent<PlayerMovement>();
        interactionScript = playerObject.GetComponent<PlayerInteraction>();
        animationScript = playerObject.GetComponent<PlayerAnimation>();
    }

	public void Enter(){
        Debug.Log("Player entered Bespoke Action state");
        // Disable free movement and control.
        movementScript.enabled = false;
        // Disable Wall Script check as well.
    }

	public void Execute(){

	}

	public void Exit(){

	}
}
