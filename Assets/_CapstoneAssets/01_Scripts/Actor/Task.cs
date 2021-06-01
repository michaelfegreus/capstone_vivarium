using UnityEngine;
using System.Collections;

// Base class for NPC tasks that are triggered
// i.e. things that are not immediately accomplished, such as waiting for the player to have a conversation with this NPC, or waiting to receive an item from the player.
public class Task {

	public enum NPCTaskType
	{
		NoTask,
		TalkToTask,
		ItemTask
	}

	// Universal to all Tasks:
	public string eventMessage; // Send this when the task trigger occurs, i.e. NPC gets to talk to the player character.
	public string taskYarnNode; // Use this dialogue node when the task trigger occurs.
	public NPCTaskType myTaskType;

	// For ItemTask:
	public Item itemSought;
	public bool takeItem;

	/*public Task(string newEventMessage, string newTaskYarnNode){
		eventMessage = newEventMessage;
		taskYarnNode = newTaskYarnNode;
	}*/

	public Task(NPCTaskType newTask, string newEventMessage, string newTaskYarnNode, Item newItemSought, bool newTakeItem) {

		myTaskType = newTask;
		eventMessage = newEventMessage;
		taskYarnNode = newTaskYarnNode;
		itemSought = newItemSought;
		takeItem = newTakeItem;

	}
}
/* 4/20/2019 blzase it -- Couldn't access variables from derived classes, so I'm not using them anymore.
//-----------------------------------------------------------------------------------------
// Derived Classes :
//-----------------------------------------------------------------------------------------

// I didn't break these derived classes out into their own .cs documents because I thought they were small enough to exist here.

public class ItemTask : Task {



	public ItemTask(string newEventMessage, string newTaskYarnNode, Item newItemSought, bool newTakeItem) {

		eventMessage = newEventMessage;
		taskYarnNode = newTaskYarnNode;
		itemSought = newItemSought;
		takeItem = newTakeItem;

	}


}*/