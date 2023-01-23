using UnityEngine;

namespace Gamekit2D.Runtime.Extensions
{
    /// <summary>
    /// Extension methods for Vector2 class
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Rounds and returns a vector
        /// </summary>
        /// <param name="x">this vector 2</param>
        /// <returns>This vector2 rounded</returns>
        public static Vector2 Round(this Vector2 x) => new(Mathf.Round(x.x), Mathf.RoundToInt(x.y));
    }
}
