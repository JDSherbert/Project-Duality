/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.AI
{
    using UnityEngine;
    using UnityEngine.Events;

    using Sherbert.GameplayStatics;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// An extendable AI basic framework class containing base functions and events, and virtual methods.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_AIBaseFramework : MonoBehaviour
    {
        public class EntityTypes //? Tags
        {
            public const string PLAYER = "Player";
            public const string BUNNY = "Bunny";
            public const string PUPPY = "Puppy";
            public const string BIRD = "Bird";
            public const string DISTRACTION = "Distraction";
            public const string WALL = "Wall";
        }

        [System.Serializable]
        public class BaseProperties
        {
            public GameObject self;
            public Transform target; 
            public float patrolWaitTime;
            public float patrolSpeed;
            public float chaseSpeed;
            public bool chasing;
        }
        public BaseProperties baseProperties = new BaseProperties();

        [System.Serializable]
        public class Events 
        {
            public JDH_World.Events.OnWorldStateChangeDelegate OnWorldStateChangeDelegateAI;
            public UnityEvent OnSpawn;
            public UnityEvent<JDH_World.WorldState> OnTransformed;
            public UnityEvent<Transform> OnDetectPlayer;
            public UnityEvent<GameObject> OnInteraction;
            public UnityEvent OnDestroyed;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // MonoBehaviour Methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            events.OnWorldStateChangeDelegateAI += JDH_World.Events.OnWorldStateChangeEvent; //Subscribe to event
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public virtual void InitializeAI()
        {
            baseProperties.self = this.gameObject;
            events.OnSpawn.Invoke();
        }

        public virtual void BehaviourHandler()
        {

        }

        public virtual void Transformation()
        {
            events.OnTransformed.Invoke(JDH_World.GetWorld());
        }

        public virtual Transform AcquireTarget()
        {
            return JDH_GameplayStatics.GetPlayer().transform;
        }
    }
}


