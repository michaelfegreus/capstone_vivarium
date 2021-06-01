using UnityEngine;
using Fungus;
using Yarn.Unity;

public class FUNGUS_variable_manager : MonoBehaviour {

	public Flowchart myFlowchart;

	//-----------------------------------------------------------------------------------------
	// YarnSpinner Public Methods:
	//-----------------------------------------------------------------------------------------

	/* Make sure to call from the corresponding Game Objects. i.e:
	 * Yarn:	
	 *	 <<move Sally exit>>
	 * 
	 * Unity:
			YarnCommand("move")]
				public void MoveCharacter(string destinationName) {
				// move to the destination
			}
	*/


        /*
	// Set a variable in Yarn based on a fungus variable. The command creates the Yarn variable if it doesn't already exist.
	[YarnCommand("GetGlobalVariable")]
	public void GetGlobalVariable(string variableType, string newYarnVariableName, string fungusVariableName){

		// If the first character is not a $, set that up. I always forget to put the stupid thing onto Yarn variables.
		if(newYarnVariableName[0].Equals('$') == false){
			newYarnVariableName = "$" + newYarnVariableName;
		}

		// Depending on the variable type, get that from Fungus flowchart.
		switch (variableType) {
		case "int":
			GAME_manager.Instance.variableStorageManager.SetValue (newYarnVariableName, new Yarn.Value (myFlowchart.GetIntegerVariable(fungusVariableName)));
			break;
		case "float":
			GAME_manager.Instance.variableStorageManager.SetValue (newYarnVariableName, new Yarn.Value (myFlowchart.GetFloatVariable (fungusVariableName)));
			break;
		case "string":
			GAME_manager.Instance.variableStorageManager.SetValue (newYarnVariableName, new Yarn.Value (myFlowchart.GetStringVariable (fungusVariableName)));
			break;
		case "bool":
			GAME_manager.Instance.variableStorageManager.SetValue (newYarnVariableName, new Yarn.Value (myFlowchart.GetBooleanVariable (fungusVariableName)));
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}
	}

	// Set a global variable created in Fungus from a command in Yarn.
	// TODO: Test this command!
	[YarnCommand("SetGlobalVariable")]
	public void SetGlobalVariable(string fungusVariableName, string yarnVariableType, string yarnVariableValue){

		switch (yarnVariableType) {
		case "int":
			myFlowchart.SetIntegerVariable (fungusVariableName, int.Parse (yarnVariableValue));
			break;
		case "float":
			myFlowchart.SetFloatVariable (fungusVariableName, float.Parse (yarnVariableValue));
			break;
		case "string":
			myFlowchart.SetStringVariable (fungusVariableName, yarnVariableValue);
			break;
		case "bool":
			// ** Note: value HAS to be written as one of the following: True, False, 1, 0
			myFlowchart.SetBooleanVariable (fungusVariableName, bool.Parse (yarnVariableValue));
			break;
		default:
			throw new System.ArgumentOutOfRangeException (); 
		}
	}*/
}
