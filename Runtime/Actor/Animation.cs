using System;
using Gamekit2D.Runtime.Enums;
using UnityEngine;

namespace Gamekit2D.Runtime.Actor
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Actor))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu(" Eren Kit/Actor/Animation")]
    public class Animation : MonoBehaviour
    {
        [HideInInspector] public Animator anim;
        private Movement movement;
        
        private void Awake() => Init();

        protected virtual void Init()
        {
            anim = GetComponent<Animator>();
            movement = GetComponent<Movement>();
            if (anim.runtimeAnimatorController is null)
                throw new Exception($"Animator controller isn't set on the Animator component on {gameObject.name}");
        }

        private void Update() //updates all the animation values
            => UpdateAnimations();

        /// <summary>
        /// Updates all animation values on the attached animator. Can be overridden, however you should call base.UpdateAnimations()
        /// </summary>
        protected virtual void UpdateAnimations()
        {
            if (movement.actorType is ActorType.platformer)
            {
                SetBool("onGround", movement.onGround);
                SetFloat("yVelocity", Mathf.Round(movement.Velocity.y));
            }
            else
                SetFloat("yVelocity", Mathf.Round(movement.input.normalized.y));

            SetFloat("xVelocity", Mathf.Round(Mathf.Abs(movement.input.normalized.x)));
        }
        
        /// <summary>
        /// Enables a trigger parameter on the animator, can also be invoked as a UnityEvent from the Inspector
        /// </summary>
        /// <param name="triggerName">string name of the trigger to enable</param>
        public void SetTrigger(string triggerName) => anim.SetTrigger(triggerName);
        /// <summary>
        /// Sets a given bool parameter on the animator to a given value
        /// </summary>
        /// <param name="name">string name of the parameter to modify</param>
        /// <param name="value">bool new value</param>
        public void SetBool(string name, bool value) => anim.SetBool(name, value);
        /// <summary>
        /// Sets a given int parameter on the animator to a given value
        /// </summary>
        /// <param name="name">string name of the parameter to modify</param>
        /// <param name="value">int new value</param>
        public void SetInt(string name, int value) => anim.SetInteger(name, value);
        /// <summary>
        /// Sets a given float parameter on the animator to a given value
        /// </summary>
        /// <param name="name">string name of the parameter to modify</param>
        /// <param name="value">float new value</param>
        public void SetFloat(string name, float value) => anim.SetFloat(name, value);
        /// <summary>
        /// Toggles the value of a given bool parameter on the animator. If it was true, it becomes false, and if it was false, it becomes true
        /// </summary>
        /// <param name="name">string name of the parameter to modify</param>
        public void ToggleBool(string name) => anim.SetBool(name, !anim.GetBool(name));
    }
}