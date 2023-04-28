using UnityEngine;

public class Ledge : MonoBehaviour
{
    public float ledgeFallDistance;
    public bool upRightFacing;
    public GameObject ledgeCollider;

    public Vector2 ledgeJumpOverride = new Vector2(0f,0f);
}
