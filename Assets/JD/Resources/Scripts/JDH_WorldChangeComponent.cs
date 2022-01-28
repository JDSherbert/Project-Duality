/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Systems
{
    using UnityEngine;
    using UnityEngine.Events;

    using Sherbert.GameplayStatics;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// A physical component allowing the changing of the world state and reaction to world state changes.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_WorldChangeComponent : MonoBehaviour
    {
        [System.Serializable]
        public class Events
        {
            public UnityEvent OnWorldBecameCute;
            public UnityEvent OnWorldBecameEvil;
            public UnityEvent<JDH_World.WorldState> OnWorldChanged;
            public JDH_World.Events.OnWorldStateChangeDelegate OnWorldStateChangeDelegateObject;
        }
        public Events events = new Events();

        [HideInInspector] public JDH_World.WorldState last;

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Awake() 
        {
             events.OnWorldStateChangeDelegateObject += JDH_World.Events.OnWorldStateChangeEvent; //Subscribe to event
        }

        void LateUpdate()
        {
            if(last != JDH_World.world)
            {
                last = JDH_World.world;
                WorldStateChanged();
            }
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        void WorldStateChanged()
        {
            if(JDH_World.GetWorldIsCute()) events.OnWorldBecameCute.Invoke();
            else if(JDH_World.GetWorldIsEvil()) events.OnWorldBecameEvil.Invoke();
        }
        public void SwitchWorldState()
        {
            if(JDH_World.GetWorldIsCute()) ChangeWorldStateToEvil();
            else ChangeWorldStateToCute();
        }
        void ChangeWorldStateToEvil()
        {
            JDH_World.SetWorld(JDH_World.WorldState.Evil);
        }
        void ChangeWorldStateToCute()
        {
            JDH_World.SetWorld(JDH_World.WorldState.Cute);
        }
    }
}


