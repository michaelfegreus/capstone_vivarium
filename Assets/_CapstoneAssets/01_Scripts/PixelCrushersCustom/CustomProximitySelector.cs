using PixelCrushers.DialogueSystem;
using UnityEngine.Events;

public class CustomProximitySelector : ProximitySelector
{
    // This code was generously written by Tony Li as a means to run events when a Proximity Selector uses something.

    public UnityEvent onUse;

    public override void UseCurrentSelection()
    {
        base.UseCurrentSelection();
        onUse.Invoke();
    }
}