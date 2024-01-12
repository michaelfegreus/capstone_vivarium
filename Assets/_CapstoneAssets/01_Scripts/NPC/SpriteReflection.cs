using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteReflection : MonoBehaviour
{

    public Animator anim;

    [SerializeField] private Transform reflectionTransform;

    private SpriteRenderer regularSprite;
    private SpriteRenderer reflectSprite;

    // Start is called before the first frame update
    void Start()
    {
        regularSprite = anim.GetComponent<SpriteRenderer>();
        reflectSprite = reflectionTransform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        reflectSprite.sprite = regularSprite.sprite;
    }
}
