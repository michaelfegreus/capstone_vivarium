using System.Collections;
using UnityEngine;
using Fungus;

[CommandInfo("NPC Commands",
			 "Move to Location",
			 "Moves the NPC to a location.")]

public class FUNGUS_command_npc_move : Command {

	public ACTOR_npc_manager npcManager;

	[Tooltip("The GameObject marking the destination of this movement.")]
	public Transform destinationTransform;
	[Tooltip("Should the NPC walk to their destination, or warp there?")]
	public bool warpToDestination;

	Flowchart myFlowchart;


	public override void OnEnter(){
		myFlowchart = GetFlowchart ();
		if (npcManager == null) {
			npcManager = myFlowchart.GetComponentInParent <ACTOR_npc_manager> ();
		}
		// Always set warpToDestination to true if it's time for the next block.
		// That way you don't have to wait for the character to navigate to the destination before continuing.
		if (myFlowchart.GetBooleanVariable ("nextBlockTrigger")) {
			warpToDestination = true;
		}
		npcManager.MoveCommand (ContinueOnMoveComplete, destinationTransform, warpToDestination);
	}

	///<summary>>If still executing this Command, then run Continue() to move onto the next Command.
	/// This checks if executing the Move Command before running Continue() in the event that the game hasn't already moved on from this command to do something else.
	/// (If that were the case it could just call Continue() on the wrong Command.)</summary>
	public void ContinueOnMoveComplete(){
		if (IsExecuting) {
			Continue ();
		}
	}
}
