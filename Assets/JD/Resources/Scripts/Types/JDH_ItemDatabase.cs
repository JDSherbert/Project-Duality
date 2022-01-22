/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Inventory
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Database structure for holding items.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Sherbert Tools/Item Database")]
    public class JDH_ItemDatabase : ScriptableObject
    {
        public List<JDH_Item> itemDatabase;
    }
}