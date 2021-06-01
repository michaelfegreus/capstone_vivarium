using UnityEngine;
using Fungus;

[CommandInfo("NPC Commands",
	"Set Next Block Time",
	"Set the time for the next block to begin.")]

public class FUNGUS_command_npc_nextblocktime : Command {

	[Tooltip("X Hours : Y Minutes")]
	public Vector2Int nextScheduleTime;

	public ACTOR_npc_manager npcManager;
	Flowchart myFlowchart;

	public override void OnEnter(){
		myFlowchart = GetFlowchart().GetComponent<Flowchart>();
		myFlowchart.SetBooleanVariable("nextBlockTrigger", false);
		if (npcManager == null) {
			npcManager = myFlowchart.GetComponentInParent <ACTOR_npc_manager> ();
		}
		npcManager.SetNextScheduleTime (nextScheduleTime.x, nextScheduleTime.y, SetTimeTrigger);
		// Also reset dialogue, just to make sure things are cleared for the next startup.
		npcManager.SetYarnNode("");

		Continue ();
	}

	public void SetTimeTrigger(){
		myFlowchart.SetBooleanVariable("nextBlockTrigger", true);
	}
}
