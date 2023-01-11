using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D.Runtime.Interactables.Power
{
    [DisallowMultipleComponent]
    public class Powered : MonoBehaviour
    {
        [field: Tooltip("On/Off state for this target")]
        [field: SerializeField] public bool isOn { get; private set; }
        [Tooltip("When checked, this target will invert the signal from the power source.\ni.e. when the source is on, this target is off")]
        [SerializeField] private bool invertPower;
        [HideInInspector] public PowerSupply powerSupply;

        [Header("Events")] 
        [Tooltip("Event is fired when the signal from source turns this target on")]
        [SerializeField] private UnityEvent turnOn;
        [Tooltip("Event is fired when the signal from source turns this target off")]
        [SerializeField] private UnityEvent turnOff;
        
        private void OnDrawGizmosSelected()
        {
            if (powerSupply is null) return;
            DrawGizmo();
            powerSupply.DrawGizmo();
        }
        public void DrawGizmo()
        {
            UpdatePower();
            //draw a cube to show if we're on or off
            Gizmos.color = isOn ? new Color(.1f, 1f, 1f) : new Color(1,.7f, 0.1f);
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }

        public void UpdatePower()
        {
            //update our own state
            isOn = powerSupply.IsOn;
            if (invertPower) isOn = !isOn;
            //call events
            if (isOn) turnOn.Invoke();
            else turnOff.Invoke();;
        }
    }
}