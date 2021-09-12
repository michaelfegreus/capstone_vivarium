using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandSetPlayerState: SequencerCommand
    {


        public void Awake()
        {
            string state = GetParameter(0);
            state = state.ToUpper(); //  Make them all upper case for ease of use and to reduce human error.

            switch (state)
            {
                case "FREE":
                    PlayerManager.Instance.EnterFreeState();
                    break;
                case "MENU":
                    PlayerManager.Instance.EnterMenuState();
                    break;
            }
            Stop();
        }

    }
}