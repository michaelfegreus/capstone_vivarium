using UnityEngine;
using UnityEngine.Events;

public class ObjectEnableEvents : MonoBehaviour
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
