/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Application
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Application Operations
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public static class JDH_ApplicationManager
    {
        public const int LOADINGSCREENBUILDINDEX = 1;
        public static Scene NextScenePayload;

        public static void OpenURLPayload(int Index = 0)
        {
            Index = Mathf.Clamp(Index, 0, JDH_ExternalLinks.URL_Payloads.Length);
            OpenURLPayload(JDH_ExternalLinks.URL_Payloads[Index]);
        }
        public static void OpenURLPayload(string CustomURL)
        {
            Debug.Log("Opening: " + CustomURL);
            if(CustomURL != null) Application.OpenURL(CustomURL);
        }

        public static void QuitApplication()
        {
            Debug.Log("Closing Application...");

            #if UNITY_STANDALONE
            Application.Quit();
            #endif

            #if UNITY_EDITOR
		    UnityEditor.EditorApplication.isPlaying = false;
	        #endif
        }

        //? Load Async Operations
        public static void LoadSceneAsync(int BuildIndex = 0)
        {
            BuildIndex = Mathf.Clamp(BuildIndex, 0, SceneManager.sceneCountInBuildSettings);
            NextScenePayload = SceneManager.GetSceneByBuildIndex(BuildIndex);
            SceneManager.LoadSceneAsync(JDH_ApplicationManager.LOADINGSCREENBUILDINDEX);
        }
        public static void LoadSceneAsync(string SceneName)
        {
            NextScenePayload = SceneManager.GetSceneByName(SceneName);
            SceneManager.LoadSceneAsync(JDH_ApplicationManager.LOADINGSCREENBUILDINDEX);
        }

        //? Load Direct operations
        public static void ForceLoadLevel(int BuildIndex = 0)
        {
            BuildIndex = Mathf.Clamp(BuildIndex, 0, SceneManager.sceneCountInBuildSettings);
            Scene scene = SceneManager.GetSceneByBuildIndex(BuildIndex);
            ForceLoadLevel(scene.name);
        }
        public static void ForceLoadLevel(string SceneName)
        {
            if(SceneManager.GetSceneByName(SceneName) != null) SceneManager.LoadScene(SceneName);
        }
        public static void ForceRestartLevel()
        {
            ForceLoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
