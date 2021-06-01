using UnityEngine;

[CreateAssetMenu()]
public class NPCActivity : ScriptableObject
{
    public Vector2 position;
    public AnimationClip activityAnimation;
    public string dialogueNode;
}
