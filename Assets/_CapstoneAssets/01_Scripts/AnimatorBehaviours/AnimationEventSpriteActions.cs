using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSpriteActions : MonoBehaviour
{
    private SpriteRenderer thisSprite;

    private void Awake()
    {
        thisSprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        EnableSprite();    
    }

    public void DisableSprite()
    {
        thisSprite.enabled = false;
    }

    public void EnableSprite()
    {
        thisSprite.enabled = true;
    }

}
