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

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// handy tool for creating delayed callbacks.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_Delayer : MonoBehaviour
    {
        [System.Serializable]
        public class DelaySettings
        {
            [Tooltip("Maximum delay.")]
            public float maxDelay = 0.0f;
            [Tooltip("Current delay time.")]
            public float currentDelayTime = 0.0f;
            [Tooltip("Is timer currently active?.")]
            public bool active = false;
        }

        [System.Serializable]
        public class Events
        {
            public UnityEvent<bool> OnDelayTimerStart;
            public UnityEvent<float> OnDelayTimerTick;
            public UnityEvent OnDelayTimerFinish;
        }

        public DelaySettings delayer = new DelaySettings();
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Update()
        {
            if (delayer.active) DelayerTick();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        /// <summary>
        /// Call to start the delay.
        /// 
        /// </summary>
        /// <returns> [void] </returns>
        public void DelayerActivate(bool Activate)
        {
            if(delayer.active == Activate) DelayPurge();
            delayer.active = Activate;
            events.OnDelayTimerStart.Invoke(Activate);
        }

        /// <summary>
        /// Called every frame.
        /// 
        /// </summary>
        /// <returns> [void] </returns>
        public void DelayerTick()
        {
            delayer.currentDelayTime += Time.deltaTime;
            events.OnDelayTimerTick.Invoke(delayer.currentDelayTime);

            if (delayer.currentDelayTime >= delayer.maxDelay) DelayerCompleted();
        }

        /// <summary>
        /// Called upon delay completion. Resets the script after completion.
        /// 
        /// </summary>
        /// <returns> [void] </returns>
        public void DelayerCompleted()
        {
            events.OnDelayTimerFinish.Invoke();
            DelayPurge();
        }

        /// <summary>
        /// Call to completely stop and reset the delay.
        /// 
        /// </summary>
        /// <returns> [void] </returns>
        public void DelayPurge()
        {
            delayer.active = false;
            delayer.currentDelayTime = 0.0f;
        }
    }
}


