using UnityEngine;

namespace Gamekit2D.Runtime.Actor
{
    [System.Serializable]
    public record RaySettings
    {
        public float rayLength = 1;
        public Vector2 rayStartOffset;
        public LayerMask rayLayer;
    }
}