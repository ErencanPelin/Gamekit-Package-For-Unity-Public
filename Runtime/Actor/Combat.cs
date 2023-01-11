using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit2D.Runtime.Enums;
using Gamekit2D.Runtime.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D.Runtime.Actor
{
    /// <summary>
    /// Handles the combat mechanics for the actor including attacking & getting hit
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Actor))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [AddComponentMenu(" Eren Kit/Actor/Combat")]
    public class Combat : MonoBehaviour
    {
        [SerializeField] private CombatType combatType;
        [SerializeField] private Transform gunTransform;
        private SpriteRenderer gunSprite;
        [SerializeField] private ParticleSystem bulletParticles;
        [SerializeField] private AttackType attackType;
        [SerializeField] private AttackDirection attackDirection;
        [SerializeField] private bool invert;
        [SerializeField] private Transform target;
        [SerializeField] private bool holdToAttack; 
        [SerializeField] private bool doesTakeDamage;
        [SerializeField] private float damageDealt = 10f;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float invulnerableCooldown;
        [SerializeField] private float knockBackForce;
        public RaySettings attackRaySettings;
        [SerializeField] private UnityEvent onGetHit;
        [SerializeField] private UnityEvent onAttack;

        private Movement movement;
        private Health health;
        private bool canAttack;
        private bool canGetHit;
        private Transform _transform;
        private Vector2 dir = Vector2.right;

        protected virtual void Reset()
        {
            //set default values
            attackCooldown = 0.1f;
            invulnerableCooldown = 1f;
            knockBackForce = 25f;
            attackRaySettings = new RaySettings
            {
                rayLength = 1f,
                rayLayer = 1 << LayerMask.NameToLayer("Actor")
            };
        }

        private void Awake() => Init();

        protected virtual void Init()
        {
            canAttack = canGetHit = true;
            _transform = transform;
            movement = GetComponent<Movement>();

            if (doesTakeDamage)
                health = GetComponent<Health>();
            
            if (attackType is AttackType.onMouseClick && !holdToAttack)
                //register attack function with onMouseDown even from the InputManager
                Inputs.leftMouse.performed += _ => Attack();

            if (gunTransform != null) //create sprite renderer reference for gun
                gunSprite = gunTransform.GetComponent<SpriteRenderer>();
            if (bulletParticles is null)
                bulletParticles = gunTransform.GetComponentInChildren<ParticleSystem>();
            
            switch (attackDirection)
            {
                //verify variables
                case Enums.AttackDirection.targetDirectionX or Enums.AttackDirection.targetDirectionXY when target == null:
                    try
                    {
                        target = GameObject.FindGameObjectWithTag("Player").transform;
                    }
                    catch
                    {
                        Debug.LogWarning($"No Attack Target set, combat will be disabled on {gameObject.name}");
                        enabled = false;
                    }
                    return;
                case Enums.AttackDirection.facingDirectionX when !TryGetComponent(out movement):
                    throw new Exception(
                        "Attack direction set to facing direction, but no movement component was found on this gameObject");
            }
        }

        private void FixedUpdate()
        {
            if (combatType is CombatType.shooter) //update gun rotation
                RotateGun();
            
            if (attackType is not AttackType.AI)
            {
                if (!holdToAttack) return;
                if (Inputs.leftMouse.ReadValue<float>() > 0f)
                    Attack();
                return;
            }

            Attack();
        }

        private void RotateGun()
        {
            //calculate direction
            gunTransform.right = AttackDirection();
            //face global rotation
            gunTransform.localScale =
                Vector3.right * (_transform.lossyScale.x > 0 ? 1 : -1) + Vector3.forward + Vector3.up;
            //flip sprite if needed
            if (attackDirection is Enums.AttackDirection.targetDirectionXY or Enums.AttackDirection.mouseDirectionXY)
                gunSprite.flipY = gunTransform.right.x < 0;
        }
        
        private void Attack()
        {
            if (!canAttack) return;
            switch (combatType)
            {
                case CombatType.melee:
                    var others = AttackRay();
                    var attack = false;
                    foreach (var other in others)
                    {
                        if (!other.transform.TryGetComponent(out Combat oc)) continue;
                        if (oc == this) continue; //this object
                        if (oc.CompareTag(tag)) continue; //same tag
                        if (!oc.canGetHit) continue; //he is invulnerable!
                        oc.GetHit(this);
                        attack = true;
                    }
                    if (!attack && attackType is AttackType.AI) return; //we didn't actually hit anything, so don't attack!
                    StartCoroutine(CanAttack());
                    onAttack.Invoke();
                    break;
                case CombatType.shooter:
                    //do particles
                    bulletParticles.Play();
                    onAttack.Invoke();
                    StartCoroutine(CanAttack());
                    break;
            }
        }

        private void OnParticleCollision(GameObject go)
        {
            var other = go.GetComponentInParent<Combat>();
            if (other is null) return;
            GetHit(other);
        }

        private void GetHit(Combat @this)
        {
            if (!canGetHit) return;
            StartCoroutine(Invulnerable());
            movement.Knockback((_transform.position - @this._transform.position), @this.knockBackForce); //do knockback
            if (doesTakeDamage)
                health.NegateHealth(@this.damageDealt);
            onGetHit.Invoke();
        }

        private IEnumerator Invulnerable()
        {
            canGetHit = false;
            yield return new WaitForSeconds(invulnerableCooldown);
            canGetHit = true;
        }

        private IEnumerator CanAttack()
        {
            canAttack = false;
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }

        private IEnumerable<RaycastHit2D> AttackRay()
        {
            dir = AttackDirection();
          //  if (combatType is CombatType.shooter && dir != Vector2.zero) bulletParticles.transform.forward = dir; //point bullet particles to direction
            return Physics2D.RaycastAll(_transform.position + (Vector3)attackRaySettings.rayStartOffset, dir, attackRaySettings.rayLength,
                attackRaySettings.rayLayer);
        }

        protected virtual Vector2 AttackDirection()
        {
            return attackDirection switch
            {
                Enums.AttackDirection.facingDirectionX => AttackDirFacingX(),
                Enums.AttackDirection.targetDirectionX => AttackDirTargetX(),
                Enums.AttackDirection.targetDirectionXY => AttackDirTargetXY(),
                Enums.AttackDirection.mouseDirectionX => AttackDirCursorX(),
                Enums.AttackDirection.mouseDirectionXY => AttackDirCursorXY(),
                _ => Vector2.right
            };
        }

        /// <summary>
        /// Sets the target for this actor to aim at, **Does not** change the attack or aim SpriteDirection. Can also be invoked
        /// as a UnityEvent from inside the inspector window.
        /// </summary>
        /// <param name="target">Transform target for this actor to aim at</param>
        public void SetAimAtTarget(Transform target)
            => this.target = target;
        
        private Vector2 AttackDirFacingX()
            => (_transform.localScale.x > 0 ? Vector2.right : Vector2.left) * (invert ? -1f : 1f);

        private Vector2 AttackDirTargetX()
            => Vector2.right * ((target.position.x - _transform.position.x) * (invert ? -1f : 1f));

        private Vector2 AttackDirTargetXY()
            => (invert ? _transform.position - target.position : target.position - _transform.position).normalized;

        private Vector2 AttackDirCursorX()
            => Vector2.right *
               (invert ? _transform.position.x - Inputs.mousePos.x : Inputs.mousePos.x - _transform.position.x);

        private Vector2 AttackDirCursorXY()
            => (invert ? _transform.position - Inputs.mousePos : Inputs.mousePos - _transform.position).normalized;
    }
}