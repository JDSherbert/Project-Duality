/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Created for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

public static class World
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
