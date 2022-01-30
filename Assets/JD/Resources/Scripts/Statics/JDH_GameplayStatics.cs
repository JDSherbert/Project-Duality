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
    using Sherbert.Framework;
    using Sherbert.Tools.UI;

    public static class JDH_GameplayStatics
    {
        public const string PLAYERTAG = "Player";
        public const string UITAG = "UI Element";
        public const string TRAPS = "Traps";
        
        public static GameObject GetPlayer()
        {
            return GameObject.FindGameObjectWithTag(PLAYERTAG);
        }
        public static GameObject GetPlayer(string PlayerTag)
        {
            return GameObject.FindGameObjectWithTag(PlayerTag);
        }
        public static Vector3 GetPlayerChaseSpeed()
        {
            return GetPlayer().GetComponent<JDH_PlayerChaseController>().chaser.forcedMovement;
        }

        public static void KillPlayer()
        {
            GetPlayer().GetComponent<JDH_HealthSystem>().DealDamage();
        }

        public static void ToggleUI(bool Active)
        {
            if(Active) GetPlayer().GetComponent<JDH_UIContainer>().EnableUI();
            else if(!Active) GetPlayer().GetComponent<JDH_UIContainer>().DisableUI();
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

        public static void UncoverAllTraps()
        {
            MJB_TrapTileBehaviour[] traps = GameObject.Find(TRAPS).GetComponentsInChildren<MJB_TrapTileBehaviour>();
            foreach(MJB_TrapTileBehaviour trap in traps)
            {
                trap.UncoverTrap();
            }
        }
    }

}
