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

    using Sherbert.Lexicon;
    using Sherbert.Inventory;
    
    public static class JDH_PreserveCollected
    {
        public static bool HasHumphrey = false;
        public static JDH_Item Humphrey;
        public static List<JDH_Rune> DiscoveredRunes = new List<JDH_Rune>();

        public static void PlayerHasHumphrey()
        {
            HasHumphrey = JDH_GameplayStatics.GetHasHumphrey();
            Humphrey = JDH_GameplayStatics.GetItemReference("ITEM0000");
        }

        public static void StoreRunes()
        {
            DiscoveredRunes = JDH_GameplayStatics.GetAllRunes();
        }
    }
}



