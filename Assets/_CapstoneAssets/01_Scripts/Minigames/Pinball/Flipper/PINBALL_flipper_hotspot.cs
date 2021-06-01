using UnityEngine;

public class PINBALL_flipper_hotspot : MonoBehaviour
{
    [HideInInspector] public bool pinballInHotspot;

    [Tooltip("Added forward thrust to flip. Positive force if going right, negative force if going left.")]
    public float flipperForwardThrust;

    [Tooltip("Added upward to flip.")]
    public float flipperUpwardThrust;

    // Trigger volumes just for this hot spot.

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Trim().Equals("Pinball".Trim()))
        {
            pinballInHotspot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Trim().Equals("Pinball".Trim()))
        {
            pinballInHotspot = false;
        }
    }
}
