using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D.Runtime.Collectables
{
    /// <summary>
    /// Collectable object behaviour, destroys OnTrigger with the player, and invokes the onCollect events described in the inspector
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    [AddComponentMenu(" Eren Kit/Level/Collectable")]
    public class Collectable : MonoBehaviour
    {
        [Tooltip("Events called when this collected touches/is collected by the player")]
        [SerializeField] private UnityEvent onCollect;

        private void Reset() //apply defaults
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
                Collect();
        }   

        /// <summary>
        /// Called automatically when the player collects this object. By default, invokes the onCollect events. When overriding this function, you should still include base.Collect();
        /// </summary>
        protected virtual void Collect() => onCollect.Invoke(); //do the collect events as described in the editor
        public void Destroy(float afterSeconds) => Destroy(gameObject, afterSeconds);
    }
}