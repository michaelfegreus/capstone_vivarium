using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class StandardUISubtitlePanel_TG : StandardUISubtitlePanel
{
    public override void ShowContinueButton()
    {
        base.ShowContinueButton();
        Tools.SetGameObjectActive(continueButton, false);
    }
}
