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

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________
    /// Fades in and out text based on params.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_TextFader : JDH_TextSystemBase
    {
        [System.Serializable]
        public class FaderSettings
        {
            public enum Fade
            {
                NONE, OUT, IN
            }
            public Fade fade = new Fade();
            public const float TIMERMAX = 1.0f;
            [Tooltip("Speed of fade.")]
            public float timerMultiplier = 2.0f;
            [Tooltip("Dynamic timer.")]
            public float timer;

            [Tooltip("Text colour to start.")]
            public Color startColour;
            [Tooltip("Text colour to end. Dynamically adjusted to 0 Alpha.")]
            public Color endColour;
        }
        public FaderSettings fader = new FaderSettings();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Update()
        {
            switch (fader.fade)
            {
                case (FaderSettings.Fade.IN):
                    FadeEffectHandler(true);
                    break;

                case (FaderSettings.Fade.OUT):
                    FadeEffectHandler(false);
                    break;
            }
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        void FadeEffectHandler(bool Fader)
        {
            if (Fader && fader.timer <= FaderSettings.TIMERMAX) fader.timer += Time.deltaTime * fader.timerMultiplier;
            if (!Fader && fader.timer >= 0) fader.timer -= Time.deltaTime * fader.timerMultiplier;

            fader.timer = Mathf.Clamp(fader.timer, 0, FaderSettings.TIMERMAX);
            switch (txtype)
            {
                case (TextType.Legacy):
                    component.txt_TextBox.color = Fade();
                    break;
                case (TextType.TMPro):
                    component.tmp_TextBox.color = Fade();
                    break;
            }
        }

        Color Fade()
        {
            return Color.Lerp(fader.endColour, fader.startColour, fader.timer);
        }

        public void FadeInMode()
        {
            fader.fade = FaderSettings.Fade.IN;
        }
        public void FadeInMode(string CustomPayload)
        {
            if(component.txt_TextBox) component.txt_TextBox.text = CustomPayload;
            if(component.tmp_TextBox) component.tmp_TextBox.text = CustomPayload;
            fader.fade = FaderSettings.Fade.IN;
        }
        public void FadeOutMode()
        {
            fader.fade = FaderSettings.Fade.OUT;
        }
        public void FadeOutMode(string CustomPayload)
        {
            if(component.txt_TextBox) component.txt_TextBox.text = CustomPayload;
            if(component.tmp_TextBox) component.tmp_TextBox.text = CustomPayload;
            fader.fade = FaderSettings.Fade.OUT;
        }
        public void FadeStop()
        {
            fader.fade = FaderSettings.Fade.NONE;
        }
    }
}


