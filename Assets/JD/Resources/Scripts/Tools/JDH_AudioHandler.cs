/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Audio;

    using TMPro;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Handles audio in-app. Useful when a physical object reference is needed, such as from an event.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_AudioHandler : MonoBehaviour
    {
        public const string MASTER = "Volume [Master]";
        public const string SFX = "Volume [SFX]";
        public const string UI = "Volume [UI]";
        public const string MUSIC = "Volume [Music]";

        public AudioMixer audioMixer;

        public void SetMasterBusVolume(float NewVolume)
        {
            audioMixer.SetFloat(MASTER, NewVolume);
        }
        public void SetSFXBusVolume(float NewVolume)
        {
            audioMixer.SetFloat(SFX, NewVolume);
        }
        public void SetUIBusVolume(float NewVolume)
        {
            audioMixer.SetFloat(UI, NewVolume);
        }
        public void SetMusicBusVolume(float NewVolume)
        {
            audioMixer.SetFloat(MUSIC, NewVolume);
        }
    }
}
