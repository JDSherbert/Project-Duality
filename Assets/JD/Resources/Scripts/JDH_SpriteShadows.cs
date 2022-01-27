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
    using UnityEngine.Rendering;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Allows drawing of shadows from a spriterenderer component. Will require a shader for the sprite materials.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class JDH_SpriteShadows : MonoBehaviour
    {
        [System.Serializable]
        public class ShadowSettings
        {
            public bool setOnStart = true;
            public bool recieveShadows = false;
            public ShadowCastingMode shadowMode = new ShadowCastingMode();
            //! [Off = 0] No shadows are cast from this object.
            //! [On = 1] Shadows are cast from this object.
            //! [TwoSided = 2] Shadows are cast from this object, treating it as two-sided.
            //! [ShadowsOnly = 3] Object casts shadows, but is otherwise invisible in the Scene.
        }
        public ShadowSettings shadow = new ShadowSettings();

        void Start()
        {
            if(shadow.setOnStart) SetShadows();
        }

        public void SetShadows()
        {
            GetComponent<SpriteRenderer>().receiveShadows = shadow.recieveShadows;
            GetComponent<SpriteRenderer>().shadowCastingMode = shadow.shadowMode;
        }
    }
}


