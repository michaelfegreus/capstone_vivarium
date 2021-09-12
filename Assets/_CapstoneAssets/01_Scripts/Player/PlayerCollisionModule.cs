using UnityEngine;

public class PlayerCollisionModule : MonoBehaviour
{

    // Event delegate system for OnTriggerEnter. This is used to send signals to the event system in Fungus.
    public delegate void TriggerEnterDelegate(Collider2D otherCollider);
    public static event TriggerEnterDelegate OnTriggerEnterEvent;

    // Event delegate system for OnTriggerExit. This is used to send signals to the event system in Fungus.
    public delegate void TriggerExitDelegate(Collider2D otherCollider);
    public static event TriggerExitDelegate OnTriggerExitEvent;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (OnTriggerEnterEvent != null)
        {
            OnTriggerEnterEvent(col);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (OnTriggerExitEvent != null)
        {
            OnTriggerExitEvent(col);
        }
    }

    }


