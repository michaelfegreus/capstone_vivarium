using UnityEngine;

[CreateAssetMenu()]
public class NPCSchedule : ScriptableObject
{

    public ScheduledActivity[] mySchedule;

    [System.Serializable]
    public struct ScheduledActivity {
        public GameTime activityTime;
        public NPCActivity scheduledActivity;
    }
}
