using System.Collections.Generic;
using Gamekit2D.Runtime.Actor;
using UnityEngine;

namespace Gamekit2D.Runtime.LevelTiles
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    [AddComponentMenu(" Eren Kit/Level/Moving Platform")]
    public class MovingPlatform : MonoBehaviour
    {
        [Tooltip("Speed at which this platform moves along its set path")]
        [SerializeField] private float moveSpeed = 1f;
        [Tooltip("List of points/nodes along this platform's path")]
        public List<Vector2> points = new();
        private Rigidbody2D rb;
        private int targetPointIndex;
        private bool backwardsDir;

        private void Reset() //apply defaults
        {
            gameObject.layer = LayerMask.NameToLayer("Standable");
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<CircleCollider2D>().isTrigger = true;
            GetComponent<CircleCollider2D>().offset = new Vector2(0, 0.5f);
            GetComponent<CircleCollider2D>().radius = 0.4f;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (targetPointIndex >= points.Count - 1)
                backwardsDir = true;
            if (targetPointIndex == 0)
                backwardsDir = false;
            
            if (Vector2.Distance(transform.position, points[targetPointIndex]) < 0.1f)
                targetPointIndex = backwardsDir? targetPointIndex - 1 : targetPointIndex + 1;
                
            rb.velocity = (points[targetPointIndex] - (Vector2)transform.position).normalized * (moveSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.layer != LayerMask.NameToLayer("Actor")) return;
            if (col.TryGetComponent(out Movement movement))
                movement.SetFollowPlatform(rb);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.layer != LayerMask.NameToLayer("Actor")) return;
            if (col.TryGetComponent(out Movement movement))
                movement.SetFollowPlatform(null);
        }
    }
}