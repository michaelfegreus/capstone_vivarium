using UnityEngine;

public class LEVEL_audio_trigger : MonoBehaviour
{
    /*[Header("Mute Options")]
    [Tooltip("Fade out current playing music instead of selecting from the playlist.")]
    public bool muteAreaMusic;
    [Tooltip("Fade out current playing SFX instead of selecting from the playlist.")]
    public bool muteAreaAmbientSFX;

    [Header("Playlists")]
    public LevelAudioPlaylist myAreaMusic;

    public LevelAudioPlaylist myAreaAmbientSFX;
    
    void OnTriggerEnter2D (Collider2D col) {
        if (col.tag == ("Player")){
            if (muteAreaMusic) {
                GAME_audio_manager.Instance.MuteLevelMusic();
            }
            else {
                if (myAreaMusic != null) {
                    GAME_audio_manager.Instance.AddLevelMusicPlaylist(myAreaMusic);
                }
            }
            if (muteAreaAmbientSFX) {
                GAME_audio_manager.Instance.MuteLevelAmbientSFX();
            }
            else {
                if (myAreaAmbientSFX != null) {
                    GAME_audio_manager.Instance.AddLevelAmbientSFX(myAreaAmbientSFX);
                }
            }
        }
    }*/
}

/*
 * Started writing a custom inspector that would hide the myAreaMusic and myAreaAmbientSFX properties but it got messy with Scriptable Object fields so I dropped it.
 * 
 * [CustomEditor(typeof(LEVEL_audio_trigger))]
public class LEVEL_audio_trigger_editor : Editor {
    override public void OnInspectorGUI() {
        var LEVEL_audio_trigger = target as LEVEL_audio_trigger;

        LEVEL_audio_trigger.muteArea = GUILayout.Toggle(LEVEL_audio_trigger.muteArea, "Hide Fields");

        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(LEVEL_audio_trigger.muteArea))) {
            if (group.visible == false) {
                EditorGUI.indentLevel++;
               // LEVEL_audio_trigger.myAreaMusic = EditorGUILayout.ObjectField(LEVEL_audio_trigger.myAreaMusic, typeof(LevelAudioPlaylist));
            }
        }
        
    }
}*/