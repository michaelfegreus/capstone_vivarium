using UnityEngine;

public class DebugScreenshot : MonoBehaviour
{
    public KeyCode screencapInput;
    public string screencapDirectory = "DebugScreencap";

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(screencapInput))
        {
            ScreenCapture.CaptureScreenshot(screencapDirectory);
        }
    }
}
