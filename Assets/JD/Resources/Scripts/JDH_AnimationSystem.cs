/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Animation
{
    using UnityEngine;
    using UnityEngine.Events;

    using Sherbert.GameplayStatics;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// A simple tool that uses events to drive an animation component. Extendable.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class JDH_AnimationSystem : MonoBehaviour
    {
        [System.Serializable]
        public class AnimationSettings
        {
            public const string DEFAULTPARAMNAME = "ParameterName";
            public const int DEFAULTLAYER = 0;
            public enum Type
            {
                Delegated, Updated
            }
            public Type type = new Type();

            public string currentAnimationParameter = AnimationSettings.DEFAULTPARAMNAME;
            
            [HideInInspector] 
            public string cacheParameter = AnimationSettings.DEFAULTPARAMNAME; 
            
            public int currentAnimationLayer = AnimationSettings.DEFAULTLAYER;
            
            public bool useRootMotion = false;
        }
        public AnimationSettings anim = new AnimationSettings();

        [System.Serializable]
        public class Components
        {
            public Animator animator;
        }
        public Components component = new Components();

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnAnimatorCallback;
            public UnityEvent<int> OnAnimationLayerChanged;
            public UnityEvent<string> OnAnimationParamChanged;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // MonoBehaviour Methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            Init();
        }

        void Update()
        {
            if (anim.type == AnimationSettings.Type.Updated) AnimationUpdate();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        void Init()
        {
            if (GetComponent<Animator>()) component.animator = GetComponent<Animator>();
            if (component.animator) anim.useRootMotion = component.animator.applyRootMotion;
        }

        public virtual void AnimationUpdate() {}

        public virtual void TransformWithWorld()
        {
            if (JDH_World.GetWorldIsEvil()) SetAnimatorLayer(1);
            else SetAnimatorLayer();
        }

        public void ForcePlayAnimation(AnimationClip Animation)
        {
            ForcePlayAnimation(Animation.name);
        }
        public void ForcePlayAnimation(string AnimationByName = AnimationSettings.DEFAULTPARAMNAME)
        {
            component.animator.StopPlayback();
            component.animator.Play(AnimationByName, anim.currentAnimationLayer);
            component.animator.StartPlayback();
            events.OnAnimatorCallback.Invoke();
        }
        public void ForceAnimatorTrigger(string TriggerName = AnimationSettings.DEFAULTPARAMNAME)
        {
            component.animator.ResetTrigger(anim.currentAnimationParameter);
            component.animator.SetTrigger(TriggerName);
            events.OnAnimatorCallback.Invoke();
        }

        //> Set First
        public void SetAnimatorLayer(int NewLayer = AnimationSettings.DEFAULTLAYER)
        {
            anim.currentAnimationLayer = NewLayer;
            events.OnAnimationLayerChanged.Invoke(NewLayer);
        }
        public void SetAnimatorParameterName(string NewParameter = AnimationSettings.DEFAULTPARAMNAME)
        {
            anim.cacheParameter = anim.currentAnimationParameter;
            anim.currentAnimationParameter = NewParameter;
            events.OnAnimationParamChanged.Invoke(NewParameter);
        }

        //! --- SHOULD BE USED IN CONJUNCTION WITH SetAnimatorParameterName(); IN A SEPERATE CALL FIRST! --- !//
        public virtual void ActivateAnimatorTrigger()
        {
            try {component.animator.ResetTrigger(anim.cacheParameter);}
            catch {}
            component.animator.SetTrigger(anim.currentAnimationParameter);
            events.OnAnimatorCallback.Invoke();
        }
        public virtual void ActivateAnimatorTrigger(string Trigger)
        {
            try {component.animator.ResetTrigger(Trigger);}
            catch {}
            component.animator.SetTrigger(Trigger);
            events.OnAnimatorCallback.Invoke();
        }
        public virtual void ActivateAnimatorBool(bool NewBool)
        {
            component.animator.SetBool(anim.currentAnimationParameter, NewBool);
            events.OnAnimatorCallback.Invoke();
        }
        public virtual void ActivateAnimatorInt(int NewInt)
        {
            component.animator.SetInteger(anim.currentAnimationParameter, NewInt);
            events.OnAnimatorCallback.Invoke();
        }
        public virtual void ActivateAnimatorFloat(int NewFloat)
        {
            component.animator.SetFloat(anim.currentAnimationParameter, NewFloat);
            events.OnAnimatorCallback.Invoke();
        }

        public void ChangeLayerWeight(float Weight = 0)
        {
            component.animator.SetLayerWeight(anim.currentAnimationLayer, Weight);
        }
        public void ChangeLayerWeight(float Weight, int Layer = 0)
        {
            component.animator.SetLayerWeight(Layer, Weight);
        }
    }
}
