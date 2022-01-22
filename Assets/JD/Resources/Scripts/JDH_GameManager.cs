/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Tools.Systems
{
    using UnityEngine.SceneManagement;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________
    /// Tracks current gamestate.
    ///____________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_GameManager : JDH_Singleton
    {
        [System.Serializable]
        public class GameInstance
        {
            public Scene currentLevel;
            public int timesDead = 0;
        }
        public GameInstance game = new GameInstance();
    }
}
