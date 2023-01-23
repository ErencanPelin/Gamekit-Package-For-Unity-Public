using System;
using UnityEngine;

namespace Gamekit2D.Runtime.Utils
{
    public static class AssetSearch
    {
        internal static T GetAsset<T>(string name) where T : UnityEngine.Object
            => Resources.Load<T>(name) ?? throw new Exception($"{name} not found");
        
        /// <summary>
        /// Returns the Gamekit settings file inside /Assets/Settings - and creates the folder + asset if they don't exist
        /// </summary>
        /// <returns>GamekitSettings.asset file</returns>
        public static GamekitSettings FindGamekitSettings()
            => Resources.Load<GamekitSettings>("GamekitSettings")?? throw new Exception("\"GamekitSettings.asset\" file could not be located in the Assets/Resources Directory!");
    }
}
