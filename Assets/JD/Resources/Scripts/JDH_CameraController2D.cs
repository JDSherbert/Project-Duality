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
    /// Camera controller that follows a target.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_CameraController2D : MonoBehaviour
    {
        [System.Serializable]
        public class CameraControllerSettings
        {
            public Transform focus;
            public float smoothTime = 2;

            public Vector3 offset;
        }

        public CameraControllerSettings camerasetting = new CameraControllerSettings();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________
        
        void Awake()
        {
            Init();
        }

        void Update()
        {
            SmoothPan();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        void SmoothPan()
        {
            if(camerasetting.focus) transform.position = 
                Vector3.Lerp(transform.position, camerasetting.focus.position - camerasetting.offset, Time.deltaTime * camerasetting.smoothTime);
        }

        void Init()
        {
            if(!camerasetting.focus && GameObject.FindWithTag("Player")) camerasetting.focus = GameObject.FindWithTag("Player").transform;
            else if(!camerasetting.focus && !GameObject.FindWithTag("Player")) Debug.LogWarning("No focus target for camera.");

            if(camerasetting.focus) camerasetting.offset = camerasetting.focus.position - transform.position;

            if(transform.parent) this.transform.parent = null;
        }
    }
}
