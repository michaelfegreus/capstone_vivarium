using UnityEngine;
using UnityEngine.Playables;

public class MarkerFunctions : MonoBehaviour, INotificationReceiver
{
    public void SetPlayerMenuState()
    {
        PLAYER_manager.Instance.EnterMenuState();
    }
    public void SetPlayerFreeState()
    {
        PLAYER_manager.Instance.EnterFreeState();
    }
    public void SetActivePlayerAnimationObject(bool active)
    {
        PLAYER_manager.Instance.playerAnimation.gameObject.SetActive(active);
    }
    public void SetPlayerPosition(Transform destinationPosition)
    {
        PLAYER_manager.Instance.playerMovement.playerMovementModule.transform.position = destinationPosition.transform.position;
    }


    // The following is used for custom markers
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is PlayerPositionMarker playerPositionMarker)
        {
            PLAYER_manager.Instance.playerMovement.playerMovementModule.transform.position = playerPositionMarker.Destination;
        }
        if (notification is PlayerSpriteToggleMarker playerSpriteToggleMarker)
        {
            PLAYER_manager.Instance.playerAnimation.EnableDisableCharacterSprite(playerSpriteToggleMarker.PlayerSpriteEnabled);
        }
        if (notification is PlayerStateMarker playerStateMarker)
        {
            switch (playerStateMarker.SetPlayerState)
            {
                case PlayerStateMarker.PlayerState.Free:
                    PLAYER_manager.Instance.EnterFreeState();
                    break;
                case PlayerStateMarker.PlayerState.Menu:
                    PLAYER_manager.Instance.EnterMenuState();
                    break;
            }
        }
        if (notification is PlayerDirectionMarker playerDirectionMarker)
        {
            switch (playerDirectionMarker.SetPlayerDirection)
            {
                case PlayerDirectionMarker.Direction.N:
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(0f, 1f);
                    break;
                case PlayerDirectionMarker.Direction.NE:
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(1f, 1f);
                    break;
                case PlayerDirectionMarker.Direction.E:
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(1f, 0f);
                    break;
                case PlayerDirectionMarker.Direction.SE:
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(1f, -1f);
                    break;
                case PlayerDirectionMarker.Direction.S:
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(0f, -1f);
                    break;
                case PlayerDirectionMarker.Direction.SW:
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(-1f, -1f);
                    break;
                case PlayerDirectionMarker.Direction.W:
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(-1f, 0f);
                    break;
                case PlayerDirectionMarker.Direction.NW:
                    PLAYER_manager.Instance.playerMovement.SetFaceDirection(-1f, 1f);
                    break;

            }
        }
    }
}