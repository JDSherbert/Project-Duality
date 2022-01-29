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
    /// Cheap class component, great for a delegate. Deals damage to a health system component
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_Damage : MonoBehaviour
    {

        [System.Serializable]
        public class DamageSettings
        {

        }


        public void DealDamageToTarget(GameObject Object)
        {
            if(Object.GetComponent<JDH_HealthSystem>()) Object.GetComponent<JDH_HealthSystem>().DealDamage();
        }
    }
}
