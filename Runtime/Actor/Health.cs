using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gamekit2D.Runtime.Actor
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Actor))]
    [AddComponentMenu(" Eren Kit/Actor/Health")]
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        public float curHealth { get; private set; }
        [SerializeField] private Slider healthSlider;
        [SerializeField] public UnityEvent onDeath;

        private void Awake() => Init();

        protected virtual void Init()
        {
            curHealth = maxHealth;
            UpdateSlider();
        }

        protected virtual void Reset()
        {
            maxHealth = 100f;
        }

        /// <summary>
        /// Negates a given value from the actor's health. If health is then 0, the function invokes the 'OnDeath' event
        /// </summary>
        /// <param name="amount">The amount of health to negate</param>
        public void NegateHealth(float amount)
        {
            curHealth = Mathf.Clamp(curHealth - amount, 0, maxHealth);
            if (curHealth is 0) onDeath.Invoke();
            UpdateSlider();
        }

        /// <summary>
        /// Adds a given value to the actor's health.
        /// </summary>
        /// <param name="amount">The amount of health to be added</param>
        public void AddHealth(float amount)
        {
            curHealth = Mathf.Clamp(curHealth + amount, 0, maxHealth);
            UpdateSlider();
        }

        /// <summary>
        /// Sets the value of the Actor's current health
        /// </summary>
        /// <param name="value">New value of health</param>
        public void SetHealth(float value)
        {
            curHealth = Mathf.Clamp(value, 0, maxHealth);
            if (curHealth is 0) onDeath.Invoke();
            UpdateSlider();
        }

        private void UpdateSlider()
        {
            if (healthSlider is null) return; //no slider set
            healthSlider.maxValue = maxHealth;
            healthSlider.value = curHealth;
        }
    }
}