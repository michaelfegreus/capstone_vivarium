using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ClockSystemVariableUpdate : MonoBehaviour
{
    private void OnEnable()
    {
        ClockManager.OnMinuteTick += UpdateDialogueSystemVariables;
    }

    private void OnDisable()
    {
        ClockManager.OnMinuteTick -= UpdateDialogueSystemVariables;
    }

    // Cache a reference to the GameManager's clock
    public ClockManager gameClockManager;

    private void Start()
    {
        gameClockManager = GameManager.Instance.clockManager;
    }

    public void UpdateDialogueSystemVariables()
    {
        DialogueLua.SetVariable("Time.Hour", gameClockManager.inGameTime.hour);
        DialogueLua.SetVariable("Time.Minute", gameClockManager.inGameTime.minute);

        // Debug.Log("Time in DialogueSystem " + DialogueLua.GetVariable("Time.Hour").asInt + ":" + DialogueLua.GetVariable("Time.Minute").asInt);

    }
}