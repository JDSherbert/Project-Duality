/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Framework
{
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Health system component for handling character death and states of "alive"ness.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_HealthSystem : MonoBehaviour
    {
        [System.Serializable]
        public class HealthSettings
        {
            public const int MAXHEALTH = 1;
            public int current = 1;

            public enum State
            {
                Alive, Dead
            }
            public State state = new State();
        }
        public HealthSettings health = new HealthSettings();

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnHeal;
            public UnityEvent<string> OnDamagedBy;
            public UnityEvent OnDeath;
            public UnityEvent OnRespawn;
        }
        public Events events = new Events();

        public virtual void DealDamage(int Damage = 1)
        {
            if(health.current > 0) health.current -= Damage;
            CheckHealth();
        }
        public virtual void DealDamage(GameObject Instigator, int Damage = 1)
        {
            Debug.Log(Instigator.name + " inflicted damage to " + this.gameObject.name);
            events.OnDamagedBy.Invoke(Instigator.name);
            DealDamage(Damage);
        }

        public void CheckHealth()
        {
            Mathf.Clamp(health.current, 0, HealthSettings.MAXHEALTH);
            
            if(health.current > 0)
            {
                health.state = HealthSettings.State.Alive;
            } 
            if(health.current <= 0)
            {
                health.state = HealthSettings.State.Dead;
                events.OnDeath.Invoke();
            } 
        }
    }
}
