using PixelCrushers.DialogueSystem;
using UnityEngine.Events;
using Opsive.Shared.Utility;
using Opsive.UltimateInventorySystem.Core.DataStructures;
using Opsive.UltimateInventorySystem.Exchange;
using Opsive.UltimateInventorySystem.ItemActions;
using Opsive.UltimateInventorySystem.Storage;
using System;
using UnityEngine;

public class TimeDialogueSystemTrigger : DialogueSystemTrigger
{
    [HideInInspector] public bool firedSuccessfully = false;

    // Cache a reference to the GameManager's clock
    protected ClockManager gameClockManager;

    // Alter the base variable so it's default is the "None" enum
    // public new DialogueSystemTriggerEvent trigger = DialogueSystemTriggerEvent.None;

    public new void OnEnable()
    {
        // Like, comment, subscribe
        ClockManager.OnMinuteTick += TryStartTimedEvent;
        // Try to run the event when enabling
        TryStartTimedEvent();
        // Reset fire
        firedSuccessfully = false;

        // The below is copied from the base class -- base class did not set OnEnable/OnDisable as virtual voids so I could not override them as I normally would
        PersistentDataManager.RegisterPersistentData(gameObject);
        listenForOnDestroy = true;
        // Waits one frame to allow all other components to finish their OnEnable() methods.
        if (trigger == DialogueSystemTriggerEvent.OnEnable) StartCoroutine(StartAtEndOfFrame());
    }

    public void TryStartTimedEvent()
    {
        // Only run if it hasn't already successfully triggered
        if (!firedSuccessfully)
        {
            TryStart(null);
        }
    }

    public new void OnDisable()
    {
        // Disiked, deleted comment, unsubbed, reported
        ClockManager.OnMinuteTick -= TryStartTimedEvent;

        // The below is copied from the base class -- base class did not set OnEnable/OnDisable as virtual voids so I could not override them as I normally would
        StopMonitoringConversationDistance();
        StopAllCoroutines();
        PersistentDataManager.UnregisterPersistentData(gameObject);
        if (listenForOnDestroy && trigger == DialogueSystemTriggerEvent.OnDisable) TryStart(null);
    }

    public override void Fire(Transform actor)
    {
        base.Fire(actor);
        firedSuccessfully = true;
    }
}