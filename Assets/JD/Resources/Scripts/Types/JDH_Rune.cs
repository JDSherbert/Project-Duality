/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Lexicon
{
    using UnityEngine;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Class template that defines a Rune.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    [CreateAssetMenu(fileName = "New Rune", menuName = "Sherbert Tools/Rune")]
    public class JDH_Rune : ScriptableObject
    {
        [Tooltip("The Rune's image to use.")]
        public Sprite sprite;
        [Tooltip("The Rune's alphabetical character.")]
        public char letter;
        [TextArea] [Tooltip("Short description for the Rune.")]
        public string description;

        [Tooltip("The item's physical object.")]
        public GameObject runeObject;
    }
}
