/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Physics
{
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________
    /// Detects collision or trigger entry (on an attached collider/rigidbody). Reports an event with information about the object.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_CollisionSensor : MonoBehaviour
    {
        [System.Serializable]
        public class CollisionEvents
        {
            public UnityEvent<GameObject> OnCollisionEntered;
            public UnityEvent<GameObject> WhileColliding;
            public UnityEvent<GameObject> OnCollisionLeft;
        }

        [System.Serializable]
        public class TriggerEvents
        {
            public UnityEvent<GameObject> OnTriggerEntered;
            public UnityEvent<GameObject> WhileTriggering;
            public UnityEvent<GameObject> OnTriggerLeft;
        }

        public CollisionEvents CollidedEvents = new CollisionEvents();
        public TriggerEvents TriggeredEvents = new TriggerEvents();

        void Awake()
        {
            Init();
        }

        void Init()
        {
            if (CollidedEvents.OnCollisionEntered == null) CollidedEvents.OnCollisionEntered = new UnityEvent<GameObject>();
            if (CollidedEvents.WhileColliding == null) CollidedEvents.WhileColliding = new UnityEvent<GameObject>();
            if (CollidedEvents.OnCollisionLeft == null) CollidedEvents.OnCollisionLeft = new UnityEvent<GameObject>();
            if (TriggeredEvents.OnTriggerEntered == null) TriggeredEvents.OnTriggerEntered = new UnityEvent<GameObject>();
            if (TriggeredEvents.WhileTriggering == null) TriggeredEvents.WhileTriggering = new UnityEvent<GameObject>();
            if (TriggeredEvents.OnTriggerLeft == null) TriggeredEvents.OnTriggerLeft = new UnityEvent<GameObject>();
        }

        //---------------- COLLISIONS ------------------//
        #region Collisions
        void OnCollisionEnter(Collision other)
        {
            if (CollidedEvents.OnCollisionEntered != null) CollidedEvents.OnCollisionEntered.Invoke(other.gameObject);
        }

        void OnCollisionStay(Collision other)
        {
            if (CollidedEvents.WhileColliding != null) CollidedEvents.WhileColliding.Invoke(other.gameObject);
        }

        void OnCollisionExit(Collision other)
        {
            if (CollidedEvents.OnCollisionLeft != null) CollidedEvents.OnCollisionLeft.Invoke(other.gameObject);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (CollidedEvents.OnCollisionEntered != null) CollidedEvents.OnCollisionEntered.Invoke(other.gameObject);
        }

        void OnCollisionStay2D(Collision2D other)
        {
            if (CollidedEvents.WhileColliding != null) CollidedEvents.WhileColliding.Invoke(other.gameObject);
        }

        void OnCollisionExit2D(Collision2D other)
        {
            if (CollidedEvents.OnCollisionLeft != null) CollidedEvents.OnCollisionLeft.Invoke(other.gameObject);
        }
        #endregion Collisions

        //---------------- TRIGGERS ------------------//
        #region Triggers
        void OnTriggerEnter(Collider other)
        {
            if (TriggeredEvents.OnTriggerEntered != null) TriggeredEvents.OnTriggerEntered.Invoke(other.gameObject);
        }

        void OnTriggerStay(Collider other)
        {
            if (TriggeredEvents.WhileTriggering != null) TriggeredEvents.WhileTriggering.Invoke(other.gameObject);
        }

        void OnTriggerExit(Collider other)
        {
            if (TriggeredEvents.OnTriggerLeft != null) TriggeredEvents.OnTriggerLeft.Invoke(other.gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (TriggeredEvents.OnTriggerEntered != null) TriggeredEvents.OnTriggerEntered.Invoke(other.gameObject);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if (TriggeredEvents.WhileTriggering != null) TriggeredEvents.WhileTriggering.Invoke(other.gameObject);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (TriggeredEvents.OnTriggerLeft != null) TriggeredEvents.OnTriggerLeft.Invoke(other.gameObject);
        }
        #endregion Triggers
    }
}

