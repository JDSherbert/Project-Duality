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
    /// Footstep Engine. Used to generate events after set times which can be used to callback audio or VFX.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_FootstepEngine : MonoBehaviour
    {
        [System.Serializable]
        public class FootstepSettings
        {
            public enum Type
            {
                Delegate, Animated, Auto
            }
            [Tooltip("The way this component will function: Delegate = Callback, Animated = Use an Animator State Info, Auto = Update()")]
            public Type type = new Type();

            [Tooltip("The spacing between footsteps.")]
            [Range(0, 1)] public float leftFootInterval, rightFootInterval;
            [HideInInspector] public float currentTime;

            public enum Footing
            {
                Left, Right
            }
            [Tooltip("Which foot will generate the event.")]
            public Footing footing = new Footing();
        }
        public FootstepSettings footstep = new FootstepSettings();

        [System.Serializable]
        public class Components
        {
            public Animator animator;
        }
        public Components component = new Components();

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnLeftFoot;
            public UnityEvent OnRightFoot;
            public UnityEvent OnAnyFoot;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // MonoBehaviour Methods
        //____________________________________________________________________________________________________________________________________________

        void Update()
        {
            if (footstep.type == FootstepSettings.Type.Auto) GenerateSteps();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public void GenerateSteps() //? Generates a footstep event based on intervals
        {
            switch (footstep.footing)
            {
                case (FootstepSettings.Footing.Left):
                    if (footstep.currentTime < footstep.leftFootInterval) footstep.currentTime += Time.deltaTime;
                    else
                    {
                        events.OnLeftFoot.Invoke();
                        events.OnAnyFoot.Invoke();
                        footstep.currentTime = 0;
                        footstep.footing = FootstepSettings.Footing.Right;
                    }
                    break;

                case (FootstepSettings.Footing.Right):
                    if (footstep.currentTime < footstep.rightFootInterval) footstep.currentTime += Time.deltaTime;
                    else
                    {
                        events.OnRightFoot.Invoke();
                        events.OnAnyFoot.Invoke();
                        footstep.currentTime = 0;
                        footstep.footing = FootstepSettings.Footing.Left;
                    }
                    break;
            }
        }

        public void ResetStep(int Foot = 0)
        {
            Mathf.Clamp01(Foot);
            if(Foot == 0) footstep.footing = FootstepSettings.Footing.Left;
            if(Foot == 1) footstep.footing = FootstepSettings.Footing.Right;
            footstep.currentTime = 0;
        }
    }
}
