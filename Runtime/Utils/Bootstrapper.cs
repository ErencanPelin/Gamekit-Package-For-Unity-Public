using UnityEngine;

namespace Gamekit2D.Runtime.Utils
{
    /// <summary>
    /// Code within this class runs before the game itself is executed
    /// </summary>
    internal static class BootStrapper
    {
        /// <summary>
        /// Code in this function runs before the rest of the application - it runs automatically as soon as the application plays
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Awake()
            => Object.Instantiate(AssetSearch.GetAsset<GameObject>("Systems"));
    }
}