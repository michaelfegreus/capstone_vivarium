using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Stops the currently playing game music.
    /// </summary>
    [CommandInfo("Audio",
                 "Fade Out Music",
                 "Fades out the currently playing game music, then stops it.")]
    [AddComponentMenu("")]
    public class FadeOutMusic : Command
    {
        #region Public members

        [Tooltip("Time it takes to fade out in seconds.")]
        [SerializeField] protected float fadeTime = 1f;

        public override void OnEnter()
        {
            var musicManager = FungusManager.Instance.MusicManager;

            musicManager.FadeOutMusic(fadeTime);

            Continue();
        }

        public override Color GetButtonColor()
        {
            return new Color32(242, 209, 176, 255);
        }

        #endregion
    }
}