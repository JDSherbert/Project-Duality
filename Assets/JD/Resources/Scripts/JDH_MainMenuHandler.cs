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

    using Sherbert.Application;
    
    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Main menu component that handles calls to application manager from buttons or other UI elements that require a physical object instance.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_MainMenuHandler : MonoBehaviour
    {
        public void PlayGame(int FirstLevel)
        {
            JDH_ApplicationManager.LoadSceneAsync(FirstLevel);
        }

        public void QuitGame()
        {
            JDH_ApplicationManager.QuitApplication();
        }

        public void LoadURL(int Index = 0)
        {
            JDH_ApplicationManager.OpenURLPayload(JDH_ExternalLinks.URL_Payloads[Index]);
        }
    }
}
