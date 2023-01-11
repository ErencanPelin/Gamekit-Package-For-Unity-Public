using UnityEngine;

namespace Gamekit2D.Runtime.LevelTiles
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(AreaEffector2D))]
    [AddComponentMenu(" Eren Kit/Level/Air Gust")]
    public class AirGust : MonoBehaviour
    {
        private void Reset()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<BoxCollider2D>().usedByEffector = true;
            GetComponent<AreaEffector2D>().useGlobalAngle = true;
            GetComponent<AreaEffector2D>().forceAngle = 90;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1, 1, 0.4f);
            Gizmos.DrawCube(GetComponent<BoxCollider2D>().offset + (Vector2)transform.position, GetComponent<BoxCollider2D>().size);
        }
    }
}