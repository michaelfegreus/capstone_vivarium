using UnityEngine;

public class PLAYER_collision_delegate : MonoBehaviour
{

    public LEVEL_surfaceType.surface currentSurface;

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

        //Floor type Detection
        if (col.tag.Trim().Equals("Carpet"))
        {
            currentSurface = LEVEL_surfaceType.surface.Carpet;
        }
        if (col.tag.Trim().Equals("Wood"))
        {
            currentSurface = LEVEL_surfaceType.surface.Wood;
        }
        if (col.tag.Trim().Equals("Tile"))
        {
            currentSurface = LEVEL_surfaceType.surface.Tile;
        }
        if (col.tag.Trim().Equals("Grass"))
        {
            currentSurface = LEVEL_surfaceType.surface.Grass;
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