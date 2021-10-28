﻿using UnityEngine;

public class PlayerWallCheck : MonoBehaviour
{
    [SerializeField]
    float rayLength = 2f;

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
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * rayLength, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), rayLength, layermask);

        if (hit)
        {
            //Debug.Log("Hit something: " + hit.collider.name);
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