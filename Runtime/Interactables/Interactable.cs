using Gamekit2D.Runtime.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Gamekit2D.Runtime.Interactables
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Interaction")]
        [Tooltip("Determines when the onInteract event is called depending on onTrigger area for this interactable")]
        [SerializeField] private InteractionType interactionType;
        [Tooltip("Event called when the player successfully interacts with this object")]
        [SerializeField] private UnityEvent onInteract;
        
        private enum InteractionType
        {
            onPlayerEnter,
            onPlayerEnterAndClick
        }

        private void Reset() //apply defaults
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        protected abstract void Interact();
        
        //called via unity events
        public void DoInteract() => Interact();

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (interactionType is not InteractionType.onPlayerEnter) return;
            if (col.CompareTag("Player")) onInteract.Invoke();
        }
        protected virtual void OnTriggerStay2D(Collider2D col)
        {
            if (interactionType is not InteractionType.onPlayerEnterAndClick) return;
            if (col.CompareTag("Player"))
                Inputs.leftMouse.started += ClickInteract;
        }
        protected virtual void OnTriggerExit2D(Collider2D col)
        {
            if (interactionType is not InteractionType.onPlayerEnterAndClick) return;
            if (col.CompareTag("Player"))
                Inputs.leftMouse.started -= ClickInteract;
        }

        private void ClickInteract(InputAction.CallbackContext callbackContext) => onInteract.Invoke();
    }
}