/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Audio
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Handles playing music in the game. Atatch to a physical object.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class JDH_MusicPlayerComponent : MonoBehaviour
    {
        public List<AudioClip> Music = new List<AudioClip>();
        int currentSelection = 0;

        public AudioSource audioSource;

        [System.Serializable]
        public class Events
        {
            public UnityEvent<int> OnSelectionChanged;
            public UnityEvent<AudioClip> OnPlayMusic;
        }
        public Events events = new Events();

        public void PlayMusic()
        {
            PlayMusic(currentSelection);
        }
        public void PlayMusic(int Selection)
        {
            ChangeSelection(Selection);
            audioSource.clip = Music[currentSelection];
            audioSource.Play();
            events.OnPlayMusic.Invoke(Music[currentSelection]);
        }
        
        public void StopMusic()
        {
            audioSource.Stop();
        }

        public void ChangeSelection(int Selection = 0)
        {
            Selection = Mathf.Clamp(Selection, 0, Music.Count);
            currentSelection = Selection;
            events.OnSelectionChanged.Invoke(currentSelection);
        }
    }
}
