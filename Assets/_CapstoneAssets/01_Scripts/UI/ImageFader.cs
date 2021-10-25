using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    [SerializeField] Image myImage;
    [SerializeField] float fadeDuration;


    public void FadeUp()
    {
        StartCoroutine(ImageFade(1f, fadeDuration));
    }

    public void FadeDown()
    {
        StartCoroutine(ImageFade(0f, fadeDuration));
    }

    public IEnumerator ImageFade(float endValue, float duration)
    {
        float startValue = myImage.color.a;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, newAlpha);
            
            yield return null;
        }
    }
}
