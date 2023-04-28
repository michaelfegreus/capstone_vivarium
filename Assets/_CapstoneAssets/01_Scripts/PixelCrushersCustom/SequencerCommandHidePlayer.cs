using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandHidePlayer: SequencerCommand
    {


        public void Awake()
        {
            PlayerManager.Instance.playerAnimation.EnableDisableCharacterSprite(false);
            Stop();
        }

    }
}