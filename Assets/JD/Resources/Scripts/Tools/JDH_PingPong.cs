/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Generic
{
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________
    /// A simple pingpong component. Will lerp between two values based on mode of choice. Setup traits in PingPong Settings.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_PingPong : MonoBehaviour
    {
        [System.Serializable]
        public class InterpolationSettings
        {
            [Tooltip("Is running currently.")]
            public bool bActive = true;
            [Tooltip("Value to lerp up to, before starting to go back to min again.")]
            public float lerpMax = 1.0f;
            [Tooltip("Value to lerp down to, before starting to go back to max again.")]
            public float lerpMin = 0.0f;
            [Tooltip("How fast this should be. Between 0 to 1 is slower, 1 to X is faster.")]
            public float lerpSpeedMultiplier = 1.0f;
            [Tooltip("Current value, for debug purposes. Can be used to change the starting value.")]
            public float lerpCurrentValue = 0;

            [System.Serializable]
            public enum Type
            {
                Lerp, Toggle
            }

            public Type type = new Type();
        }

        public InterpolationSettings interpolation = new InterpolationSettings();

        [System.Serializable]
        public class Events
        {
            public UnityEvent<float> OnLerpValueChanged;
            public UnityEvent OnMinValue;
            public UnityEvent OnMaxValue;
        }

        public Events events = new Events();

        void Awake()
        {
            Mathf.Clamp(interpolation.lerpCurrentValue, interpolation.lerpMin, interpolation.lerpMax);
        }
        
        void Update()
        {
            if (interpolation.bActive) Interpolate();
        }

        public void Interpolate()
        {
            float Last = interpolation.lerpCurrentValue;

            //? The important calculation
            interpolation.lerpCurrentValue = Mathf.Lerp
                (interpolation.lerpMin, interpolation.lerpMax, 
                    Mathf.PingPong(Time.time * interpolation.lerpSpeedMultiplier, interpolation.lerpMax));

            switch (interpolation.type)
            {
                case (InterpolationSettings.Type.Lerp):
                    if (Last != interpolation.lerpCurrentValue) events.OnLerpValueChanged.Invoke(interpolation.lerpCurrentValue);
                    if (interpolation.lerpCurrentValue >= interpolation.lerpMax) events.OnMaxValue.Invoke();
                    if (interpolation.lerpCurrentValue <= interpolation.lerpMin) events.OnMinValue.Invoke();
                    break;

                case (InterpolationSettings.Type.Toggle):
                    if (interpolation.lerpCurrentValue > interpolation.lerpMax / 2)
                    {
                        interpolation.lerpCurrentValue = interpolation.lerpMax;
                        if (Last != interpolation.lerpCurrentValue)
                        {
                            events.OnLerpValueChanged.Invoke(Last);
                            events.OnMaxValue.Invoke();
                        }
                    }
                    if (interpolation.lerpCurrentValue <= interpolation.lerpMax / 2)
                    {
                        interpolation.lerpCurrentValue = interpolation.lerpMin;
                        if (Last != interpolation.lerpCurrentValue)
                        {
                            events.OnLerpValueChanged.Invoke(Last);
                            events.OnMinValue.Invoke();
                        }
                    }
                    break;
            }
        }

        public void ActiveInterpolation(bool NewState)
        {
            interpolation.bActive = NewState;
            if (NewState != true) SetLerpValue(interpolation.lerpMin);
        }

        public void SetLerpValue(float NewValue)
        {
            if (NewValue <= interpolation.lerpMax && NewValue > interpolation.lerpMin)
            {
                interpolation.lerpCurrentValue = NewValue;
                if (NewValue == interpolation.lerpMax) events.OnMaxValue.Invoke();
            }
            else
            {
                interpolation.lerpCurrentValue = interpolation.lerpMin;
                events.OnMinValue.Invoke();
            }

            events.OnLerpValueChanged.Invoke(interpolation.lerpCurrentValue);
        }
    }
}