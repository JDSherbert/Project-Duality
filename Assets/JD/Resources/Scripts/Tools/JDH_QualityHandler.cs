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
    /// Sets a quality based on a passed in integer. Requires a physical object reference because of events.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_QualityHandler : MonoBehaviour
    {
        public void SetQuality(int Quality)
        {
            QualitySettings.SetQualityLevel(Quality);
        }
    }
}
