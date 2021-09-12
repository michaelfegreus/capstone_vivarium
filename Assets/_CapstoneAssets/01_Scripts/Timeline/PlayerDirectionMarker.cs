using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerDirectionMarker : Marker, INotification
{
    public enum Direction
    {
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W,
        NW
    }

    [SerializeField] private Direction setPlayerDirection;

    [Space(20)]
    [SerializeField] private bool retroactive = false;
    [SerializeField] private bool emitOnce = false;

    public PropertyName id => new PropertyName();
    public Direction SetPlayerDirection => setPlayerDirection;

    public NotificationFlags flags =>
        (retroactive ? NotificationFlags.Retroactive : default) |
        (emitOnce ? NotificationFlags.TriggerOnce : default);
}