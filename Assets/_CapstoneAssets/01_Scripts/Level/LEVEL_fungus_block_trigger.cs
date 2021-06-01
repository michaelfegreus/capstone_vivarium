using UnityEngine;
using Fungus;

public class LEVEL_fungus_block_trigger : MonoBehaviour
{

    [Tooltip("Execute the following blocks when the Player encounters the object's Trigger Collider.")]
    [SerializeField] BlockReference[] triggerBlock;

    [SerializeField] bool triggerOnEnter = true;
    [SerializeField] bool triggerOnExit;

    Collider2D myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOnEnter)
        {
            if (collision.tag.Trim().Equals("Player".Trim()))
            {
                TriggerBlocks();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerOnExit)
        {
            if (collision.tag.Trim().Equals("Player".Trim()))
            {
                TriggerBlocks();
            }
        }
    }

    private void TriggerBlocks() {
        for (int i = 0; i < triggerBlock.Length; i++)
        {
                if (triggerBlock[i].Equals(null)){
                    Debug.LogWarning("No Fungus Block applied to LEVEL_fungus_trigger component on " + gameObject.name + " Game Object at index " + i);
                }
                else
                {
                    triggerBlock[i].Execute();
                }
            }

    }
}