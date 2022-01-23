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
    /// Handles VSync in-app. Useful when a physical object reference is needed, such as from an event.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_VSyncHandler : MonoBehaviour
    {
        public void SetVSync(bool bVSync)
        {
            if(bVSync)  QualitySettings.vSyncCount = 1;
            else  QualitySettings.vSyncCount = 0;
        }
    }
}
