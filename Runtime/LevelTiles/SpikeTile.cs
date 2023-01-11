using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D.Runtime.LevelTiles
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    [AddComponentMenu(" Eren Kit/Level/Spike Tile")]
    public class SpikeTile : MonoBehaviour
    {
        [Tooltip("Event fires when player touches this tile. NOTE: the player will automatically die, this event should active only components on this object/its children")]
        [SerializeField] private UnityEvent onPlayerTouch;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f,0.1f, 0.1f, 0.5f);
            Gizmos.DrawCube(  (Vector2)transform.position + GetComponent<BoxCollider2D>().offset, GetComponent<BoxCollider2D>().size);
        }

        private void Reset() //apply defaults
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            onPlayerTouch.Invoke();
        }
    }
}