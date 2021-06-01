using UnityEngine;
using Fungus;

[CommandInfo("NPC Commands",
	"Set NPC Item Task",
	"Sets the NPC to seek a specific Item from the player. When the player uses the Item on this NPC, the NPC Manager will send a corresponding event message back to this flowchart.")]

public class FUNGUS_command_npc_itemtask : Command {
	

	public ACTOR_npc_manager npcManager;

	[Tooltip("The item being sought by the NPC as a part of this task.")]
	public Item taskItem;
	[Tooltip("Should the NPC remove the item from the player's inventory when the player shows the item to the character?")]
	public bool takeItemOnUse;
	[Tooltip("Send this event message to the Fungus flowcharts to potentially continue the flow.")]
	public string eventMessageToSend;
	[Tooltip("Associated dialogue the character should have during this task.")]
	public string yarnNode;
	
	public override void OnEnter(){
		if (npcManager == null) {
			npcManager = GetFlowchart().GetComponentInParent <ACTOR_npc_manager> ();
		}
		npcManager.SetItemTask (eventMessageToSend, yarnNode, taskItem, takeItemOnUse);

		Continue ();
	}
}
