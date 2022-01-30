/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.GameplayStatics
{
    using System.Collections.Generic;
    using UnityEngine;

    using Sherbert.Lexicon;
    using Sherbert.Inventory;

    public static class JDH_GameplayStatics
    {
        public const string PLAYERTAG = "Player";
        
        public static GameObject GetPlayer()
        {
            return GameObject.FindGameObjectWithTag(PLAYERTAG);
        }
        public static GameObject GetPlayer(string PlayerTag)
        {
            return GameObject.FindGameObjectWithTag(PlayerTag);
        }

        public static void KillPlayer()
        {
            
        }

        public static GameObject GetObject(string Name)
        {
            return GameObject.Find(Name);
        }

        public static List<JDH_Item> GetAllItems()
        {
            return GetPlayer().GetComponentInChildren<JDH_InventorySystem>().GetAllItems();
        }

        public static List<JDH_Rune> GetAllRunes()
        {
            return GetPlayer().GetComponentInChildren<JDH_RuneSystem>().GetAllRunes();
        }

        public static bool IsTrueNull(this UnityEngine.Object obj)
        {
            return (object)obj == null;
        }
    }

}
