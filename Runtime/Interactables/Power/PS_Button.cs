using System.Collections;
using UnityEngine;

namespace Gamekit2D.Runtime.Interactables.Power
{
    public class PS_Button : PowerSupply
    {
        [Header("Button")]
        [Tooltip("Time for this source to stay on until it turns itself off")]
        [SerializeField] private float activeTime = 2f;
        [Tooltip("When checked, this source behaves like a lever and stays on. Useful for when a button is activated by standing on it, and should remain on until the player steps off it")]
        [SerializeField] private bool stayOn;
        
        public override bool IsOn
        {
            protected set
            {
                if (!stayOn)
                {
                    StartCoroutine(Power(value));
                    RefreshSprite();
                }
                else
                {
                    isOn = value;
                    RefreshSprite();
                }
            }
        }

        protected override void OnTriggerExit2D(Collider2D col)
        {
            if (!stayOn) return;
            base.Init();
            IsOn = false;
        }

        private IEnumerator Power(bool power)
        {
            isOn = power;
            yield return new WaitForSeconds(activeTime);
            isOn = false;
            RefreshSprite();
        }
        
        protected override void Interact() => IsOn = true;
    }
}