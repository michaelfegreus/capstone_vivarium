using UnityEngine;

public class PlayerWallCheck : MonoBehaviour
{
    [SerializeField]
    float rayLength = 4f;

    int layermask;

    Ray ray;
    RaycastHit2D rayHit;

    public GameObject collidingWall;

    public bool stickingWall;

    private void Start()
    {
        layermask = LayerMask.GetMask("LevelCollision");
    }

    private void Update()
    {
        // set to the updated position of the player
        transform.position = PlayerCollisionModule.instance.transform.position;

        // get our movement direction

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * rayLength, Color.red);

        // original
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), rayLength, layermask);
        // new
        // hit = Physics2D.CircleCast(transform.position, 2, transform.TransformDirection(Vector2.down), rayLength, layermask);

        if (hit)
        {
            collidingWall = hit.collider.gameObject;
            stickingWall = true;
        }
        else
        {
            stickingWall = false;
        }
    }

    private void OnDisable()
    {
        stickingWall = false;
        collidingWall = null;
    }
}
