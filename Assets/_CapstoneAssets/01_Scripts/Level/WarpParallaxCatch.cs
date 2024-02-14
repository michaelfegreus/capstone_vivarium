using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WarpParallaxCatch : MonoBehaviour
{
    [Tooltip("Make this object the focus of the parallax")]
    [SerializeField]
    CinemachineVirtualCamera myCamera;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Trim().Equals("Player".Trim()))
        {

        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Trim().Equals("Player".Trim()))
        {

        }

    }
}