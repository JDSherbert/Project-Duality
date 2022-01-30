/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools
{
    using UnityEngine;
    using UnityEngine.Events;

    public class JDH_MonoEvents : MonoBehaviour
    {
        [System.Serializable]
        public class Events
        {
            public UnityEvent OnAwake;
            public UnityEvent OnStart;
            public UnityEvent OnEnable;
            public UnityEvent OnDisable;
            public UnityEvent OnDestroy;

        }
        public Events events = new Events();

        void Awake()
        {
            events.OnAwake.Invoke();
        }
        void Start()
        {
            events.OnStart.Invoke();
        }
        void OnEnable()
        { 
            events.OnEnable.Invoke();
        }
        void OnDisable()
        {
            events.OnDisable.Invoke();
        }
        void OnDestroy()
        {
            events.OnDestroy.Invoke();
        }
    }
}


