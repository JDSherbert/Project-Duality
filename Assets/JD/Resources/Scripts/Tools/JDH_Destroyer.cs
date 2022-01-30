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
    /// handy tool for destroying objects
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>

    public class JDH_Destroyer : MonoBehaviour
    {
        [System.Serializable]
        public class DestroyerSettings
        {
            public enum Type
            {
                Delegated, Auto
            }
            public Type type = new Type();
            public float timer;
        }
        public DestroyerSettings destroyer = new DestroyerSettings();

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnActivated;
            public UnityEvent<GameObject> OnDestroyObject;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            if(destroyer.type == DestroyerSettings.Type.Auto) ActivateDestroyer();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public void ActivateDestroyer()
        {
            ActivateDestroyer(this.gameObject);
        }
        public void ActivateDestroyer(GameObject Obj)
        {
            events.OnActivated.Invoke();
            Destroy(Obj, destroyer.timer);
        }

        void OnDestroy() 
        {
            events.OnDestroyObject.Invoke(this.gameObject);
        }
    }
}
