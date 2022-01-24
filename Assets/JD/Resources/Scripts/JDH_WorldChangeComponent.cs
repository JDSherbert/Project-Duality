/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Systems
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using Sherbert.GameplayStatics;

    /// <summary>
    ///________________________________________________________________________________________________________________________________________________________
    /// A physical component allowing the changing of the world state for buttons
    ///________________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_WorldChangeComponent : MonoBehaviour
    {
        public void SwitchWorldState()
        {
            if(JDH_World.GetWorldIsCute()) ChangeWorldStateToEvil(); 
            //else if(JDH_World.GetWorldIsEvil()) ChangeWorldStateToCute(); 
        }
        void ChangeWorldStateToEvil()
        {
            JDH_World.SetWorld(JDH_World.WorldState.Evil);
        }
        void ChangeWorldStateToCute()
        {
            JDH_World.SetWorld(JDH_World.WorldState.Cute);
        }
    }
}


