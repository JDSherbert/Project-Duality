/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Systems
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.SceneManagement;

    using Sherbert.Application;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________
    /// Loads a level, asynchronously. Can be performed as a background operation, allowing a loading screen to be deisplayed.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_LoadScreenHandler : MonoBehaviour
    {
        [System.Serializable]
        public class LoadSettings
        {
            public const float ASYNCDELAY = 5.0f;

            [Tooltip("Amount of progression towards the load.")]
            public float progress = 0.0f;
            [Tooltip("Scene Payload to load.")]
            public Scene scenePayload;
        }
        public LoadSettings loader = new LoadSettings();

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnLoadOpStart;
            public UnityEvent<float> OnLoadOpUpdate;
            public UnityEvent<Scene> OnLoadOpComplete;
        }
        public Events events = new Events();

        void Start()
        {
            Init();
        }


        public IEnumerator LoadAsyncOperation()
        {
            loader.scenePayload = SceneManager.GetActiveScene();
            yield return new WaitForSeconds(1);

            if (loader.scenePayload != null || loader.scenePayload.buildIndex > -1)
            {
                AsyncOperation gamelevel = SceneManager.LoadSceneAsync(loader.scenePayload.buildIndex);
                loader.progress = gamelevel.progress;

                yield return new WaitForSeconds(LoadSettings.ASYNCDELAY);

                while (loader.progress < 1)
                {
                    if (loader.progress != gamelevel.progress) events.OnLoadOpUpdate.Invoke(gamelevel.progress);
                    if (gamelevel.progress >= 1) events.OnLoadOpComplete.Invoke(loader.scenePayload);
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        public void ForceLoad()
        {
            SceneManager.LoadScene(loader.scenePayload.buildIndex);
        }

        void Init()
        {
            loader.scenePayload = JDH_ApplicationManager.NextScenePayload;
            events.OnLoadOpStart.Invoke();
            StartCoroutine(LoadAsyncOperation());
        }
    }
}

