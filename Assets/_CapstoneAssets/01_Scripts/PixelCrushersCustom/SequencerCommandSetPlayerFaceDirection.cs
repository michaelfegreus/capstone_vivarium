using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandSetPlayerFaceDirection : SequencerCommand
    {

        public void Awake()
        {
            string direction = GetParameter(0);
            direction = direction.ToUpper(); //  Make them all upper case for ease of use and to reduce human error.

            switch (direction)
            {
                case "N":
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(0f, 1f);
                    break;
                case "NE":
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(1f, 1f);
                    break;
                case "E":
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(1f, 0f);
                    break;
                case "SE":
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(1f, -1f);
                    break;
                case "S":
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(0f, -1f);
                    break;
                case "SW":
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(-1f, -1f);
                    break;
                case "W":
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(-1f, 0f);
                    break;
                case "NW":
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(-1f, 1f);
                    break;

            }
        }
    }
}