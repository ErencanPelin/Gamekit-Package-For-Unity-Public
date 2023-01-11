using System.Collections;
using System.Numerics;
using Gamekit2D.Runtime.Extensions;
using Gamekit2D.Runtime.Enums;
using Gamekit2D.Runtime.LevelTiles;
using Gamekit2D.Runtime.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.Vector2;
using Vector2 = UnityEngine.Vector2;

namespace Gamekit2D.Runtime.Actor
{
    /// <summary>
    /// The actor class which handles all movement controls for a 2D character. Requires the Actor class, Rigidbody2D and a CapsuleCollider2D
    /// attached to a gameObject to work properly
    /// </summary>
    [AddComponentMenu(" Eren Kit/Actor/Movement")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Actor))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Movement : MonoBehaviour
    {
        //handled by custom Editor
        public ActorType actorType;
        [SerializeField] private float gravityForce;
        [SerializeField] private float maxFallSpeed;
        [SerializeField] private float maxJumpVelocity;
        [SerializeField] private float jumpForce;
        [SerializeField] private float airControlPercent;
        public float doubleJumpMinThreshold, doubleJumpMaxThreshold;
        [SerializeField] private bool canDoubleJump;
        [SerializeField] private InputType inputType;
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float moveSmoothing;
        [SerializeField] private bool canDash;
        [SerializeField] private float dashForce;
        [SerializeField] private float dashCooldown;
        [SerializeField] private UnityEvent onJump;
        [SerializeField] private UnityEvent onDash;
        [SerializeField] private UnityEvent onDoubleJump;
        public RaySettings groundCheckSettings;

        /// <summary>Transform component attached to this Actor. Use this to reduce expensive referencing</summary>
        protected Transform _transform; //reference to the transform attached to this object
        /// <summary>Rigidbody2D component attached to this Actor. Use this to reduce expensive referencing</summary>
        protected Rigidbody2D rb; //the rigidbody attached to this object
        private bool hasDoubleJumped;
        protected Vector2 velocity; //holds the current velocity for the object, modify this to make the object move
        private Vector2 _knockBack;
        private Vector2 _vel; //velocity in cache to apply to the active velocity. It is set to zero after being applied - used for things like knockback
        private Rigidbody2D followPlatform;
        private bool hasDashed;

        //properties
        public Vector2 Velocity => velocity; //exposed version of the actor's velocity
        public Vector2 input { get; private set; } //the input direction which gets passed to the move functions
        public bool onGround { get; private set; } //returns true when the object is on the ground

        private void Awake() => Init();

        /// <summary>
        /// Initialisation - sets the rigidbody and transform references - can be overridden
        /// </summary>
        protected virtual void Init()
        {
            rb = GetComponent<Rigidbody2D>();
            _transform = transform;

            //verify values
            if (inputType is InputType.target && target == null)
            {
                try
                {
                    target = GameObject.FindGameObjectWithTag("Player").transform;
                }
                catch
                {
                    Debug.LogWarning($"No Follow Target set, motion will be disabled on {gameObject.name}");
                    enabled = false;
                }
            }

            if (canDash)
                Inputs.dash.performed += _ => Dash(); //add dash listener
        }

        private void Reset()
        {
            GetComponent<Collider2D>().sharedMaterial = GetComponent<Rigidbody2D>().sharedMaterial =
                AssetSearch.GetAsset<PhysicsMaterial2D>("Player");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
            GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            //set default values
            moveSpeed = 50f;
            moveSmoothing = 0.1f;
            gravityForce = 2f;
            jumpForce = 50f;
            dashForce = 30f;
            dashCooldown = 3f;
            airControlPercent = 0.5f;
            maxFallSpeed = maxJumpVelocity = 23f;
            doubleJumpMinThreshold = -23f;
            doubleJumpMaxThreshold = -20f;
            groundCheckSettings = new RaySettings
            {
                rayLayer = 1 << LayerMask.NameToLayer("Standable") | 1 << LayerMask.NameToLayer("Default"),
                rayLength = 0.6f
            };
        }

        private void FixedUpdate()
        {
            input = GetInput(); //get the direction for where to move the actor
            velocity = rb.velocity;
            velocity = CalculateMovement();
            _vel = zero;
            if (_knockBack != zero) velocity = _knockBack;
            _knockBack = zero;
            rb.velocity = velocity; //apply velocity to the rigidbody
        }

        /// <summary>
        /// Calculates the velocity to move in and returns the velocity - can be overridden
        /// </summary>
        /// <returns>Vector2 velocity to move in</returns>
        protected Vector2 CalculateMovement()
        {
            return actorType switch
            {
                ActorType.platformer => MovePlatformer(),
                ActorType.topdown => MoveTopDown(),
                _ => zero
            };
        }

        /// <summary>
        /// Move the actor using platformer movement style - using gravity and allowing jump
        /// </summary>
        private Vector2 MovePlatformer()
        {
            var vel = velocity;
            onGround = OnGround();
            ApplyGravity(ref vel);
            vel.y += _vel.y;
            
            vel.x = Mathf.Lerp(vel.x, (input.x * (10 * (moveSpeed * Time.fixedDeltaTime))) + _vel.x,
                moveSmoothing);
            vel.x = onGround ? vel.x : Mathf.Lerp(rb.velocity.x, vel.x, airControlPercent);
            
            //jumping
            if (input.y > 0.5f && onGround)
            {
                velocity.y = jumpForce;
                onJump.Invoke();
            }
            //double jumping
            if (canDoubleJump && onGround) hasDoubleJumped = false;
            if (input.y > 0.5f && onGround) vel.y = jumpForce;
            if (input.y > 0.5f &&
                canDoubleJump && !hasDoubleJumped &&
                vel.y > doubleJumpMinThreshold && vel.y < doubleJumpMaxThreshold &&
                !onGround)
            {
                vel.y = jumpForce;
                hasDoubleJumped = true;
                onDoubleJump.Invoke();
            }
            
            return vel;
        }

        /// <summary>
        /// Sets the target for this actor to follow, also sets the input type to InputType.Target if it is not already set. Can also be invoked
        /// as a UnityEvent from inside the inspector window.
        /// </summary>
        /// <param name="target">Transform target for this actor to follow</param>
        public void SetFollowTarget(Transform target)
        {
            this.target = target;
            inputType = InputType.target;
        }
        
        /// <summary>
        /// Move the actor using top down movement style
        /// </summary>
        private Vector2 MoveTopDown()
        {
            var vel = velocity;
            vel.x = Mathf.Lerp(vel.x, input.x * (10 * (moveSpeed * Time.fixedDeltaTime)),
                moveSmoothing);
            vel.y = Mathf.Lerp(vel.y, input.y * (10 * (moveSpeed * Time.fixedDeltaTime)),
                moveSmoothing);
            return vel;
        }

        // Calculate the input direction based on the inputType
        private Vector2 GetInput()
        {
            return inputType switch
            {
                InputType.keyboard => Inputs.move.ReadValue<Vector2>(),
                InputType.target => ((Vector2)(target.position - _transform.position)).Round().normalized,
                _ => zero
            };
        }

        /// <summary>
        /// Applies a gravity force onto the actor to make them fall - clamps their Y velocity to their maxFallSpeed
        /// </summary>
        private void ApplyGravity(ref Vector2 vel)
        {
            if (onGround)
            {
                vel.y = -gravityForce;
                return;
            }
            
            vel.y -= gravityForce;
            vel.y = Mathf.Clamp(vel.y, -maxFallSpeed, maxJumpVelocity);
        }

        /// <summary>
        /// Casts a ray down and returns whether the ray hit something (if the play is on the ground)
        /// </summary>
        /// <returns>True if this object is on a solid ground</returns>
        private bool OnGround()
        {
            var hit = Physics2D.Raycast((Vector2)_transform.position + groundCheckSettings.rayStartOffset,
                down, groundCheckSettings.rayLength, groundCheckSettings.rayLayer);
            if (hit.collider is null) return false;
            return !hit.collider.isTrigger;
        }

        private void Dash()
        {
            if (hasDashed) return;
            Knockback(input, dashForce);
            onDash.Invoke();
            StartCoroutine(DashCooldown());
        }

        private IEnumerator DashCooldown()
        {
            hasDashed = true;
            yield return new WaitForSeconds(dashCooldown);
            hasDashed = false;
        }

        /// <summary>
        /// Applies a short burst force in the given direction.
        /// </summary>
        /// <param name="direction">Vector2 direction to push the player</param>
        /// <param name="force">float strength of the force</param>
        public void Knockback(Vector2 direction, float force)
            => _knockBack = direction.normalized * force;

        public void SetFollowPlatform(Rigidbody2D platform)
        {
            if (platform is null)
            {
                _vel = zero;
                return;
            }
            _vel = platform.velocity;
        }
    }
}