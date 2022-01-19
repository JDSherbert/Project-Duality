/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.GameplayStatics
{

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Static class that controls the world state.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public static class JDH_World
    {
        public enum WorldState
        {
            Cute, Evil
        }

        public static WorldState world = new WorldState();

        public static void SetWorld(WorldState NewState)
        {
            world = NewState;
        }
    }
}
