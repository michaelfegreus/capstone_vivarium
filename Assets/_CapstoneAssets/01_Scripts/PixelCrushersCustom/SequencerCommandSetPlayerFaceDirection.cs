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
                    PlayerManager.Instance.playerMovement.SetFaceDirection(0f, 1f);
                    break;
                case "NE":
                    PlayerManager.Instance.playerMovement.SetFaceDirection(1f, 1f);
                    break;
                case "E":
                    PlayerManager.Instance.playerMovement.SetFaceDirection(1f, 0f);
                    break;
                case "SE":
                    PlayerManager.Instance.playerMovement.SetFaceDirection(1f, -1f);
                    break;
                case "S":
                    PlayerManager.Instance.playerMovement.SetFaceDirection(0f, -1f);
                    break;
                case "SW":
                    PlayerManager.Instance.playerMovement.SetFaceDirection(-1f, -1f);
                    break;
                case "W":
                    PlayerManager.Instance.playerMovement.SetFaceDirection(-1f, 0f);
                    break;
                case "NW":
                    PlayerManager.Instance.playerMovement.SetFaceDirection(-1f, 1f);
                    break;

            }
            Debug.Log("Sequencer Forcing Direction " + direction);
            Stop();
        }
    }
}