/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.UI
{
    using UnityEngine;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Handles fullscreening in-app. Useful when a physical object reference is needed, such as from an event.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_FullscreenHandler : MonoBehaviour
    {
        public void SetFullscreen(bool bFullscreen)
        {
            Screen.fullScreen = bFullscreen;
        }
    }
}
