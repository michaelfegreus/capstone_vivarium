/// ---------------------------------------------
/// Opsive Shared
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.Shared.Integrations.MasterAudio
{
    using Opsive.Shared.Audio;
    using UnityEngine;

    /// <summary>
    /// Implements the AudioManagerModule for the Master Audio integration.
    /// </summary>
    [CreateAssetMenu(fileName = "MasterAudioManagerModule", menuName = "Opsive/Audio/Master Audio Manager Module", order = 2)]
    public class MasterAudioManagerModule : AudioManagerModule
    {
        /// Plays the audio clip.
        /// </summary>
        /// <param name="gameObject">The GameObject that the AudioClip should be played on.</param>
        /// <param name="audioClipInfo">The AudioClipInfo that should be played.</param>
        /// <returns>The result of playing the AudioClip.</returns>
        public override PlayResult PlayAudio(GameObject gameObject, AudioClipInfo audioClipInfo)
        {
            var volume = audioClipInfo.AudioModifier.VolumeOverride.ValueOverride != FloatOverride.Override.NoOverride ? audioClipInfo.AudioModifier.VolumeOverride.Value : 1;
            float? pitch = null;
            if (audioClipInfo.AudioModifier.PitchOverride.ValueOverride != FloatOverride.Override.NoOverride) {
                pitch = audioClipInfo.AudioModifier.PitchOverride.Value;
            }
            DarkTonic.MasterAudio.MasterAudio.PlaySound3DAtTransformAndForget(audioClipInfo.AudioClip.name, gameObject.transform, volume, pitch);
            return new PlayResult();
        }

        /// <summary>
        /// Plays the audio clip at the specified position.
        /// </summary>
        /// <param name="gameObject">The GameObject that the AudioClip should be played on.</param>
        /// <param name="audioClipInfo">The AudioClipInfo that should be played.</param>
        /// <param name="position">The position that the AudioClip should play at.</param>
        /// <returns>The result of playing the AudioClip.</returns>
        public override PlayResult PlayAtPosition(GameObject gameObject, AudioClipInfo audioClipInfo, Vector3 position)
        {
            var volume = audioClipInfo.AudioModifier.VolumeOverride.ValueOverride != FloatOverride.Override.NoOverride ? audioClipInfo.AudioModifier.VolumeOverride.Value : 1;
            float? pitch = null;
            if (audioClipInfo.AudioModifier.PitchOverride.ValueOverride != FloatOverride.Override.NoOverride) {
                pitch = audioClipInfo.AudioModifier.PitchOverride.Value;
            }
            DarkTonic.MasterAudio.MasterAudio.PlaySound3DAtVector3AndForget(audioClipInfo.AudioClip.name, position, volume, pitch);
            return new PlayResult();
        }

        /// <summary>
        /// Stops playing the audio on the specified GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject that contains the playing AudioSource.</param>
        /// <param name="audioConfig">The AudioConfig that should be stopped.</param>
        /// <returns>The AudioSource that was stopped playing (can be null).</returns>
        public override AudioSource Stop(GameObject gameObject, AudioConfig audioConfig)
        {
            DarkTonic.MasterAudio.MasterAudio.StopAllSoundsOfTransform(gameObject.transform);
            return null;
        }

        /// <summary>
        /// Stops playing the audio on the specified GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject that contains the playing AudioSource.</param>
        /// <param name="playResult">The PlayResult from when the audio was played.</param>
        /// <returns>The AudioSource that was stopped playing (can be null).</returns>
        public override AudioSource Stop(GameObject gameObject, PlayResult playResult)
        {
            DarkTonic.MasterAudio.MasterAudio.StopAllSoundsOfTransform(gameObject.transform);
            return null;
        }
    }
}