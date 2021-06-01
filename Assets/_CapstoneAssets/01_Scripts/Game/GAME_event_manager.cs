using UnityEngine;
using Yarn.Unity;

public class GAME_event_manager : MonoBehaviour
{

    DigitalSalmon.Fade.FadePostProcess screenFade;

    [Header("Sleep and New Day Variables")]
    public Transform startDayLocation;
    public Vector2 startDayFaceDirection;

    void Start()
    {
        screenFade = Camera.main.gameObject.GetComponent<DigitalSalmon.Fade.FadePostProcess>();

    }


    /*void Update(){ // Test functions here.
        if (Input.GetKeyDown (KeyCode.Q)) {
            screenFade.FadeUp ();
        }
        if (Input.GetKeyDown (KeyCode.T)) {
            screenFade.FadeDown();
        }
    }*/

    //-----------------------------------------------------------------------------------------
    // YarnSpinner Public Methods:
    //-----------------------------------------------------------------------------------------

    // The player character sleeps until morning.
    [YarnCommand("SleepEvent")]
    public void SleepEvent()
    {
        /*
        // Target the player's location for this fade.
        // TODO: (May find that creating targeted fades is a useful function that can be accessed more than just here.)
        Vector3 screenPos = Camera.main.WorldToScreenPoint(PLAYER_manager.Instance.playerAnimation.targetTransform.position);

        Material mat = screenFade.CurrentEffect.BaseMaterial;
        mat.SetVector ("[Radial] Anchor", new Vector4( 1f, 1f, 0, 0));
        Debug.Log(mat.GetVector("[Radial] Anchor"));*/

        screenFade.FadeDown(false, OnSleepFadedDown);


    } // the below function is used in conjunction with Sleep Event
    public void OnSleepFadedDown()
    {
        Debug.Log("Sleep complete!");
        // Set player location and the direction they'll begin facing.
        if (startDayLocation != null)
        {
            PLAYER_manager.Instance.playerMovement.playerMovementModule.transform.position = startDayLocation.position;
            PLAYER_manager.Instance.playerMovement.SetFaceDirection(startDayFaceDirection.x, startDayFaceDirection.y);
        }
        WORLD_manager.Instance.timeManager.SetNewTime(8, 0, true); // TODO: WAKE UP TIMES subject to change -- Should put these in articy:draft
        Camera.main.gameObject.GetComponent<DigitalSalmon.Fade.FadePostProcess>().FadeUp();

    }

    /* No longer using articy : draft.
    // Set a variable in Yarn based on an articy:draft variable. The command creates the Yarn variable if it doesn't already exist.
    [YarnCommand("GetGlobalVariable")]
    public void GetGlobalVariable(string variableType, string yarnVariableName, string articyVariableName){

        // If the first character is not a $, set that up. I always forget to put the stupid thing onto Yarn variables.
        if(yarnVariableName[0].Equals('$') == false){
            yarnVariableName = "$" + yarnVariableName;
        }

        // Depending on the variable type, get that from articy:draft
        switch (variableType) {
        case "int":
            GAME_manager.Instance.variableStorageManager.SetValue (yarnVariableName, new Yarn.Value (ArticyDatabase.DefaultGlobalVariables.GetVariableByString <int> (articyVariableName)));
            break;
        case "float":
            GAME_manager.Instance.variableStorageManager.SetValue (yarnVariableName, new Yarn.Value (ArticyDatabase.DefaultGlobalVariables.GetVariableByString <float> (articyVariableName)));
            break;
        case "string":
            GAME_manager.Instance.variableStorageManager.SetValue (yarnVariableName, new Yarn.Value (ArticyDatabase.DefaultGlobalVariables.GetVariableByString <string> (articyVariableName)));
            break;
        case "bool":
            GAME_manager.Instance.variableStorageManager.SetValue (yarnVariableName, new Yarn.Value (ArticyDatabase.DefaultGlobalVariables.GetVariableByString <bool> (articyVariableName)));
            break;
        default:
            throw new System.ArgumentOutOfRangeException ();
        }
            


    }

    // Set a global variable created in articy:draft from a command in Yarn.
    // TODO: Test this command!
    [YarnCommand("SetGlobalVariable")]
    public void SetGlobalVariable(string articyVariableName, string variableType, string variableValue){
        
        switch (variableType) {
        case "int":
            ArticyDatabase.DefaultGlobalVariables.SetVariableByString (articyVariableName, int.Parse (variableValue));
            break;
        case "float":
            ArticyDatabase.DefaultGlobalVariables.SetVariableByString (articyVariableName, float.Parse (variableValue));
            break;
        case "string":
            ArticyDatabase.DefaultGlobalVariables.SetVariableByString (articyVariableName, variableValue);
            break;
        case "bool":
            // ** Note: value HAS to be written as one of the following: True, False, 1, 0
            ArticyDatabase.DefaultGlobalVariables.SetVariableByString (articyVariableName, bool.Parse (variableValue));
            break;
        default:
            throw new System.ArgumentOutOfRangeException (); 
        }
    }*/

        /*
    [YarnCommand("DebugPrint")]
    public void DebugPrint(string lineToPrint)
    {
        // Accepts a string from Yarn and prints it on the Unity debug log.
        Debug.Log(lineToPrint);
    }
    [YarnCommand("CheckPlayerInventoryForItem")]
    public void CheckPlayerInventoryForItem(string itemName)
    {
        // Compare names with this. Need to call Inventory Manager and then have it look through the current items.
        if (GAME_inventory_manager.Instance.CompareItemNames(itemName))
        {
            Debug.Log("Found the item that the NPC dialogue is searching for!");
            // Set up a Yarn value to send to the Yarn Variable Storage, which will be sent into the YarnSpinner dialogue tree.
            var valueToSet = new Yarn.Value("true");
            GAME_manager.Instance.variableStorageManager.SetValue("$playerHasItem_" + itemName, valueToSet);
        }
        else
        {
            Debug.Log("Did not find the item that the NPC dialogue is searching for.");
        }
    }
    [YarnCommand("TakeItemFromPlayerInventory")]
    public void TakeItemFromPlayerInvetory(string itemName)
    {
        // Compare names of inventory items to the itemName string and remove the first associated item found.
        GAME_inventory_manager.Instance.RemoveItemByName(itemName);
    }*/
}