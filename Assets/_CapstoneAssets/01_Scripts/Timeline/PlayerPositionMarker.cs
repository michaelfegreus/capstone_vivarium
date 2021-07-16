using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerPositionMarker : Marker, INotification
{
    [SerializeField] private Vector3 destination;

    [Space(20)]
    [SerializeField] private bool retroactive = false;
    [SerializeField] private bool emitOnce = false;

    public PropertyName id => new PropertyName();
    public Vector3 Destination => destination;

    public NotificationFlags flags =>
        (retroactive ? NotificationFlags.Retroactive : default) |
        (emitOnce ? NotificationFlags.TriggerOnce : default);
}
