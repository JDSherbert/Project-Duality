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
            public int sceneBuildIndex = -1;
        }
        public LoadSettings loader = new LoadSettings();

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnLoadOpStart;
            public UnityEvent<float> OnLoadOpUpdate;
            public UnityEvent<int> OnLoadOpComplete; //Buildindex
        }
        public Events events = new Events();

        void Start()
        {
            Init();
        }


        public IEnumerator LoadAsyncOperation()
        {
            loader.sceneBuildIndex = JDH_ApplicationManager.NextSceneBuildIndex;
            yield return new WaitForSeconds(1);

            if (loader.sceneBuildIndex > -1)
            {                
                yield return new WaitForSeconds(LoadSettings.ASYNCDELAY);
                AsyncOperation gamelevel = SceneManager.LoadSceneAsync(loader.sceneBuildIndex);
                loader.progress = gamelevel.progress;

                while (loader.progress < 1)
                {
                    if (loader.progress != gamelevel.progress) events.OnLoadOpUpdate.Invoke(gamelevel.progress);
                    if (gamelevel.progress >= 1) events.OnLoadOpComplete.Invoke(loader.sceneBuildIndex);
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        public IEnumerator FakeLoadOperation() //! Use this for small games that are loading too quickly!
        {
            loader.sceneBuildIndex = JDH_ApplicationManager.NextSceneBuildIndex;
            yield return new WaitForSeconds(1);

            if (loader.sceneBuildIndex > -1)
            {              
                float loadbar = 0.0f;
                float loadbarlast = 0.0f;

                while (true)
                {
                    if (loadbar != loadbarlast) 
                    {
                        events.OnLoadOpUpdate.Invoke(loadbar);
                        loadbarlast = loadbar;
                    }
                    if (loadbar >= 1.0f) 
                    {
                        events.OnLoadOpComplete.Invoke(loader.sceneBuildIndex);
                        yield return new WaitForSeconds(LoadSettings.ASYNCDELAY);
                        AsyncOperation gamelevel = SceneManager.LoadSceneAsync(loader.sceneBuildIndex);
                    }
                    
                    //? Random discload style stops
                    if(Random.Range(0, 2000) >= 1980) yield return new WaitForSeconds(Random.Range(0, 3.0f)); 
                    
                    loadbar += Random.Range(0, 0.0025f);
                    yield return new WaitForEndOfFrame();
                }
            }
        }


        public void ForceLoad()
        {
            SceneManager.LoadScene(loader.sceneBuildIndex);
        }

        void Init()
        {
            loader.sceneBuildIndex = JDH_ApplicationManager.NextSceneBuildIndex;
            events.OnLoadOpStart.Invoke();
            //StartCoroutine(LoadAsyncOperation());
            StartCoroutine(FakeLoadOperation());
        }
    }
}

