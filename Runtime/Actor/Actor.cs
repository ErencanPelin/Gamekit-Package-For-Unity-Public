using System;
using Gamekit2D.Runtime.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gamekit2D.Runtime.Actor
{
    /// <summary>
    /// The main Actor component which all other components rely on
    /// </summary>
    [AddComponentMenu(" Eren Kit/Actor/Actor")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(SpriteDirection))]
    public class Actor : MonoBehaviour
    {
        [SerializeField] private bool useCheckpoints;
        [SerializeField] private bool canResetSelf;

        public bool UseCheckpoints => useCheckpoints;
        private Vector2 spawnPoint;
        private Action<InputAction.CallbackContext> resetBinding;

        private void Reset()
        {
            gameObject.layer = LayerMask.NameToLayer("Actor");
            useCheckpoints = true;
            canResetSelf = true;
        }

        private void Awake() => Init();

        /// <summary>
        /// Initialises the component, - can be overriden, called on Awake()
        /// </summary>
        protected virtual void Init()
        {
            resetBinding = _ => Respawn();
            spawnPoint = transform.position;
            if (canResetSelf)
                Inputs.reset.performed += resetBinding;
        }

        private void OnDisable()
        {
            Inputs.reset.performed -= resetBinding;
        }

        /// <summary>
        /// Sets the respawn position of this actor to a given position
        /// </summary>
        /// <param name="position">Vector2 new respawn position</param>
        public void SetSpawn(Vector2 position) => spawnPoint = position;

        /// <summary>
        /// Teleports the actor to their saved spawnPoint position
        /// </summary>
        public void Respawn() => transform.position = spawnPoint;

        /// <summary>
        /// Simply destroys this actor gameObject and removes it from the scene
        /// </summary>
        /// <param name="seconds">Seconds to wait in real time before disposing of the object</param>
        public void Destroy(float seconds) => Destroy(gameObject, seconds);
    }
}