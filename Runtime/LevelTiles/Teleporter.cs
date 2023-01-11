using System.Collections;
using Gamekit2D.Runtime.Actor;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D.Runtime.LevelTiles
{
    [AddComponentMenu(" Eren Kit/Level/Teleporter")]
    public class Teleporter : LevelTileBase
    {
        [SerializeField] private Teleporter partnerLocation;

        [SerializeField] private UnityEvent onTeleportAway;
        [SerializeField] private UnityEvent onTeleportTo;
        
        protected override void Perform(GameObject interactingObject = null)
        {
            onTeleportAway.Invoke();
            interactingObject.transform.position = partnerLocation.transform.position;
            partnerLocation.onTeleportTo.Invoke();
            StartCoroutine(partnerLocation.Disable());
        }

        private IEnumerator Disable()
        {
            GetComponent<Collider2D>().enabled = false;
            yield return new WaitForSeconds(1f);
            GetComponent<Collider2D>().enabled = true;
        }
    }
}