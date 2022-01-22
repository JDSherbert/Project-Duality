/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Text
{
    using UnityEngine;
    using UnityEngine.UI;

    using TMPro;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Base Inherited class for other text systems. Allows distinction between Legacy and TMPro text.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_TextSystemBase : MonoBehaviour
    {
        [System.Serializable]
        public class Components
        {
            [Tooltip("Text box.")]
            public Text txt_TextBox;
            [Tooltip("TMPro text box.")]
            public TextMeshProUGUI tmp_TextBox;
        }

        public Components component = new Components();

        [System.Serializable]
        public enum TextType
        {
            Legacy, TMPro
        }

        public TextType txtype = new TextType();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            Init();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        protected virtual void Init()
        {
            if (!component.txt_TextBox && !component.tmp_TextBox && GetComponent<Text>()) component.txt_TextBox = GetComponent<Text>();
            else if (!component.txt_TextBox && !component.tmp_TextBox && GetComponent<TextMeshProUGUI>()) component.tmp_TextBox = GetComponent<TextMeshProUGUI>();

            //? Set TextBox Type
            if (component.txt_TextBox != null) txtype = TextType.Legacy;
            if (component.tmp_TextBox != null) txtype = TextType.TMPro;
        }
    }
}


