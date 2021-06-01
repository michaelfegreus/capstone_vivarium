using UnityEngine;
using Fungus;

[CommandInfo("NPC Commands",
	"Set NPC Talk To Task",
	"Sets the NPC to wait for a conversation with the player. When the player talks to this NPC, the NPC Manager will send a corresponding event message back to this flowchart.")]

public class FUNGUS_command_npc_talktotask : Command {

	public ACTOR_npc_manager npcManager;

	[Tooltip("Send this event message to the Fungus flowcharts to potentially continue the flow.")]
	public string eventMessageToSend;
	[Tooltip("Associated dialogue the character should have during this task.")]
	public string yarnNode;

	public override void OnEnter(){
		if (npcManager == null) {
			npcManager = GetFlowchart().GetComponentInParent <ACTOR_npc_manager> ();
		}
		npcManager.SetTalkToTask (eventMessageToSend, yarnNode);

		Continue ();
	}
}
