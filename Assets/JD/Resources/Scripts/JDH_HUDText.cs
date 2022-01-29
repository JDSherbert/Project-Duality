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
    using UnityEngine.Events;

    using TMPro;
    
    /// <summary>
    ///____________________________________________________________________________________________________________________________________________
    /// An ultra simple text system. Uses the Event System to recieve and print a payload to a Textbox of choice.
    /// The text will be cleared after a short delay.
    ///
    /// Note: Supports both TMPro and Legacy Text.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_HUDText : JDH_TextSystemBase
    {
        [System.Serializable]
        public class TextSettings
        {
            [Tooltip("Text duration to remain on screen.")]
            public float textDuration = 2.0f;
            [Tooltip("Time taken to remove text from screen.")]
            public float fadeDuration = 0.5f;
            [Tooltip("Dynamic timer to track text duration.")]
            public float durationTimer;
            [Tooltip("Dynamic timer.")]
            public float timer;

            [Tooltip("Text colour to start.")]
            public Color startColour;
            [Tooltip("Text colour to end. Dynamically adjusted to 0 Alpha.")]
            public Color endColour;

            [Tooltip("Used for tracking the current state of the fade.")]
            [HideInInspector] public bool bUseFade;
            [Tooltip("Stack multiple texts on top of one another")]
            public bool bStack = false;
        }
        public TextSettings txt = new TextSettings();

        [System.Serializable]
        public class Events
        {
            public UnityEvent<string> OnWriteText;
        }

        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            Init();
        }

        void Update()
        {
            FadeEffectHandler();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public void PrintText(string Message)
        {
            if (component.tmp_TextBox) TMPro_Print(Message);
            else if (component.txt_TextBox) Txt_Print(Message);
            else Debug.Log("No suitable box to print to: " + Message);

            events.OnWriteText.Invoke(Message);
        }

        void TMPro_Print(string Message, float Duration = 2f, float FadeTransition = 0.5f)
        {
            if (!txt.bUseFade)
            {
                component.tmp_TextBox.text = Message;
                txt.textDuration = Duration;
                txt.fadeDuration = FadeTransition;
                txt.durationTimer = 0f;
                txt.timer = 0f;
                txt.bUseFade = true;
            }
            else
            {
                if(txt.bStack) component.tmp_TextBox.text += "\n" + Message;
                else component.tmp_TextBox.text = Message;

                txt.textDuration = Duration;
                txt.fadeDuration = FadeTransition;
                txt.durationTimer = 0f;
                txt.timer = 0f;
            }
        }

        void Txt_Print(string Message, float Duration = 2f, float FadeTransition = 0.5f)
        {
            if (!txt.bUseFade)
            {
                component.txt_TextBox.text = Message;
                txt.textDuration = Duration;
                txt.fadeDuration = FadeTransition;
                txt.durationTimer = 0f;
                txt.timer = 0f;
                txt.bUseFade = true;
            }
            else
            {
                if(txt.bStack) component.txt_TextBox.text += "\n" + Message;
                else component.txt_TextBox.text = Message;
                
                txt.textDuration = Duration;
                txt.fadeDuration = FadeTransition;
                txt.durationTimer = 0f;
                txt.timer = 0f;
            }
        }

        void FadeEffectHandler()
        {
            switch (txtype)
            {
                case (TextType.Legacy):
                    Txt_FadeEffect();
                    break;
                case (TextType.TMPro):
                    Tmp_FadeEffect();
                    break;
            }
        }

        void Txt_FadeEffect()
        {
            if (txt.bUseFade)
            {
                component.txt_TextBox.color = Color.Lerp(txt.endColour, txt.startColour, txt.timer);
                if (txt.timer < 1) txt.timer += Time.deltaTime / txt.fadeDuration;

                if (component.txt_TextBox.color.a >= 1)
                {
                    txt.bUseFade = false;
                    txt.timer = 0f;
                }
            }
            else
            {
                if (component.txt_TextBox.color.a >= 1) txt.durationTimer += Time.deltaTime;

                if (txt.durationTimer >= txt.textDuration)
                {
                    component.txt_TextBox.color = Color.Lerp(txt.startColour, txt.endColour, txt.timer);
                    if (txt.timer < 1)
                        txt.timer += Time.deltaTime / txt.fadeDuration;
                }
            }
        }

        void Tmp_FadeEffect()
        {
            if (txt.bUseFade)
            {
                component.tmp_TextBox.color = Color.Lerp(txt.endColour, txt.startColour, txt.timer);
                if (txt.timer < 1) txt.timer += Time.deltaTime / txt.fadeDuration;

                if (component.tmp_TextBox.color.a >= 1)
                {
                    txt.bUseFade = false;
                    txt.timer = 0f;
                }
            }
            else
            {
                if (component.tmp_TextBox.color.a >= 1) txt.durationTimer += Time.deltaTime;

                if (txt.durationTimer >= txt.textDuration)
                {
                    component.tmp_TextBox.color = Color.Lerp(txt.startColour, txt.endColour, txt.timer);
                    if (txt.timer < 1)
                        txt.timer += Time.deltaTime / txt.fadeDuration;
                }
            }
        }

        protected override void Init()
        {
            if (!component.txt_TextBox && !component.tmp_TextBox && GetComponent<Text>()) component.txt_TextBox = GetComponent<Text>();
            else if (!component.txt_TextBox && !component.tmp_TextBox && GetComponent<TextMeshProUGUI>()) component.tmp_TextBox = GetComponent<TextMeshProUGUI>();

            //? Set TextBox Type
            if (component.txt_TextBox != null) txtype = TextType.Legacy;
            if (component.tmp_TextBox != null) txtype = TextType.TMPro;

            if (txtype == TextType.Legacy)
            {
                component.txt_TextBox.verticalOverflow = VerticalWrapMode.Overflow;
                txt.startColour = component.txt_TextBox.color;
                txt.endColour.a = 0f;
                component.txt_TextBox.color = txt.endColour;
            }
            if (txtype == TextType.TMPro)
            {
                txt.startColour = component.tmp_TextBox.color;
                txt.endColour.a = 0f;
                component.tmp_TextBox.color = txt.endColour;
            }
        }
    }
}
