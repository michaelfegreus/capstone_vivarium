using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerStateMarker : Marker, INotification
{
    public enum PlayerState
    {
        Free,
        Menu
    }

    [SerializeField] private PlayerState setPlayerState;

    [Space(20)]
    [SerializeField] private bool retroactive = false;
    [SerializeField] private bool emitOnce = false;

    public PropertyName id => new PropertyName();
    public PlayerState SetPlayerState => setPlayerState;

    public NotificationFlags flags =>
        (retroactive ? NotificationFlags.Retroactive : default) |
        (emitOnce ? NotificationFlags.TriggerOnce : default);
}