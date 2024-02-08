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
                    PlayerManager.Instance.playerMovement.SetStandingFaceDirection(0f, 1f);
                    break;
                case "NE":
                    PlayerManager.Instance.playerMovement.SetStandingFaceDirection(1f, 1f);
                    break;
                case "E":
                    PlayerManager.Instance.playerMovement.SetStandingFaceDirection(1f, 0f);
                    break;
                case "SE":
                    PlayerManager.Instance.playerMovement.SetStandingFaceDirection(1f, -1f);
                    break;
                case "S":
                    PlayerManager.Instance.playerMovement.SetStandingFaceDirection(0f, -1f);
                    break;
                case "SW":
                    PlayerManager.Instance.playerMovement.SetStandingFaceDirection(-1f, -1f);
                    PlayerManager.Instance.playerMovement.playerMovementModule.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -61.189f));
                    break;
                case "W":
                    PlayerManager.Instance.playerMovement.SetStandingFaceDirection(-1f, 0f);
                    break;
                case "NW":
                    PlayerManager.Instance.playerMovement.SetStandingFaceDirection(-1f, 1f);
                    break;

            }
            Debug.Log("Sequencer Forcing Direction " + direction);
            Stop();
        }
    }
}