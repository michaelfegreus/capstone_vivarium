using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class StandardUISubtitlePanel_TG : StandardUISubtitlePanel
{

    //This makes it so the player can't skip ENTIRE LINES OF DIALOGUE if they push the continue button fast
    public override void ShowContinueButton()
    {
        base.ShowContinueButton();
        Tools.SetGameObjectActive(continueButton, false);
    }
}
