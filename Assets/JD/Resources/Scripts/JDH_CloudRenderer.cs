/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Graphics
{
    using UnityEngine;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Performs batch translation of cloud transforms, resetting the transform position when
    /// the cloud transform position exceeds the resetRadius distance to the controller.
    /// Automatically collects and animates all it's child transforms.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_CloudRenderer : MonoBehaviour
    {
        [System.Serializable]
        public class CloudSettings
        {
            public Vector3 windDirection = Vector2.left;
            public float windSpeed = 1;
            public float minSpeed = 0.5f;
            public float resetRadius = 100;
        }
        public CloudSettings cloud = new CloudSettings();

        [System.Serializable]
        public class CachedData
        {
            public Transform[] cloudTransforms;
            public float[] cloudSpeeds;
        }
        public CachedData cache = new CachedData();

        //____________________________________________________________________________________________________________________________________________
        // MonoBehaviour Methods
        //____________________________________________________________________________________________________________________________________________

        void Start()
        {
            Init();
        }

        void Update()
        {
            DrawClouds();
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, cloud.resetRadius);
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public void Init()
        {
            cache.cloudTransforms = new Transform[transform.childCount];
            cache.cloudSpeeds = new float[transform.childCount];
            for (var i = 0; i < transform.childCount; i++)
            {
                cache.cloudTransforms[i] = transform.GetChild(i);
                cache.cloudSpeeds[i] = Random.value;
            }
        }

        public void DrawClouds()
        {
            float radiusSquared = cloud.resetRadius * cloud.resetRadius;
            for (var i = 0; i < cache.cloudSpeeds.Length; i++)
            {
                Transform cloudInstance = cache.cloudTransforms[i];
                float thisSpeed = Mathf.Lerp(cloud.minSpeed, cloud.windSpeed, cache.cloudSpeeds[i]);
                cloudInstance.position += cloud.windDirection * thisSpeed;

                if (cloudInstance.localPosition.sqrMagnitude > radiusSquared)
                {
                    cloudInstance.position = -cloudInstance.position;
                }
            }
        }
    }
}
