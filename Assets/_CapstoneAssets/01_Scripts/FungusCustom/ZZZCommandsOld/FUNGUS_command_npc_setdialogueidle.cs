using UnityEngine;
using Fungus;

[CommandInfo("NPC Commands",
	"Set NPC Dialogue to Idle",
	"Causes the NPC to call from it's idle Yarn dialogue node.")]

public class FUNGUS_command_npc_setdialogueidle : Command {

	public ACTOR_npc_manager npcManager;

	public override void OnEnter(){
		if (npcManager == null) {
			npcManager = GetFlowchart().GetComponentInParent <ACTOR_npc_manager> ();
		}
		npcManager.SetYarnNode ("");

		Continue ();
	}
}
