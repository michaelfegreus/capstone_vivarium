using UnityEngine;
using Fungus;

[CommandInfo("NPC Commands",
	"Hide this NPC",
	"Hides the NPC by warping this flowchart's parent (which should be the NPC) to a far away object pool location.")]

public class FUNGUS_command_npc_hide : Command {

	float objectPoolCoordinate = 99999f;

	public override void OnEnter(){
		transform.parent.position = new Vector3 (objectPoolCoordinate, objectPoolCoordinate, 0f);
		Continue ();
	}
}
