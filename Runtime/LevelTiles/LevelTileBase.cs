using UnityEngine;

namespace Gamekit2D.Runtime.LevelTiles
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class LevelTileBase : MonoBehaviour
    {
        [SerializeField] private bool onlyAffectPlayer = true;

        private void Reset()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (onlyAffectPlayer && !col.CompareTag("Player")) return;
            Perform(col.gameObject);
        }

        protected abstract void Perform(GameObject interactingObject = null);
    }
}