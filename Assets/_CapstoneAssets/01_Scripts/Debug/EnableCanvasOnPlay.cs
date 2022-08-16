using UnityEngine;

public class EnableCanvasOnPlay : MonoBehaviour
{
    // Use this for Editor purposes so you can keep UI canvases disabled off in the Game viewport until you start the game.

    Canvas myCanvas;

    void Start()
    {
        myCanvas = GetComponent<Canvas>();
    }
}
