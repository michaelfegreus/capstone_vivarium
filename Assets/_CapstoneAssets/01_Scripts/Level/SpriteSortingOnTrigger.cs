using UnityEngine;

public class SpriteSortingOnTrigger : MonoBehaviour
{
    [Tooltip("Sprites to change the sorting order of.")]
    [SerializeField]
    SpriteRenderer[] spriteToOrder;

    [SerializeField]
    int enterTargetSortLevel;
    [SerializeField]
    int exitTargetSortLevel = 0;

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Trim().Equals("Player".Trim()))
        {
            for (int i = 0; i < spriteToOrder.Length; i++)
            {
                spriteToOrder[i].sortingOrder = exitTargetSortLevel;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Trim().Equals("Player".Trim()))
        {
            for (int i = 0; i < spriteToOrder.Length; i++)
            {
                spriteToOrder[i].sortingOrder = enterTargetSortLevel;
            }
        }
    }
}
