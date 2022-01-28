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
    /// Pans an audiosource's spatialization, once, each time when called.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class JDH_AudioPanner : MonoBehaviour
    {
        [System.Serializable]
        public class SpacializationSettings
        {
            [Tooltip("If sidechained, this tool will not PLAY the audio, but still process the AudioSource.")]
            public bool sideChain = false;
            public enum Type
            {
                Delegate, Continuous
            }
            [Tooltip("Choose delegated for event handling or on tick for continuous.")]
            public Type type = new Type();

            [Tooltip("Minimum pan to play sound at.")]
            [Range(0, 1)] public float minimumPan = 0.5f;
            [Tooltip("Maximum volume to play sound at.")]
            [Range(0, 1)] public float maximumPan = 0.5f;
        }
        public SpacializationSettings pan = new SpacializationSettings();

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
            public UnityEvent<float> OnSoundPlay;
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
            if (pan.type == SpacializationSettings.Type.Continuous) PanAudio(component.audioClip); 
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public void PanAudio(AudioClip Audio = null)
        {
            if (component.audioSource)
            {
                float RandomizedPan = Random.Range(pan.minimumPan, pan.maximumPan);
                component.audioSource.panStereo = RandomizedPan;

                events.OnSoundPlay.Invoke(RandomizedPan);
                if(!pan.sideChain && Audio) component.audioSource.PlayOneShot(Audio);
            }
            else Debug.Log("No AudioSource");
        }
    }
}

