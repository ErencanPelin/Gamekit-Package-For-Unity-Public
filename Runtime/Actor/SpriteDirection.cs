using System;
using Gamekit2D.Runtime.Enums;
using Gamekit2D.Runtime.Utils;
using UnityEngine;

namespace Gamekit2D.Runtime.Actor
{
    /// <summary>
    /// Handles the direction for the sprite to face
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Actor))]
    [AddComponentMenu(" Eren Kit/Actor/Sprite Direction")]
    public class SpriteDirection : MonoBehaviour
    {
        [SerializeField] private bool invertDirection;
        [SerializeField] private DirectionType faceDirection;
        [SerializeField] private Transform lookAtTarget;

        protected Transform _transform;
        private Vector2 scale = Vector2.one; //reference to our scale
        private Movement movement;
        private Vector3 originalScale;
        
        private void Awake() => Init();

        /// <summary>
        /// Initialises the Component - can be overridden, - called on Awake()
        /// </summary>
        /// <exception cref="UnassignedReferenceException"></exception>
        protected virtual void Init()
        {
            _transform = transform;
            originalScale = _transform.localScale; //store our current scale in cache so we don't modify the type set in the inspector
            
            //verify the inspector variables:
            switch (faceDirection)
            {
                case DirectionType.velocity when !TryGetComponent(out movement):
                    throw new Exception("Using Velocity direction requires a Movement component to be attached to this gameObject!");
                case DirectionType.target when lookAtTarget is null:
                    throw new Exception("Using Target direction, but there is no target set in the inspector!");
            }
        }

        private void FixedUpdate()
        {
            scale = CalculateDirection();
            _transform.localScale = new Vector3(
                scale.x * originalScale.x * (invertDirection ? -1 : 1),
                scale.y * originalScale.y,
                originalScale.z);
        }

        /// <summary>
        /// Calculate the desired scale for this actor and return it as a type.
        /// </summary>
        /// <returns>A Vector2 in the format of (-1, 1) or (1, 1) to represent the scale of this actor where -1 flips the actor's current scale</returns>
        protected virtual Vector2 CalculateDirection()
        {
            return faceDirection switch
            {
                DirectionType.velocity => VelocityDirection(),
                DirectionType.target => TargetDirection(),
                DirectionType.cursor => CursorDirection(),
                _ => Vector2.one
            };
        }
        
        /// <summary>
        /// Sets the target for this actor to look at, also sets the SpriteDirection type to DirectionType.Target if it is not already set. Can also be invoked
        /// as a UnityEvent from inside the inspector window.
        /// </summary>
        /// <param name="target">Transform target for this actor to look at</param>
        public void SetLookAtTarget(Transform target)
        {
            lookAtTarget = target;
            faceDirection = DirectionType.target;
        }
        
        private Vector2 VelocityDirection()
            => Mathf.Approximately(movement.input.x, 0) ? scale : new Vector2((movement.input.x > 0 ? 1 : -1), 1);

        private Vector2 CursorDirection()
            => Mathf.Approximately(_transform.position.x - Inputs.mousePos.x, 0) ? scale : 
                new Vector2((_transform.position.x > Inputs.mousePos.x ? 1 : -1), 1);

        private Vector2 TargetDirection()
            => Mathf.Approximately(_transform.position.x - lookAtTarget.position.x, 0) ? scale : 
                new Vector2((_transform.position.x > lookAtTarget.position.x ? 1 : -1), 1);
    }
}