using UnityEngine;

public class SetActiveOnTrigger : MonoBehaviour
{
    [Tooltip("Set these objects to Inactive on Start.")]
    [SerializeField]
    bool startObjectsInactive;

    [Tooltip("Set these objects active when the player enters the trigger, and set them inactive when the player exits the trigger.")]
    [SerializeField]
    GameObject[] levelObjects;

    private void Start()
    {
        foreach(GameObject element in levelObjects)
        {
            element.SetActive(false);
        }
    }

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