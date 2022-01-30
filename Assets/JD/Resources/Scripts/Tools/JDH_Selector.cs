/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2021
/// Fractal Gear and all associated works are owned by and copyrighted to JDSherbert.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Generic
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________
    /// Iterates through a list of <Type>, turning them on or off based on selection.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_Selector : MonoBehaviour
    {
        [Tooltip("Stuff to choose from.")]
        public List<GameObject> Selections = new List<GameObject>();

        [System.Serializable]
        public class SelectorSettings
        {
            public enum SelectorType
            {
                Default, Iterated, PingPong, Random
            }
            public SelectorType type = new SelectorType();

            [Tooltip("Current selection.")]
            public int current = 0;
            [Tooltip("Do this automatically during update?")]
            public bool autoUpdate = false;
            [Tooltip("Direction to move in when cycling through selector.")]
            public bool ascending = true;
            [Tooltip("Timer setting for auto mode. Will cycle after this duration.")]
            public float timer = 1.0f;
            [Tooltip("Timer setting for auto mode.")]
            public float currentTimer = 0.0f;

        }

        public SelectorSettings selector = new SelectorSettings();

        [System.Serializable]
        public class Events
        {
            public UnityEvent OnRecycle;
            public UnityEvent<int> OnSelect;
        }

        public Events events = new Events();

        void Update()
        {
            if (selector.autoUpdate) HandleAutoSelect();
        }

        void HandleAutoSelect()
        {
            if (selector.currentTimer >= selector.timer)
            {
                selector.currentTimer = 0.0f;
                if (Selections.Count > 0 && Selections != null)
                {
                    switch (selector.type)
                    {
                        case (SelectorSettings.SelectorType.Iterated):
                            Iterate(false);
                            Select(selector.current);
                            break;

                        case (SelectorSettings.SelectorType.PingPong):
                            Iterate(true);
                            Select(selector.current);
                            break;

                        case (SelectorSettings.SelectorType.Random):
                            SelectRandom();
                            break;

                        default:
                            break;
                    }
                }
                else Debug.Log("No targets in list.");
            }
            else selector.currentTimer += Time.deltaTime;
        }

        public void SelectRandom()
        {
            int selection = Random.Range(0, Selections.Count);
            Select(selection);
        }
        public void Select(int Selection)
        {
            Selection = Mathf.Clamp(Selection, 0, Selections.Count);

            events.OnSelect.Invoke(Selection);
            selector.current = Selection;

            foreach (GameObject obj in Selections)
            {
                if (obj == Selections[Selection]) Selections[Selection].gameObject.SetActive(true);
                else obj.gameObject.SetActive(false);
            }
        }

        void Iterate(bool PingPong)
        {
            if (selector.ascending) selector.current++;
            else selector.current--;

            if (selector.current >= Selections.Count && !PingPong && selector.ascending)
            {
                selector.current = 0;
            }
            else if (selector.current <= 0 && !PingPong && !selector.ascending)
            {
                selector.current = Selections.Count;
            }

            if (selector.current >= Selections.Count && PingPong && selector.ascending)
            {
                selector.current = Selections.Count - 1;
                selector.ascending = false;
            }
            else if (selector.current <= 0 && PingPong && !selector.ascending)
            {
                selector.current = 0;
                selector.ascending = true;
            }
        }
    }
}

