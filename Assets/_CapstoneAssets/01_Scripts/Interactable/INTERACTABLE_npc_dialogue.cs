using UnityEngine;
using Yarn.Unity; // Can delete this if the following Monobehavior doesn't actually directly call the StartDialogue() function

public class INTERACTABLE_npc_dialogue : MonoBehaviour, IInteractable {


	// Below taken from the Yarn Spinner example script, NPC.cs
	// See original script for MIT License for open source code.

	public string characterName = "";

	[Tooltip("This node is set in runtime through narrative flowcharts.")]
	[SerializeField]
	private string talkToNode = "";

	[Tooltip("The character will default to this node when not assigned something from a flowchart.")]
	public string defaultNode = "";

	[Header("Optional")]
	public TextAsset scriptToLoad;

	static readonly string emptyString = "";

	// Use this for initialization
	void Start () {
		if (scriptToLoad != null) {
			FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
		}
	}

	public void OnInteract(){
		/// Going to want to replace this later with a reference to the Game Manager or a singleton. Searching for objects in runtime is non-ideal.
		//FindObjectOfType<DialogueRunner> ().StartDialogue (talkToNode);
		if (talkToNode != emptyString) {
			//GAME_manager.Instance.dialogRunner.StartDialogue (talkToNode);
		} else {
			//GAME_manager.Instance.dialogRunner.StartDialogue (defaultNode);
		}
	}

	public void SetTalkToNode (string newNode){
		talkToNode = newNode;
	}

	// Temporary solution for item dialogue. This system will probably need to change:
	public void ItemDialogue(string itemName){
		// Send it to the associated item node.
		//GAME_manager.Instance.dialogRunner.StartDialogue (characterName + "_item_" + itemName);
	}
}