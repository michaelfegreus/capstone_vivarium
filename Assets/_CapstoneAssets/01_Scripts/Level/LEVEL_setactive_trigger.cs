using UnityEngine;

public class LEVEL_setactive_trigger : MonoBehaviour
{
    [Tooltip("Set these objects active when the player enters the trigger, and set them inactive when the player exits the trigger.")]
    [SerializeField]
    GameObject[] levelObjects;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Trim().Equals("Player".Trim()))
        {
            for (int i = 0; i < levelObjects.Length; i++)
            {
                levelObjects[i].SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Trim().Equals("Player".Trim()))
        {
            for (int i = 0; i < levelObjects.Length; i++)
            {
                levelObjects[i].SetActive(false);
            }
        }
    }

}