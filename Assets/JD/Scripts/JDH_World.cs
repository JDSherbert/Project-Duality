/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.GameplayStatics
{
    using UnityEngine;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Static class that controls the world state.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public static class JDH_World
    {
        public const float DEFAULTWORLDSPEED = 1.0f;
        public const float MAXWORLDSPEED = 10.0f;
        public enum WorldState
        {
            Cute, Evil
        }

        public static WorldState world = new WorldState();

        public static void SetWorld(WorldState NewState)
        {
            world = NewState;
        }

        public static WorldState GetWorld()
        {
            return world;
        }

        public static void SetWorldSpeed(float Speed)
        {
            Time.timeScale = Mathf.Clamp(Speed, 0.0f, MAXWORLDSPEED);
        }
        public static void ResetWorldSpeed()
        {
            Time.timeScale = DEFAULTWORLDSPEED;
        }
    }
}
