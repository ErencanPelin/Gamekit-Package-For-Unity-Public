using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D.Runtime.LevelTiles
{
    [RequireComponent(typeof(BoxCollider2D))]
    [AddComponentMenu(" Eren Kit/Level/Checkpoint")]
    public class Checkpoint : LevelTileBase
    {
        [Tooltip("Event is fired when the player actives this checkpoint by walking past it")]
        [SerializeField] private UnityEvent onCheckPointAchieved;
        private bool cp_achieved;
        
        private void Reset() //apply defaults
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        protected override void Perform(GameObject interactingObject = null)
        {
            if (cp_achieved) return;
            if (interactingObject is null) return;
            if (interactingObject.TryGetComponent(out Actor.Actor actor))
            {
                if (actor.UseCheckpoints)
                    actor.SetSpawn(transform.position);
            }

            cp_achieved = true;
            onCheckPointAchieved.Invoke();
        }
    }
}
