using UnityEngine;
using UnityEngine.Events;

public class LEVEL_enable_events : MonoBehaviour
{
    public UnityEvent EnableEvent;
    public UnityEvent DisableEvent;

    private void OnEnable()
    {
        EnableEvent.Invoke();
    }
    private void OnDisable()
    {
        DisableEvent.Invoke();
    }
}
