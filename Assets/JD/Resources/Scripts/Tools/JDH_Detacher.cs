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
    /// handy tool for removing a parent.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_Detacher : MonoBehaviour
    {
        public enum Type
        {
            Delegated, Auto
        }
        public Type type = new Type();

        [System.Serializable]
        public class Events
        {
            public UnityEvent<Transform> OnDetach;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            if (type == Type.Auto) DetachParent();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public void DetachParent()
        {
            if (transform.parent)
            {
                this.transform.parent = null;
                events.OnDetach.Invoke(this.transform);
            }
        }

        public void DetachParent(Transform Other)
        {
            if (Other.transform.parent)
            {
                Other.transform.parent = null;
                events.OnDetach.Invoke(Other);
            }
        }
    }
}
