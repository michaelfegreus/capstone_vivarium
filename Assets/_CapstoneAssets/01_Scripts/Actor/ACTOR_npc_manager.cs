using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Fungus;

public class ACTOR_npc_manager : MonoBehaviour, IInteractable, IItemTarget {

	//[SerializeField]
	//bool atTaskLocation; // Is the NPC at the task location?
	//[SerializeField]
	//bool walkToTaskLocation; // Should the NPC walk to the location, or just warp there immediately?

	INTERACTABLE_npc_dialogue yarnManager;
	ACTOR_pathfinding_ai pathfindingScript;

	bool walkingToDestination;
	Transform currentDestination;

	GameTime nextScheduleTime;
	// Call when next schedule time has been met
	public delegate void OnScheduleTimeMet();
	OnScheduleTimeMet myScheduleTimeMet;

	//Flowchart myFlowchart;

	// Has a command been completed? Run this delegate function.
	public delegate void OnCommandComplete();
	OnCommandComplete myCommandComplete;

	// Variables set when the NPC has a specific task or goal to complete.
	/*public enum NPCTaskType
	{
		NoTask,
		ConversationTask,
		UseItemTask,
		TakeItemTask
	}*/

	//public NPCTaskType myTask;		
	//public Item itemSought;
	//public string eventMessage;


	// NPC Task Class Management
	Task myTask;

	void Start(){
		//myTask = NPCTaskType.NoTask;
		pathfindingScript = GetComponent<ACTOR_pathfinding_ai> ();
		yarnManager = GetComponent<INTERACTABLE_npc_dialogue> ();
	}

	void Update(){
		// TODO: Set a coroutine to make this only tick every 1 second, not every one frame.
		if (nextScheduleTime != null) {
			CheckScheduleTime ();
		}
	}
		
	/* Next schedule block shenanigans.
	void Update(){
		// Detect a change in which Block is being executed, when waiting on a time sensitive command.
		/*if (currentExecutingBlock != null) {
			if (myFlowchart.GetExecutingBlocks () == null) { // If there are no more blocks being executed, that means the current block is over.
				NextScheduleBlockRefresh ();
			} else if (currentExecutingBlock != myFlowchart.GetExecutingBlocks () [0]) { // Or, if there is a different block now being executed, that also means the current block is over.
				NextScheduleBlockRefresh ();
			}
		}
	}

	/// <summary>
	/// Use this to handle any of the leftover Commands and functionality that the NPC is still running on its current Block,
	/// when the scheduled time to move on has come.
	/// </summary>
	void NextScheduleBlockRefresh(){
		// If currently walking to a destination, stop and warp the rest of the way.
		if (walkingToDestination) {
			WarpToDestination (currentDestination);
			//myCommandComplete (); // Set walkingToDestination to false. and potentially any future additional commands added to delegate.
			CompletedWalkingToDestination();
			currentExecutingBlock = null;
		}
	}
*/
	//-----------------------------------------------------------------------------------------
	// Methods called by Fungus Flowchart Commands (directly or indirectly):
	//-----------------------------------------------------------------------------------------

	public void SetNextScheduleTime(int hour, int minute, OnScheduleTimeMet newScheduleCommand){
		nextScheduleTime = new GameTime (hour, minute);
		myScheduleTimeMet = newScheduleCommand;
		CheckScheduleTime ();
	}

	void CheckScheduleTime(){
		if(WORLD_manager.Instance.timeManager.inGameTime.TimeMet(nextScheduleTime)){
			myScheduleTimeMet ();
			nextScheduleTime = null;
		}
	}

	public void MoveCommand(OnCommandComplete newCommandCompleteFunction, Transform _destinationTransform, bool warp){
		myCommandComplete = newCommandCompleteFunction;
		currentDestination = _destinationTransform;
		if (!warp) {
			walkingToDestination = true;
			myCommandComplete += CompletedWalkingToDestination; // Add this function to the delegate, so that the associated bool will be set to false once the destination is reached.
			pathfindingScript.SetDestinationByTransform (_destinationTransform, myCommandComplete);
			//pathfindingScript.enabled = true;
		} else {
			WarpToDestination (_destinationTransform); // TODO: Move this to update when there's functionality of waiting for the player to be offscreen to Warp.
			myCommandComplete();
		}
	}

	void CompletedWalkingToDestination(){
		walkingToDestination = false;
	}

	void WarpToDestination(Transform destination){
		pathfindingScript.enabled = false;
		transform.position = destination.position;
		// TODO: Make option to only warp when not in view of the player.
	}

	public void SetYarnNode(string newYarnNode){
		yarnManager.SetTalkToNode(newYarnNode);
	}

	//-----------------------------------------------------------------------------------------
	// Set Task Methods :
	//-----------------------------------------------------------------------------------------

	public void SetTalkToTask(string eventMessage, string taskYarnNode){
		myTask = new Task(Task.NPCTaskType.TalkToTask, eventMessage, taskYarnNode, null, false);
	}

	public void SetItemTask(string eventMessage, string taskYarnNode, Item itemSought, bool takeItem){
		myTask = new Task (Task.NPCTaskType.ItemTask, eventMessage, taskYarnNode, itemSought, takeItem);
	}

	//-----------------------------------------------------------------------------------------
	// Interface Methods :
	//-----------------------------------------------------------------------------------------

	// When interacted with (aka spoken to)
	public void OnInteract(){
		
	}

	// When an item is used on this NPC
	public void OnItemUse (Item itemUsed) {
		if (myTask.myTaskType == Task.NPCTaskType.ItemTask) {
			if(itemUsed == myTask.itemSought){
				Flowchart.BroadcastFungusMessage (myTask.eventMessage); // TODO: Maybe don't broadcast message, but instead make it just send to myFlowchart? Right now trying that gives a null object reference ;p
				myTask.myTaskType = Task.NPCTaskType.NoTask;
				//Debug.Log ("Gave the item successfully, completed the task.");
				// Remove the item from the inventory if the NPC task specifies that.
				if (myTask.takeItem == true) {
					GAME_inventory_manager.Instance.RemoveItem (myTask.itemSought);
				}
				// TODO: Have the potential to hold a conversation about this specific item interaction at this time.
				//GAME_manager.Instance.dialogRunner.StartDialogue (myTask.taskYarnNode);
			}
		}
		// TODO: Else, give a conversation about a general item you showed. This is a core mechanic that serves as hints towards puzzle solving as NPCs potentially give information on various items.
	}
}
