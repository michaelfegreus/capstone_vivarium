using UnityEngine;
using UnityEngine.Playables;

public class MarkerFunctions : MonoBehaviour, INotificationReceiver
{
    public void SetPlayerMenuState()
    {
        PlayerManager.Instance.EnterMenuState();
    }
    public void SetPlayerFreeState()
    {
        PlayerManager.Instance.EnterFreeState();
    }
    public void SetActivePlayerAnimationObject(bool active)
    {
        PlayerManager.Instance.playerAnimation.gameObject.SetActive(active);
    }
    public void SetPlayerPosition(Transform destinationPosition)
    {
        PlayerManager.Instance.playerMovement.playerMovementModule.transform.position = destinationPosition.transform.position;
    }


    // The following is used for custom markers
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is PlayerPositionMarker playerPositionMarker)
        {
            PlayerManager.Instance.playerMovement.playerMovementModule.transform.position = playerPositionMarker.Destination;
        }
        if (notification is PlayerSpriteToggleMarker playerSpriteToggleMarker)
        {
            PlayerManager.Instance.playerAnimation.EnableDisableCharacterSprite(playerSpriteToggleMarker.PlayerSpriteEnabled);
        }
        if (notification is PlayerStateMarker playerStateMarker)
        {
            switch (playerStateMarker.SetPlayerState)
            {
                case PlayerStateMarker.PlayerState.Free:
                    PlayerManager.Instance.EnterFreeState();
                    break;
                case PlayerStateMarker.PlayerState.Menu:
                    PlayerManager.Instance.EnterMenuState();
                    break;
            }
        }
        if (notification is PlayerDirectionMarker playerDirectionMarker)
        {
            switch (playerDirectionMarker.SetPlayerDirection)
            {
                case PlayerDirectionMarker.Direction.N:
                    PlayerManager.Instance.playerMovement.SetFaceDirection(0f, 1f);
                    break;
                case PlayerDirectionMarker.Direction.NE:
                    PlayerManager.Instance.playerMovement.SetFaceDirection(1f, 1f);
                    break;
                case PlayerDirectionMarker.Direction.E:
                    PlayerManager.Instance.playerMovement.SetFaceDirection(1f, 0f);
                    break;
                case PlayerDirectionMarker.Direction.SE:
                    PlayerManager.Instance.playerMovement.SetFaceDirection(1f, -1f);
                    break;
                case PlayerDirectionMarker.Direction.S:
                    PlayerManager.Instance.playerMovement.SetFaceDirection(0f, -1f);
                    break;
                case PlayerDirectionMarker.Direction.SW:
                    PlayerManager.Instance.playerMovement.SetFaceDirection(-1f, -1f);
                    break;
                case PlayerDirectionMarker.Direction.W:
                    PlayerManager.Instance.playerMovement.SetFaceDirection(-1f, 0f);
                    break;
                case PlayerDirectionMarker.Direction.NW:
                    PlayerManager.Instance.playerMovement.SetFaceDirection(-1f, 1f);
                    break;

            }
        }
    }
}