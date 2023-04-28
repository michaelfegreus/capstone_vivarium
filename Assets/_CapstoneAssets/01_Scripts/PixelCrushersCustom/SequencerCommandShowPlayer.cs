using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandShowPlayer: SequencerCommand
    {


        public void Awake()
        {
            PlayerManager.Instance.playerAnimation.EnableDisableCharacterSprite(true);
            Stop();
        }

    }
}