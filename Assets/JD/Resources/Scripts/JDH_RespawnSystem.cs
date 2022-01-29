/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Framework
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Respawn system component for handling respawning of the character.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_RespawnSystem : MonoBehaviour
    {
        public List<Vector3> spawnPoints = new List<Vector3>();

        [System.Serializable]
        public class Events
        {
            public UnityEvent<Vector3> OnSpawnAdded;
            public UnityEvent<GameObject> OnRespawn;

        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // MonoBehaviour Methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            Init();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public void AddSpawnLocation(Transform NewLocation)
        {
            AddSpawnLocation(NewLocation.position);
        }
        public void AddSpawnLocation(Vector3 NewLocation)
        {
            if (NewLocation != null)
            {
                if (!spawnPoints.Contains(NewLocation))
                {
                    spawnPoints.Add(NewLocation);
                    events.OnSpawnAdded.Invoke(NewLocation);
                }
            }
        }

        public void Respawn()
        {
            Respawn(spawnPoints.Count);
        }
        public void Respawn(int Index)
        {
            Respawn(spawnPoints[Mathf.Clamp(Index, 0, spawnPoints.Count-1)]);
        }
        public void Respawn(Vector3 SpecificWorldPoint)
        {
            this.transform.root.position = SpecificWorldPoint;
            events.OnRespawn.Invoke(this.gameObject);
        }
        public void RespawnRandom()
        {
            Respawn(Random.Range(0, spawnPoints.Count));
        }

        void Init()
        {
            Vector3 initialPoint = new Vector3();
            initialPoint = this.transform.position;

            AddSpawnLocation(initialPoint);
        }
    }
}
