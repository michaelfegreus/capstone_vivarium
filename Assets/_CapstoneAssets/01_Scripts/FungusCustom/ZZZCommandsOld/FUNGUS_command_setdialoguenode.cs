using UnityEngine;
using Fungus;

[CommandInfo("NPC Commands",
	"Set NPC Dialogue Node",
	"Sets the Yarn Node that the NPC will call dialogue from.")]

public class FUNGUS_command_npc_setdialoguenode : Command {

	public ACTOR_npc_manager npcManager;

	public string yarnNode;

	public override void OnEnter(){
		if (npcManager == null) {
			npcManager = GetFlowchart().GetComponentInParent <ACTOR_npc_manager> ();
		}
		npcManager.SetYarnNode (yarnNode);

		Continue ();
	}
}
