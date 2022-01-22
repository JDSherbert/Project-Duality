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

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________
    /// Preserves an object and maintains a single instance. Inherit from this to make other objects singletons.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_Singleton : MonoBehaviour
    {
        private static JDH_Singleton _instance;
        public static JDH_Singleton Instance { get { return _instance; } }

        void Awake()
        {
            Singleton();
        }

        void Singleton()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
