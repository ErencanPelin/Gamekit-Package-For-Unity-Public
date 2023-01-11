using Gamekit2D.Runtime.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gamekit2D.Runtime.Utils
{
    public class GamekitSettings : ScriptableObject
    {
        /// <summary>
        /// Scene Transition to use
        /// </summary>
        public SceneTransition sceneTransition;
        /// <summary>
        /// Scene transition color
        /// </summary>
        public Color transitionColor;
        /// <summary>
        /// Input action bindings for which all controls within the game kit rely on
        /// </summary>
        public InputActionAsset inputActions;
    }
}