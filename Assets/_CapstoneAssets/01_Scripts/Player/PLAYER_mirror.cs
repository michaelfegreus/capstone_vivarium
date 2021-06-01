using UnityEngine;

public class PLAYER_mirror : MonoBehaviour
{
    /*public Transform playerAnimation;
    public Transform mirrorPos;

    Vector3 targetTransform;

    // Update is called once per frame
    void Update()
    {
        float C = Mathf.Atan(playerAnimation.position.y / playerAnimation.position.x); // Player angle to origin
        float D = Mathf.PI / 6f - C;
        float E = Mathf.Sqrt(Mathf.Pow(playerAnimation.position.x, 2f) + Mathf.Pow(playerAnimation.position.y, 2f));
        float F = Mathf.PI / 2f - D;
        float G = E * Mathf.Cos(F);
       // transform.localPosition = new Vector3(G, 0f, 0f);
        float H = Mathf.PI / 3f - D;
        float newY = E * Mathf.Cos(H);
        float newX = E * Mathf.Sin(H);

        transform.position = new Vector3(newX, newY, 0f);

    }*/
    
    public Transform ObjA;
    public Transform ObjB;

    public Transform MirrorPoint;

    private void Update()
    {
        ObjB.position = Vector3.LerpUnclamped(ObjA.position, MirrorPoint.position, 2f);
    }
}
