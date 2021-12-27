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
    //public bool disableOnSuccessfulFire;
    [HideInInspector] public bool firedSuccessfully = false;

    public override void Fire(Transform actor)
    {
        base.Fire(actor);
        firedSuccessfully = true;
    }
}