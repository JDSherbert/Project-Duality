/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.UI
{
    using System.Collections.Generic;
    using UnityEngine;


    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// Container for UI Elements.
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_UIContainer : MonoBehaviour
    {
        public List<GameObject> UIElements = new List<GameObject>();

        public void DisableUI()
        {
            foreach(GameObject UIElement in UIElements) UIElement.SetActive(false);
        }
        public void EnableUI()
        {
            foreach(GameObject UIElement in UIElements) UIElement.SetActive(true);
        }
    }
}
