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

    using TMPro;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Requires a TMP_Dropdown menu to work. Creates and adds resolutions available to the user at runtime and adds them to the dropdown menu.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    [RequireComponent(typeof(TMP_Dropdown))]
    public class JDH_ResolutionsHandler : MonoBehaviour
    {
        //!TODO: Add legacy support
        public TMP_Dropdown resolutionDropdown;
        Resolution[] resolutions;
        void Start()
        {
            Init();
        }

        void Init()
        {
            if (!resolutionDropdown) GetComponent<TMP_Dropdown>();

            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetResolution(int Index)
        {
            Resolution resolution = resolutions[Index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
