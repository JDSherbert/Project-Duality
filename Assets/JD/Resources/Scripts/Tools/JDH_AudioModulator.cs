/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace SherbertSuite.Tools.Audio
{
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________
    /// Modulates an audiosource's pitch and volume, once, each time when called.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class JDH_AudioModulator : MonoBehaviour
    {
        [System.Serializable]
        public class ModulatorSettings
        {
            [Tooltip("If sidechained, this tool will not PLAY the audio, but still process the AudioSource.")]
            public bool sideChain = false;
            public enum Type
            {
                Delegate, Continuous
            }
            [Tooltip("Choose delegated for event handling or on tick for continuous.")]
            public Type type = new Type();

            [Tooltip("Minimum volume to play sound at.")]
            [Range(0, 1)] public float minimumVolume = 1.0f;
            [Tooltip("Maximum volume to play sound at.")]
            [Range(0, 1)] public float maximumVolume = 1.0f;
            [Tooltip("Minimum pitch to play sound at.")]
            [Range(0, 3)] public float minimumPitch = 1.0f;
            [Tooltip("Maximum pitch to play sound at.")]
            [Range(0, 3)] public float maximumPitch = 1.0f;
        }
        public ModulatorSettings modulator = new ModulatorSettings();

        [System.Serializable]
        public class Components
        {
            [Tooltip("REQUIRED.")]
            public AudioSource audioSource;
            [Tooltip("REQUIRED FOR NON DELEGATE.")]
            public AudioClip audioClip;
        }
        public Components component = new Components();

        [System.Serializable]
        public class Events
        {
            public UnityEvent<float, float> OnSoundPlay;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // MonoBehaviour Methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            if (GetComponent<AudioSource>()) component.audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (modulator.type == ModulatorSettings.Type.Continuous) ModulateAudio(component.audioClip); 
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public void ModulateAudio(AudioClip Audio = null)
        {
            if (component.audioSource)
            {
                float RandomizedVolume = Random.Range(modulator.minimumVolume, modulator.maximumVolume);
                float RandomizedPitch = Random.Range(modulator.minimumPitch, modulator.maximumPitch);
                component.audioSource.volume = RandomizedVolume;
                component.audioSource.pitch = RandomizedPitch;

                events.OnSoundPlay.Invoke(RandomizedVolume, RandomizedPitch);
                if(!modulator.sideChain && Audio) component.audioSource.PlayOneShot(Audio);
            }
            else Debug.Log("No AudioSource");
        }
    }
}
