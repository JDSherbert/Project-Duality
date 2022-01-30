/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Framework
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using Sherbert.Application;

    public class JDH_LevelChangeComponent : MonoBehaviour
    {
        public void DelegateLevel(int BuildIndex)
        {
            JDH_ApplicationManager.LoadSceneAsync(BuildIndex);
        }
    }
}
