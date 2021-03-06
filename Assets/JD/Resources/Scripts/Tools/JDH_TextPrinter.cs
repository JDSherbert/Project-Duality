/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Text
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// An ultra simple text system. Will iteratively print a payload to a textbox, like a typewriter.
    ///
    /// Note: Supports both TMPro and Legacy Text.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_TextPrinter : JDH_TextSystemBase
    {
        [TextArea]
        [Tooltip("Text to print.")]
        public string payload;

        [System.Serializable]
        public class PrinterSettings
        {
            public const float PRINTMAXSPACING = 0.5f;
            [Range(0.0f, PRINTMAXSPACING)] public float printSpacing = 0.025f;

            public const float DELAYSPACE = 0.1f;
            public bool bPrintOnAwake = false;
        }
        public PrinterSettings printer = new PrinterSettings();

        [System.Serializable]
        public class TextCodes
        {
            public const char DELAY = '[';
        }
        public TextCodes chars = new TextCodes();

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnStartPrintPayload;
            public UnityEvent<char> OnPrintChar;
            public UnityEvent<string> OnFinishPrintPayload;
            public UnityEvent OnTextCleared;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            Init();
            if (printer.bPrintOnAwake) StartPrint();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        public void StartPrint()
        {
            StartCoroutine(PrintText(payload));
        }
        public void StartPrint(string CustomText)
        {
            StartCoroutine(PrintText(CustomText));
        }

        public IEnumerator PrintText(string Text)
        {
            events.OnStartPrintPayload.Invoke();
            foreach (char c in Text)
            {
                yield return new WaitForSeconds(printer.printSpacing);

                if (c == TextCodes.DELAY) yield return new WaitForSeconds(PrinterSettings.DELAYSPACE);
                else
                {
                    switch (txtype)
                    {
                        case (TextType.Legacy):
                            component.txt_TextBox.text += c;
                            break;
                        case (TextType.TMPro):
                            component.tmp_TextBox.text += c;
                            break;
                    }
                    events.OnPrintChar.Invoke(c);
                }
            }
            events.OnFinishPrintPayload.Invoke(Text);
        }

        public void ClearText()
        {
            switch (txtype)
            {
                case (TextType.Legacy):
                    component.txt_TextBox.text = "";
                    break;
                case (TextType.TMPro):
                    component.tmp_TextBox.text = "";
                    break;
            }
            events.OnTextCleared.Invoke();
        }

    }
}


