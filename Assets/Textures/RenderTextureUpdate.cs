using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureUpdate : MonoBehaviour
{
    [SerializeField] CustomRenderTexture ourTex;
    [SerializeField] int frameRate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunCustomUpdate());
    }

    // our custom update
    IEnumerator RunCustomUpdate()
    {
        // run at our framerate
        yield return new WaitForSecondsRealtime(frameRate / 1);
        // run the update
        CustomUpdate();
        // loop
        StartCoroutine(RunCustomUpdate());
    }

    void CustomUpdate()
    {
        ourTex.Update();
    }
}
