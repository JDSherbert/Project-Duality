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

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Database structure for holding Runes.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    [CreateAssetMenu(fileName = "New Rune Lexicon", menuName = "Sherbert Tools/Lexicon")]
    public class JDH_RuneLexicon : ScriptableObject
    {
        public List<JDH_Rune> runeLexicon;
    }
}
