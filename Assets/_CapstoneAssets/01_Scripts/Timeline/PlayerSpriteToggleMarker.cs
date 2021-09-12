using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerSpriteToggleMarker : Marker, INotification
{
    [SerializeField] private bool playerSpriteEnabled;

    [Space(20)]
    [SerializeField] private bool retroactive = false;
    [SerializeField] private bool emitOnce = false;

    public PropertyName id => new PropertyName();
    public bool PlayerSpriteEnabled => playerSpriteEnabled;

    public NotificationFlags flags =>
        (retroactive ? NotificationFlags.Retroactive : default) |
        (emitOnce ? NotificationFlags.TriggerOnce : default);
}
