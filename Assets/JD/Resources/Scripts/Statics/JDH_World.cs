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
    using UnityEngine.Events;

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

        public static class Events
        {
            public delegate JDH_World.WorldState OnWorldStateChangeDelegate(JDH_World.WorldState NewWorldState);
            public static OnWorldStateChangeDelegate OnWorldStateChangeEvent;
            public delegate float OnWorldSpeedChangeDelegate(float NewSpeed);
            public static OnWorldSpeedChangeDelegate OnWorldSpeedChangeEvent;
        }

        public static void SetWorld(WorldState NewState)
        {
            world = NewState;
            Debug.Log("World Set to new state" + NewState.ToString());
            if(Events.OnWorldStateChangeEvent != null) Events.OnWorldStateChangeEvent.Invoke(world);
        }

        public static WorldState GetWorld()
        {
            return world;
        }
        public static bool GetWorldIsEvil()
        {
            return world == WorldState.Evil;
        }
        public static bool GetWorldIsCute()
        {
            return world == WorldState.Cute;
        }

        public static void SetWorldSpeed(float Speed)
        {
            Time.timeScale = Mathf.Clamp(Speed, 0.0f, MAXWORLDSPEED);
            Events.OnWorldSpeedChangeEvent.Invoke(Time.timeScale);
        }
        public static void ResetWorldSpeed()
        {
            Time.timeScale = DEFAULTWORLDSPEED;
        }
    }
}
