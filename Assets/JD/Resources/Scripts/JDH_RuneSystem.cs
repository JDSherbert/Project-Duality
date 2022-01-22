/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Lexicon
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    using Sherbert.GameplayStatics;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// System Component that manipulates how items are stored and used.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_RuneSystem : MonoBehaviour
    {
        public List<JDH_Rune> DiscoveredRunes = new List<JDH_Rune>();
        public int currentlySelectedIndex = 0;

        [System.Serializable]
        public class LexiconSettings
        {
            [System.Serializable]
            public enum State
            {
                Closed, Open
            }
            public State state = State.Closed;

            [Range(0.0f, 1.0f)] public float TimeScaleWhileOpen = 0.0f;

            [System.Serializable]
            public class InputSettings
            {
                public string OpenCloseAxis = "OpenLexicon";
                public float AXIS_OPENCLOSE;
            }
            public InputSettings input = new InputSettings();
        }
        public LexiconSettings lexicon = new LexiconSettings();

        [System.Serializable]
        public class Components
        {
            public GameObject LexiconMenuUI;
        }
        public Components component = new Components();

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnLexiconOpen;
            public UnityEvent OnSelectionChange;
            public UnityEvent OnLexiconClose;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Update()
        {
            InputHandler();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        void InputHandler()
        {
            lexicon.input.AXIS_OPENCLOSE = Input.GetAxisRaw(lexicon.input.OpenCloseAxis);

            if (lexicon.input.AXIS_OPENCLOSE > 0) //? Toggle State
            {
                if (lexicon.state == LexiconSettings.State.Closed) OpenLexiconMenu();
                if (lexicon.state == LexiconSettings.State.Open) CloseLexiconMenu();
            }
        }

        public void OpenLexiconMenu()
        {
            component.LexiconMenuUI.SetActive(true);
            events.OnLexiconOpen.Invoke();
            JDH_World.SetWorldSpeed(lexicon.TimeScaleWhileOpen);

        }
        public void CloseLexiconMenu()
        {
            component.LexiconMenuUI.SetActive(false);
            events.OnLexiconClose.Invoke();
            JDH_World.ResetWorldSpeed();
        }

        public void UnlockRune(JDH_Rune NewRune)
        {
            if(!DiscoveredRunes.Contains(NewRune)) DiscoveredRunes.Add(NewRune);
        }

        public List<JDH_Rune> GetAllRunes()
        {
            return DiscoveredRunes;
        }
    }
}
