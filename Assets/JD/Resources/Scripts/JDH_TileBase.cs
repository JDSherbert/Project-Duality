/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Map.Tile
{
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// An extendable tile basic framework class containing base functions and events, and virtual methods.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_TileBase : MonoBehaviour
    {

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnTileChange;
            public UnityEvent<GameObject> OnTileSteppedOn;
            public UnityEvent<GameObject> OnTrapActivated;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public virtual void OnSteppedOn(GameObject Instigator)
        {
            events.OnTileSteppedOn.Invoke(Instigator);
        }

        public virtual void TrapActivation(GameObject Instigator)
        {
            events.OnTrapActivated.Invoke(Instigator);
        }

        public virtual void OnTileChanged()
        {
            events.OnTileChange.Invoke();
        }
    }
}
