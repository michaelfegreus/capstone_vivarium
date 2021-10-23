using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureScroll : MonoBehaviour
{
    public float scrollSpeedX = .5f;
    public float scrollSpeedY = .5f;

    Image myImage;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    private void Update()
    {

        myImage.material.mainTextureOffset = myImage.material.mainTextureOffset + new Vector2(Time.deltaTime * scrollSpeedX, Time.deltaTime * scrollSpeedY);
    }
}
